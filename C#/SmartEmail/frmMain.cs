using Microsoft.Data.Sqlite;
//using MimeKit;
//using Newtonsoft.Json;
//using Org.BouncyCastle.Crypto.Generators;
//using SQLitePCL;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Design;
using System.Data;
//using System.Diagnostics;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Security.Cryptography;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Xml.Linq;
//using UglyToad.PdfPig;
//using static System.Net.Mime.MediaTypeNames;
//using System.IO;
namespace SmartEmail
{
    public partial class frmMain : Form
    {
        #region Form
        /// <summary>
        /// Main form of the SmartEmail application.
        /// Handles user interactions for settings, downloading emails, 
        /// saving credentials, and initializing the database.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Handles the form load event.
        /// Initializes UI, loads registry settings, connects to SQLite, 
        /// and ensures required tables are created.
        /// </summary>
        private void frmMain_Load(object sender, EventArgs e)
        {
            SqliteConnection conn = new SqliteConnection();
            try
            {
                AddRemoveTabs(false);
                BaseFunctions.PopCmbEmailProviders(cmbSettingsEmailProvider);
                LoadRegistry();
                string connString = $"Data Source={GVar.PathDB}";
                conn = new SqliteConnection(connString);
                conn.Open();
                MySQLite.CreateTableQueries(conn);
                MySQLite.CreateTableEmails(conn);
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// Loads user preferences and saved credentials from the registry.
        /// Applies saved email, password, provider, and keyword settings to UI controls.
        /// </summary>
        private void LoadRegistry()
        {
            try
            {
                //Email Credentials
                if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("saveCred", "0").ToString() == "1") chkSettingsEmailSave.Checked = true;
                txtSettingsEmail.Text = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("emailId", "").ToString();
                txtSettingsPassword.Text = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("emailPwd", "").ToString();
                cmbSettingsEmailProvider.SelectedIndex = Convert.ToInt16(System.Windows.Forms.Application.UserAppDataRegistry.GetValue("emailProvider", "0"));

                //Keywords for downloading emails
                if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("saveDownloadEmailKeys", "0").ToString() == "1") chkDownloadKeyWords.Checked = true;
                txtDownloadKeyWords.Text = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("downloadKeyWords", "").ToString();
            }
            catch
            {
            }
        }
        #endregion

        #region Settings
        /// <summary>
        /// Handles the Connect button click in the settings tab.
        /// Attempts to connect to the selected email provider using the provided credentials.
        /// Saves or clears credentials in the registry based on the "save" checkbox.
        /// </summary>
        private void btnSettingsEmailConnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string emailProvider = cmbSettingsEmailProvider.SelectedItem.ToString();
                if (emailProvider == null || emailProvider == "")
                {
                    MessageBox.Show("Please select an email provider");
                    return;
                }
                if (Emails.Connect(emailProvider, txtSettingsEmail.Text, txtSettingsPassword.Text))
                {
                    MessageBox.Show("Successfully connected");
                    AddRemoveTabs(true);
                    if (chkSettingsEmailSave.Checked)
                    {
                        System.Windows.Forms.Application.UserAppDataRegistry.SetValue("emailId", txtSettingsEmail.Text);
                        System.Windows.Forms.Application.UserAppDataRegistry.SetValue("emailPwd", txtSettingsPassword.Text);
                        System.Windows.Forms.Application.UserAppDataRegistry.SetValue("emailProvider", cmbSettingsEmailProvider.SelectedIndex.ToString());
                    }
                    else
                    {
                        System.Windows.Forms.Application.UserAppDataRegistry.SetValue("emailId", "");
                        System.Windows.Forms.Application.UserAppDataRegistry.SetValue("emailPwd", "");
                        System.Windows.Forms.Application.UserAppDataRegistry.SetValue("emailProvider", "0");
                    }
                }
                else
                {
                    return;
                }
            }
            catch
            {
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Toggles password visibility when the "show password" checkbox is changed.
        /// </summary>
        private void ChkSettingsPwdShow_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtSettingsPassword.PasswordChar = ChkSettingsPwdShow.Checked ? '\0' : '*';
            }
            catch
            {
            }
        }
        /// <summary>
        /// Saves the "remember credentials" checkbox state to the registry.
        /// </summary>
        private void chkSettingsEmailSave_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string saveCred = "0";
                if (chkSettingsEmailSave.Checked)
                {
                    saveCred = "1";
                }
                System.Windows.Forms.Application.UserAppDataRegistry.SetValue("saveCred", saveCred);
            }
            catch
            {
            }
        }
        #endregion

        #region DownLoad
        /// <summary>
        /// Updates the TreeView with email previews when a month node is selected.
        /// Calls Emails.PopTV to display available emails.
        /// </summary>
        private void TVDownload_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                Emails.PopTV(TVDownload);
            }
            catch
            {
            }
        }
        /// <summary>
        /// Downloads emails for the selected date.
        /// Creates a folder if needed, retrieves emails via IMAP, 
        /// saves them locally, and updates the TreeView.
        /// Also saves/clears keyword preferences in the registry.
        /// </summary>
        private void btnDownloadDownloadEmails_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DateTime xDate = dateTimePicker1.Value;
                string yyyyMM = xDate.ToString("yyyyMM");
                GVar.MonthFolderName = GVar.PathEmailDL + yyyyMM;
                if (!Directory.Exists(GVar.MonthFolderName)) Directory.CreateDirectory(GVar.MonthFolderName);
                List<string> KeyWords = txtDownloadKeyWords.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                KeyWords = KeyWords.Select(p => p.Trim()).Where(p => !string.IsNullOrWhiteSpace(p)).ToList();

                List<EmailInfo> emails = Emails.DownLoadByDateAndKeyWords(xDate, KeyWords);
                BaseFunctions.PopMonthFolders(TVDownload);
                TVDownload.SelectedNode = TVDownload.Nodes[yyyyMM];
                if (chkDownloadKeyWords.Checked)
                {
                    System.Windows.Forms.Application.UserAppDataRegistry.SetValue("downloadKeyWords", txtDownloadKeyWords.Text);
                }
                else
                {
                    System.Windows.Forms.Application.UserAppDataRegistry.SetValue("downloadKeyWords", "");
                }
            }
            catch
            {
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Saves the "remember keywords" checkbox state to the registry.
        /// </summary>
        private void chkDownloadKeyWords_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string saveDownloadEmailKeys = "0";
                if (chkDownloadKeyWords.Checked)
                {
                    saveDownloadEmailKeys = "1";
                }
                System.Windows.Forms.Application.UserAppDataRegistry.SetValue("saveDownloadEmailKeys", saveDownloadEmailKeys);
            }
            catch
            {
            }
        }
        #endregion region

        #region Embed
        /// <summary>
        /// Handles month selection in the Embed tab TreeView.
        /// Displays emails for the selected month (in short format) 
        /// and highlights already embedded ones.
        /// </summary>
        private void TVEmbedMonths_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                Emails.PopTV(TVEmbedMonths, true);
                HighligtEmbedded(TVEmbedMonths.SelectedNode);
            }
            catch
            {
            }
        }
        /// <summary>
        /// Handles the Embed Emails button click.
        /// Runs the embedding pipeline for all unembedded emails 
        /// in the selected month by calling AI.EmbedEmails.
        /// </summary>
        private void btnEmbedEmbedEmails_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (TVEmbedMonths.SelectedNode == null)
                {
                    MessageBox.Show("Please select the month.");
                    return;
                }
                string yyyyMM = TVEmbedMonths.SelectedNode.Text.ToString();
                AI.EmbedEmails(yyyyMM);
                //string pathMonth = GVar.PathEmailDL + yyyyMM;
                //List <string> LstToBeEmbedded =BaseFunctions.GetToBeEmbeddedEmails(pathMonth);
                //string ToBeEmbeddedUIDs = string.Join(",", LstToBeEmbedded);

                //if (ToBeEmbeddedUIDs == "")
                //{
                //    MessageBox.Show("All emails are embedded.");
                //    return;
                //}
                //ProcessStartInfo psi = new ProcessStartInfo
                //{
                //    FileName = GVar.PathPythonExe,
                //    Arguments = $"\"{GVar.PathPythonProgEmail}\" \"{ToBeEmbeddedUIDs.Replace("\"", "\\\"")}\" \"{pathMonth}\"",
                //    RedirectStandardOutput = true,
                //    RedirectStandardError = true,
                //    UseShellExecute = false,
                //    CreateNoWindow = true
                //};
                //var process = Process.Start(psi);
                //string output = process.StandardOutput.ReadToEnd();
                //string errors = process.StandardError.ReadToEnd();
                //process.WaitForExit();

                //if (!string.IsNullOrWhiteSpace(errors))
                //{
                //    MessageBox.Show("Python script error:\n" + errors);
                //    return;
                //}
                //if (!File.Exists(pathMonth + "\\temp.json"))
                //{
                //    MessageBox.Show("JSON file not found. Make sure the script created it.");
                //    return;
                //}

                //string jsonText = File.ReadAllText(pathMonth + "\\temp.json");
                //var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                //List<EmailEmbedding> emails = System.Text.Json.JsonSerializer.Deserialize<List<EmailEmbedding>>(jsonText, options);
                //SQLite.AddEmails(GVar.PathDB, emails);

                //MessageBox.Show($"Loaded {emails.Count} embeddings.");
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Query
        /// <summary>
        /// Adds a new query using the short name and description provided by the user.
        /// Also refreshes the TreeView to display the new entry.
        /// </summary>
        private void btnQueryAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Queries.AddUpdate(GVar.PathDB, txtQueryShortName.Text, txtQueryDescription.Text);
                Queries.PopTV(TVQuery, GVar.PathDB);
            }
            catch
            {
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Updates the selected query with new values from the text fields.
        /// Prompts for confirmation before overwriting the record.
        /// </summary>
        private void btnQueryUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (TVQuery.SelectedNode == null)
                {
                    MessageBox.Show("Please select a query to update.");
                    return;
                }
                if (MessageBox.Show("You are updating a query.. Are you sure ?", "-", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                Queries.AddUpdate(GVar.PathDB, txtQueryShortName.Text, txtQueryDescription.Text, TVQuery.SelectedNode.Name);
                Queries.PopTV(TVQuery, GVar.PathDB);
            }
            catch
            {
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Deletes the selected query from the database.
        /// Prompts for confirmation before removing the entry.
        /// </summary>
        private void btnQueryDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (TVQuery.SelectedNode == null)
                {
                    MessageBox.Show("Please select a query to delete");
                    return;
                }
                if (MessageBox.Show("You are deleting a query.. Are you sure ?", "-", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                Queries.Delete(GVar.PathDB, TVQuery.SelectedNode.Name);
                Queries.PopTV(TVQuery, GVar.PathDB);
            }
            catch
            {
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Populates the query fields (short name, description) when a query node 
        /// in the TreeView is double-clicked.
        /// </summary>
        private void TVQuery_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Queries.PopInRev(GVar.PathDB, TVQuery, txtQueryShortName, txtQueryDescription);
        }
        #endregion

        #region Finder
        /// <summary>
        /// Handles the Find button click in the Finder tab.
        /// Loads the embedding for the selected query, 
        /// compares it against all email embeddings using cosine similarity, 
        /// and displays ranked results in the DataGridView.
        /// </summary>
        private void btnFinderFind_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (TVFinderQueries.SelectedNode == null) return;
                string xQueryShortName = TVFinderQueries.SelectedNode.Text;
                List<string> QueryEmbedding = MySQLite.GetQueryEmbedding(TVFinderQueries.SelectedNode.Name);
                List<(string, List<string>, double)> xLstEmailEmbeddings = MySQLite.GetEmailEmbedding();

                //QueryShortName, EmailUID, cosineSimilarity
                List<(string, double)> cosSimilarity = new List<(string, double)>();
                for (int i = 0; i < xLstEmailEmbeddings.Count; i++)
                {
                    cosSimilarity.Add((xLstEmailEmbeddings[i].Item1, BaseFunctions.GetCosineSimilarity(QueryEmbedding, xLstEmailEmbeddings[i].Item2)));
                }
                // PopTVCosineSimilarity(TVFinderResult, cosSimilarity);
                PopDGVCosineSimilarity(dgvFinder, cosSimilarity);
            }
            catch { }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Populates the DataGridView with cosine similarity results.
        /// Adds Score, UID, and Subject columns if missing,
        /// clears old rows, sorts results in descending order, 
        /// and displays formatted similarity scores with corresponding email subjects.
        /// </summary>
        private void PopDGVCosineSimilarity(DataGridView dgv, List<(string UID, double cScore)> xLst)
        {
            try
            {
                if (dgv.Columns.Count == 0)
                {
                    dgv.Columns.Add("Score", "Score");
                    dgv.Columns.Add("UID", "UID");
                    dgv.Columns.Add("Subject", "Subject");
                }
                // dgv.Columns[2].Width = 200;
                dgv.Columns["Subject"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Rows.Clear(); // Clear existing rows

                // Sort the list by descending cosine score
                xLst = xLst.OrderByDescending(p => p.cScore).ToList();

                foreach (var (UID, cScore) in xLst)
                {
                    string score = cScore.ToString("0.000");
                    string subject = MySQLite.GetSubjectByUID(UID);

                    dgv.Rows.Add(score, UID, subject);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating DataGridView: " + ex.Message);
            }
        }
        #endregion
        #region DataViewer
        /// <summary>
        /// Handles selection in the Data Viewer TreeView.
        /// If a table node is selected (EmailEmbeddings or Queries), loads its data into the grid.
        /// </summary>
        private void TVDataViewer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            if (selectedNode.Parent == null || selectedNode.Parent.Text == "Tables")
            {
                string tableName = selectedNode.Text;
                LoadDataIntoGrid(tableName);
            }
        }
        /// <summary>
        /// Handles the Delete button click in the Data Viewer.
        /// Deletes the selected record from either EmailEmbeddings or Queries,
        /// based on the selected node in the TreeView.
        /// </summary>
        private void btnDVDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (TVDataViewer.SelectedNode == null) return;
                if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.No) return;
                int rowIndex = dgvDataViewer.CurrentCell.RowIndex;
                string connString = $"Data Source={GVar.PathDB}";

                //EmailEmbeddings
                if (TVDataViewer.SelectedNode.Text == "EmailEmbeddings")
                {
                    string uid = dgvDataViewer[0, rowIndex].Value.ToString() ?? "-1";
                    using SqliteConnection conn = new SqliteConnection(connString);
                    conn.Open();
                    string xDeleteCmd = @"DELETE FROM EmailEmbeddings WHERE UID = @UID;";
                    using SqliteCommand cmd = new SqliteCommand(xDeleteCmd, conn);
                    cmd.Parameters.AddWithValue("@UID", uid);

                    int xRowsAffected = cmd.ExecuteNonQuery();

                    if (xRowsAffected > 0)
                    {
                        MessageBox.Show($"Email Embedding deleted with UID: {uid}");
                    }
                    else
                    {
                        MessageBox.Show("Delete failed. Record not found.");
                    }
                    LoadDataIntoGrid("EmailEmbeddings");
                }
                else if (TVDataViewer.SelectedNode.Text == "Queries")
                {
                    string xID = dgvDataViewer[0, rowIndex].Value.ToString() ?? "-1";
                    if (string.IsNullOrEmpty(xID)) { return; }
                    using SqliteConnection conn = new SqliteConnection(connString);
                    conn.Open();
                    string xDeleteCmd = @"DELETE FROM QUERIES WHERE ID = @ID;";
                    using SqliteCommand cmd = new SqliteCommand(xDeleteCmd, conn);
                    cmd.Parameters.AddWithValue("@ID", xID);

                    int xRowsAffected = cmd.ExecuteNonQuery();

                    if (xRowsAffected > 0)
                    {
                        MessageBox.Show($"Query deleted with ID: {xID}");
                    }
                    else
                    {
                        MessageBox.Show("Delete failed. Record not found.");
                    }
                    LoadDataIntoGrid("Queries");
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Loads the data from the specified table (EmailEmbeddings or Queries)
        /// into the DataGridView for inspection. Includes embeddings if available.
        /// </summary>
        public void LoadDataIntoGrid(string tableName)
        {
            try
            {
                string query = "";
                string connString = "Data Source=" + GVar.PathDB;
                if (tableName == "EmailEmbeddings")
                {
                    // if (!cbDataViewerEmbeddings.Checked)
                    //{
                    //    query = $"SELECT UID, Date_yyyyMMdd AS [Date], Sender, Subject, EmailContent AS [Email Content],Attachments FROM [{tableName}] ORDER BY UID";
                    //}
                    //else
                    //{
                    query = $"SELECT UID, Date_yyyyMMdd AS [Date], Sender, Subject, EmailContent AS [Email Content],Attachments, Embedding FROM [{tableName}] ORDER BY UID";
                    //}
                }
                else if (tableName == "Queries")
                {
                    //if (!cbDataViewerEmbeddings.Checked)
                    //{
                    //    query = $"SELECT ID, ShortName as [Short Name], Description FROM [{tableName}]";
                    //}
                    //else
                    //{
                    query = $"SELECT ID, ShortName as [Short Name], Description, QEmbedding AS [Embedding] FROM [{tableName}]";
                    //}
                }
                else
                {
                    MessageBox.Show("Unknown table name.");
                    return;
                }
                using SqliteConnection conn = new SqliteConnection(connString);
                conn.Open();

                using SqliteCommand cmd = new SqliteCommand(query, conn);
                using SqliteDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader); // This loads the data from reader into the DataTable

                dgvDataViewer.DataSource = dt;
            }
            catch
            {
            }
        }
        /// <summary>
        /// Clears the current selection in the Data Viewer TreeView
        /// when the "Show Embeddings" checkbox is toggled.
        /// </summary>
        private void cbDataViewerEmbeddings_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TVDataViewer.SelectedNode = null;
            }
            catch
            {
            }
        }
        #endregion

        #region Tab and TreeNode 
        /// <summary>
        /// Handles tab switching in the main TabControl.
        /// Initializes content dynamically for each tab when selected
        /// (downloads, embeddings, queries, finder, and data viewer).
        /// </summary>
        private void TC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (TC.SelectedTab == tabSettings)
                {
                }
                else if (TC.SelectedTab == tabDownload)
                {
                    BaseFunctions.PopMonthFolders(TVDownload);
                }
                else if (TC.SelectedTab == tabEmbed)
                {
                    BaseFunctions.PopMonthFolders(TVEmbedMonths);
                }
                else if (TC.SelectedTab == tabQuery)
                {
                    Queries.PopTV(TVQuery, GVar.PathDB);
                    txtQueryShortName.Clear();
                    txtQueryDescription.Clear();
                }
                else if (TC.SelectedTab == tabFinder)
                {
                    Queries.PopTV(TVFinderQueries, GVar.PathDB);
                }
                else if (TC.SelectedTab == tabDataViewer)
                {
                    MySQLite.LoadSchema(TVDataViewer);
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Custom draw handlers for TreeViews across tabs.
        /// Delegates rendering to BaseFunctions.TVDrawNode for consistent styling.
        /// </summary>
        private void TVDownload_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            try
            {
                BaseFunctions.TVDrawNode(TVDownload, e);
            }
            catch
            {
            }
        }
        private void TVEmbedMonths_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            try
            {
                BaseFunctions.TVDrawNode(TVEmbedMonths, e);
            }
            catch
            {
            }
        }
        private void TVEmbed_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            try
            {
                BaseFunctions.TVDrawNode(TVEmbed, e);
            }
            catch
            {
            }
        }
        private void TVQuery_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            try
            {
                BaseFunctions.TVDrawNode(TVQuery, e);
            }
            catch
            {
            }
        }
        private void TVDataViewer_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            try
            {
                BaseFunctions.TVDrawNode(TVDataViewer, e);
            }
            catch
            {
            }
        }
        private void TVFinderQueries_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            try
            {
                BaseFunctions.TVDrawNode(TVFinderQueries, e);
            }
            catch
            {
            }
        }
        /// <summary>
        /// Dynamically adds or removes application tabs based on login state.
        /// Tabs are enabled when connected to email, and removed otherwise.
        /// </summary>
        private void AddRemoveTabs(bool TrueFalse)
        {
            try
            {
                if (TrueFalse)
                {
                    if (!TC.TabPages.Contains(tabDownload)) TC.TabPages.Add(tabDownload);
                    if (!TC.TabPages.Contains(tabEmbed)) TC.TabPages.Add(tabEmbed);
                    if (!TC.TabPages.Contains(tabQuery)) TC.TabPages.Add(tabQuery);
                    if (!TC.TabPages.Contains(tabFinder)) TC.TabPages.Add(tabFinder);
                    if (!TC.TabPages.Contains(tabDataViewer)) TC.TabPages.Add(tabDataViewer);
                }
                else
                {
                    if (TC.TabPages.Contains(tabDownload)) TC.TabPages.Remove(tabDownload);
                    if (TC.TabPages.Contains(tabEmbed)) TC.TabPages.Remove(tabEmbed);
                    if (TC.TabPages.Contains(tabQuery)) TC.TabPages.Remove(tabQuery);
                    if (TC.TabPages.Contains(tabFinder)) TC.TabPages.Remove(tabFinder);
                    if (TC.TabPages.Contains(tabDataViewer)) TC.TabPages.Remove(tabDataViewer);
                }
            }
            catch { }
        }
        /// <summary>
        /// Highlights TreeView nodes in the Embed tab by marking emails not yet embedded in red.
        /// Compares UIDs of embedded emails from the database against the TreeView nodes.
        /// </summary>
        private void HighligtEmbedded(TreeNode T)
        {
            try
            {
                List<string> LstEmbedded = MySQLite.GetEmbeddedEmails(T.Text);
                foreach (TreeNode T1 in T.Nodes)
                {
                    if (!LstEmbedded.Contains(T1.Name))
                    {
                        T1.ForeColor = Color.Red;
                    }
                }
            }
            catch { }
        }
        #endregion
    }
}