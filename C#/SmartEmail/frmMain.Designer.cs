namespace SmartEmail
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TC = new TabControl();
            tabSettings = new TabPage();
            splitContainer1 = new SplitContainer();
            chkSettingsEmailSave = new CheckBox();
            ChkSettingsPwdShow = new CheckBox();
            lblSettingsEmailProvider = new Label();
            cmbSettingsEmailProvider = new ComboBox();
            btnSettingsEmailConnect = new Button();
            txtSettingsPassword = new TextBox();
            lblPassword = new Label();
            txtSettingsEmail = new TextBox();
            lblEmail = new Label();
            tabDownload = new TabPage();
            splitContainer2 = new SplitContainer();
            dateTimePicker1 = new DateTimePicker();
            chkDownloadKeyWords = new CheckBox();
            txtDownloadKeyWords = new TextBox();
            lblDownloadKeyWords = new Label();
            btnDownloadDownloadEmails = new Button();
            TVDownload = new TreeView();
            tabEmbed = new TabPage();
            splitContainer3 = new SplitContainer();
            btnEmbedEmbedEmails = new Button();
            splitContainer5 = new SplitContainer();
            TVEmbedMonths = new TreeView();
            TVEmbed = new TreeView();
            tabQuery = new TabPage();
            splitContainer6 = new SplitContainer();
            btnQueryUpdate = new Button();
            btnQueryDelete = new Button();
            btnQueryAdd = new Button();
            splitContainer7 = new SplitContainer();
            TVQuery = new TreeView();
            label3 = new Label();
            label2 = new Label();
            txtQueryDescription = new TextBox();
            txtQueryShortName = new TextBox();
            tabFinder = new TabPage();
            splitContainer10 = new SplitContainer();
            btnFinderFind = new Button();
            splitContainer11 = new SplitContainer();
            TVFinderQueries = new TreeView();
            dgvFinder = new DataGridView();
            tabDataViewer = new TabPage();
            splitContainer8 = new SplitContainer();
            btnDVDelete = new Button();
            label4 = new Label();
            splitContainer9 = new SplitContainer();
            TVDataViewer = new TreeView();
            dgvDataViewer = new DataGridView();
            TC.SuspendLayout();
            tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabDownload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            tabEmbed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer5).BeginInit();
            splitContainer5.Panel1.SuspendLayout();
            splitContainer5.Panel2.SuspendLayout();
            splitContainer5.SuspendLayout();
            tabQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer6).BeginInit();
            splitContainer6.Panel1.SuspendLayout();
            splitContainer6.Panel2.SuspendLayout();
            splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer7).BeginInit();
            splitContainer7.Panel1.SuspendLayout();
            splitContainer7.Panel2.SuspendLayout();
            splitContainer7.SuspendLayout();
            tabFinder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer10).BeginInit();
            splitContainer10.Panel1.SuspendLayout();
            splitContainer10.Panel2.SuspendLayout();
            splitContainer10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer11).BeginInit();
            splitContainer11.Panel1.SuspendLayout();
            splitContainer11.Panel2.SuspendLayout();
            splitContainer11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFinder).BeginInit();
            tabDataViewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer8).BeginInit();
            splitContainer8.Panel1.SuspendLayout();
            splitContainer8.Panel2.SuspendLayout();
            splitContainer8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer9).BeginInit();
            splitContainer9.Panel1.SuspendLayout();
            splitContainer9.Panel2.SuspendLayout();
            splitContainer9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDataViewer).BeginInit();
            SuspendLayout();
            // 
            // TC
            // 
            TC.Controls.Add(tabSettings);
            TC.Controls.Add(tabDownload);
            TC.Controls.Add(tabEmbed);
            TC.Controls.Add(tabQuery);
            TC.Controls.Add(tabFinder);
            TC.Controls.Add(tabDataViewer);
            TC.Dock = DockStyle.Fill;
            TC.Location = new Point(5, 5);
            TC.Name = "TC";
            TC.SelectedIndex = 0;
            TC.Size = new Size(923, 517);
            TC.TabIndex = 0;
            TC.SelectedIndexChanged += TC_SelectedIndexChanged;
            // 
            // tabSettings
            // 
            tabSettings.BorderStyle = BorderStyle.FixedSingle;
            tabSettings.Controls.Add(splitContainer1);
            tabSettings.Location = new Point(4, 24);
            tabSettings.Name = "tabSettings";
            tabSettings.Padding = new Padding(3);
            tabSettings.Size = new Size(915, 489);
            tabSettings.TabIndex = 0;
            tabSettings.Text = "Settings";
            tabSettings.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(chkSettingsEmailSave);
            splitContainer1.Panel1.Controls.Add(ChkSettingsPwdShow);
            splitContainer1.Panel1.Controls.Add(lblSettingsEmailProvider);
            splitContainer1.Panel1.Controls.Add(cmbSettingsEmailProvider);
            splitContainer1.Panel1.Controls.Add(btnSettingsEmailConnect);
            splitContainer1.Panel1.Controls.Add(txtSettingsPassword);
            splitContainer1.Panel1.Controls.Add(lblPassword);
            splitContainer1.Panel1.Controls.Add(txtSettingsEmail);
            splitContainer1.Panel1.Controls.Add(lblEmail);
            splitContainer1.Size = new Size(907, 481);
            splitContainer1.SplitterDistance = 80;
            splitContainer1.TabIndex = 0;
            // 
            // chkSettingsEmailSave
            // 
            chkSettingsEmailSave.AutoSize = true;
            chkSettingsEmailSave.CheckAlign = ContentAlignment.MiddleRight;
            chkSettingsEmailSave.Location = new Point(499, 53);
            chkSettingsEmailSave.Name = "chkSettingsEmailSave";
            chkSettingsEmailSave.RightToLeft = RightToLeft.Yes;
            chkSettingsEmailSave.Size = new Size(50, 19);
            chkSettingsEmailSave.TabIndex = 5;
            chkSettingsEmailSave.Text = "Save";
            chkSettingsEmailSave.UseVisualStyleBackColor = true;
            chkSettingsEmailSave.CheckedChanged += chkSettingsEmailSave_CheckedChanged;
            // 
            // ChkSettingsPwdShow
            // 
            ChkSettingsPwdShow.AutoSize = true;
            ChkSettingsPwdShow.CheckAlign = ContentAlignment.MiddleRight;
            ChkSettingsPwdShow.Location = new Point(352, 52);
            ChkSettingsPwdShow.Name = "ChkSettingsPwdShow";
            ChkSettingsPwdShow.RightToLeft = RightToLeft.Yes;
            ChkSettingsPwdShow.Size = new Size(55, 19);
            ChkSettingsPwdShow.TabIndex = 0;
            ChkSettingsPwdShow.Text = "Show";
            ChkSettingsPwdShow.UseVisualStyleBackColor = true;
            ChkSettingsPwdShow.CheckedChanged += ChkSettingsPwdShow_CheckedChanged;
            // 
            // lblSettingsEmailProvider
            // 
            lblSettingsEmailProvider.AutoSize = true;
            lblSettingsEmailProvider.Location = new Point(561, 9);
            lblSettingsEmailProvider.Name = "lblSettingsEmailProvider";
            lblSettingsEmailProvider.Size = new Size(83, 15);
            lblSettingsEmailProvider.TabIndex = 4;
            lblSettingsEmailProvider.Text = "Email Provider";
            // 
            // cmbSettingsEmailProvider
            // 
            cmbSettingsEmailProvider.FormattingEnabled = true;
            cmbSettingsEmailProvider.Location = new Point(558, 27);
            cmbSettingsEmailProvider.Name = "cmbSettingsEmailProvider";
            cmbSettingsEmailProvider.Size = new Size(204, 23);
            cmbSettingsEmailProvider.TabIndex = 0;
            // 
            // btnSettingsEmailConnect
            // 
            btnSettingsEmailConnect.Location = new Point(768, 27);
            btnSettingsEmailConnect.Name = "btnSettingsEmailConnect";
            btnSettingsEmailConnect.Size = new Size(107, 23);
            btnSettingsEmailConnect.TabIndex = 0;
            btnSettingsEmailConnect.Text = "Connect";
            btnSettingsEmailConnect.UseVisualStyleBackColor = true;
            btnSettingsEmailConnect.Click += btnSettingsEmailConnect_Click;
            // 
            // txtSettingsPassword
            // 
            txtSettingsPassword.Location = new Point(348, 27);
            txtSettingsPassword.Name = "txtSettingsPassword";
            txtSettingsPassword.PasswordChar = '*';
            txtSettingsPassword.Size = new Size(204, 23);
            txtSettingsPassword.TabIndex = 3;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(351, 9);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Password";
            // 
            // txtSettingsEmail
            // 
            txtSettingsEmail.Location = new Point(12, 27);
            txtSettingsEmail.Name = "txtSettingsEmail";
            txtSettingsEmail.Size = new Size(330, 23);
            txtSettingsEmail.TabIndex = 1;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(15, 9);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(36, 15);
            lblEmail.TabIndex = 0;
            lblEmail.Text = "Email";
            // 
            // tabDownload
            // 
            tabDownload.BorderStyle = BorderStyle.FixedSingle;
            tabDownload.Controls.Add(splitContainer2);
            tabDownload.Location = new Point(4, 24);
            tabDownload.Name = "tabDownload";
            tabDownload.Padding = new Padding(3);
            tabDownload.Size = new Size(915, 489);
            tabDownload.TabIndex = 1;
            tabDownload.Text = "Download";
            tabDownload.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            splitContainer2.BorderStyle = BorderStyle.FixedSingle;
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(3, 3);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(dateTimePicker1);
            splitContainer2.Panel1.Controls.Add(chkDownloadKeyWords);
            splitContainer2.Panel1.Controls.Add(txtDownloadKeyWords);
            splitContainer2.Panel1.Controls.Add(lblDownloadKeyWords);
            splitContainer2.Panel1.Controls.Add(btnDownloadDownloadEmails);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(TVDownload);
            splitContainer2.Size = new Size(907, 481);
            splitContainer2.SplitterDistance = 80;
            splitContainer2.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(517, 27);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 9;
            // 
            // chkDownloadKeyWords
            // 
            chkDownloadKeyWords.AutoSize = true;
            chkDownloadKeyWords.CheckAlign = ContentAlignment.MiddleRight;
            chkDownloadKeyWords.Location = new Point(458, 52);
            chkDownloadKeyWords.Name = "chkDownloadKeyWords";
            chkDownloadKeyWords.RightToLeft = RightToLeft.Yes;
            chkDownloadKeyWords.Size = new Size(50, 19);
            chkDownloadKeyWords.TabIndex = 8;
            chkDownloadKeyWords.Text = "Save";
            chkDownloadKeyWords.UseVisualStyleBackColor = true;
            chkDownloadKeyWords.CheckedChanged += chkDownloadKeyWords_CheckedChanged;
            // 
            // txtDownloadKeyWords
            // 
            txtDownloadKeyWords.Location = new Point(12, 27);
            txtDownloadKeyWords.Name = "txtDownloadKeyWords";
            txtDownloadKeyWords.Size = new Size(499, 23);
            txtDownloadKeyWords.TabIndex = 7;
            // 
            // lblDownloadKeyWords
            // 
            lblDownloadKeyWords.AutoSize = true;
            lblDownloadKeyWords.Location = new Point(15, 10);
            lblDownloadKeyWords.Name = "lblDownloadKeyWords";
            lblDownloadKeyWords.Size = new Size(201, 15);
            lblDownloadKeyWords.TabIndex = 6;
            lblDownloadKeyWords.Text = "Key Words (keep blank for all emails)";
            // 
            // btnDownloadDownloadEmails
            // 
            btnDownloadDownloadEmails.Location = new Point(723, 27);
            btnDownloadDownloadEmails.Name = "btnDownloadDownloadEmails";
            btnDownloadDownloadEmails.Size = new Size(132, 23);
            btnDownloadDownloadEmails.TabIndex = 5;
            btnDownloadDownloadEmails.Text = "Download Emails";
            btnDownloadDownloadEmails.UseVisualStyleBackColor = true;
            btnDownloadDownloadEmails.Click += btnDownloadDownloadEmails_Click;
            // 
            // TVDownload
            // 
            TVDownload.Dock = DockStyle.Fill;
            TVDownload.DrawMode = TreeViewDrawMode.OwnerDrawText;
            TVDownload.HideSelection = false;
            TVDownload.Location = new Point(0, 0);
            TVDownload.Name = "TVDownload";
            TVDownload.Size = new Size(905, 395);
            TVDownload.TabIndex = 1;
            TVDownload.DrawNode += TVDownload_DrawNode;
            TVDownload.AfterSelect += TVDownload_AfterSelect;
            // 
            // tabEmbed
            // 
            tabEmbed.Controls.Add(splitContainer3);
            tabEmbed.Location = new Point(4, 24);
            tabEmbed.Name = "tabEmbed";
            tabEmbed.Size = new Size(915, 489);
            tabEmbed.TabIndex = 2;
            tabEmbed.Text = "Embed";
            tabEmbed.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            splitContainer3.BorderStyle = BorderStyle.FixedSingle;
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.FixedPanel = FixedPanel.Panel1;
            splitContainer3.IsSplitterFixed = true;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(btnEmbedEmbedEmails);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(splitContainer5);
            splitContainer3.Size = new Size(915, 489);
            splitContainer3.SplitterDistance = 80;
            splitContainer3.TabIndex = 0;
            // 
            // btnEmbedEmbedEmails
            // 
            btnEmbedEmbedEmails.Location = new Point(12, 27);
            btnEmbedEmbedEmails.Name = "btnEmbedEmbedEmails";
            btnEmbedEmbedEmails.Size = new Size(75, 23);
            btnEmbedEmbedEmails.TabIndex = 0;
            btnEmbedEmbedEmails.Text = "Embed Emails";
            btnEmbedEmbedEmails.UseVisualStyleBackColor = true;
            btnEmbedEmbedEmails.Click += btnEmbedEmbedEmails_Click;
            // 
            // splitContainer5
            // 
            splitContainer5.BorderStyle = BorderStyle.FixedSingle;
            splitContainer5.Dock = DockStyle.Fill;
            splitContainer5.FixedPanel = FixedPanel.Panel1;
            splitContainer5.IsSplitterFixed = true;
            splitContainer5.Location = new Point(0, 0);
            splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            splitContainer5.Panel1.Controls.Add(TVEmbedMonths);
            // 
            // splitContainer5.Panel2
            // 
            splitContainer5.Panel2.Controls.Add(TVEmbed);
            splitContainer5.Size = new Size(915, 405);
            splitContainer5.SplitterDistance = 300;
            splitContainer5.TabIndex = 0;
            // 
            // TVEmbedMonths
            // 
            TVEmbedMonths.Dock = DockStyle.Fill;
            TVEmbedMonths.DrawMode = TreeViewDrawMode.OwnerDrawText;
            TVEmbedMonths.HideSelection = false;
            TVEmbedMonths.Location = new Point(0, 0);
            TVEmbedMonths.Name = "TVEmbedMonths";
            TVEmbedMonths.Size = new Size(298, 403);
            TVEmbedMonths.TabIndex = 0;
            TVEmbedMonths.DrawNode += TVEmbedMonths_DrawNode;
            TVEmbedMonths.AfterSelect += TVEmbedMonths_AfterSelect;
            // 
            // TVEmbed
            // 
            TVEmbed.Dock = DockStyle.Fill;
            TVEmbed.DrawMode = TreeViewDrawMode.OwnerDrawText;
            TVEmbed.HideSelection = false;
            TVEmbed.Location = new Point(0, 0);
            TVEmbed.Name = "TVEmbed";
            TVEmbed.Size = new Size(609, 403);
            TVEmbed.TabIndex = 0;
            TVEmbed.DrawNode += TVEmbed_DrawNode;
            // 
            // tabQuery
            // 
            tabQuery.Controls.Add(splitContainer6);
            tabQuery.Location = new Point(4, 24);
            tabQuery.Name = "tabQuery";
            tabQuery.Size = new Size(915, 489);
            tabQuery.TabIndex = 3;
            tabQuery.Text = "Query";
            tabQuery.UseVisualStyleBackColor = true;
            // 
            // splitContainer6
            // 
            splitContainer6.BorderStyle = BorderStyle.FixedSingle;
            splitContainer6.Dock = DockStyle.Fill;
            splitContainer6.FixedPanel = FixedPanel.Panel1;
            splitContainer6.IsSplitterFixed = true;
            splitContainer6.Location = new Point(0, 0);
            splitContainer6.Name = "splitContainer6";
            splitContainer6.Orientation = Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            splitContainer6.Panel1.Controls.Add(btnQueryUpdate);
            splitContainer6.Panel1.Controls.Add(btnQueryDelete);
            splitContainer6.Panel1.Controls.Add(btnQueryAdd);
            // 
            // splitContainer6.Panel2
            // 
            splitContainer6.Panel2.Controls.Add(splitContainer7);
            splitContainer6.Size = new Size(915, 489);
            splitContainer6.SplitterDistance = 80;
            splitContainer6.TabIndex = 0;
            // 
            // btnQueryUpdate
            // 
            btnQueryUpdate.Location = new Point(148, 27);
            btnQueryUpdate.Name = "btnQueryUpdate";
            btnQueryUpdate.Size = new Size(75, 23);
            btnQueryUpdate.TabIndex = 2;
            btnQueryUpdate.Text = "Update";
            btnQueryUpdate.UseVisualStyleBackColor = true;
            btnQueryUpdate.Click += btnQueryUpdate_Click;
            // 
            // btnQueryDelete
            // 
            btnQueryDelete.Location = new Point(229, 27);
            btnQueryDelete.Name = "btnQueryDelete";
            btnQueryDelete.Size = new Size(75, 23);
            btnQueryDelete.TabIndex = 1;
            btnQueryDelete.Text = "Delete";
            btnQueryDelete.UseVisualStyleBackColor = true;
            btnQueryDelete.Click += btnQueryDelete_Click;
            // 
            // btnQueryAdd
            // 
            btnQueryAdd.Location = new Point(12, 27);
            btnQueryAdd.Name = "btnQueryAdd";
            btnQueryAdd.Size = new Size(130, 23);
            btnQueryAdd.TabIndex = 0;
            btnQueryAdd.Text = "Add + Embed";
            btnQueryAdd.UseVisualStyleBackColor = true;
            btnQueryAdd.Click += btnQueryAdd_Click;
            // 
            // splitContainer7
            // 
            splitContainer7.BorderStyle = BorderStyle.FixedSingle;
            splitContainer7.Dock = DockStyle.Fill;
            splitContainer7.FixedPanel = FixedPanel.Panel1;
            splitContainer7.IsSplitterFixed = true;
            splitContainer7.Location = new Point(0, 0);
            splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            splitContainer7.Panel1.Controls.Add(TVQuery);
            // 
            // splitContainer7.Panel2
            // 
            splitContainer7.Panel2.Controls.Add(label3);
            splitContainer7.Panel2.Controls.Add(label2);
            splitContainer7.Panel2.Controls.Add(txtQueryDescription);
            splitContainer7.Panel2.Controls.Add(txtQueryShortName);
            splitContainer7.Size = new Size(915, 405);
            splitContainer7.SplitterDistance = 300;
            splitContainer7.TabIndex = 0;
            // 
            // TVQuery
            // 
            TVQuery.Dock = DockStyle.Fill;
            TVQuery.DrawMode = TreeViewDrawMode.OwnerDrawText;
            TVQuery.HideSelection = false;
            TVQuery.Location = new Point(0, 0);
            TVQuery.Name = "TVQuery";
            TVQuery.Size = new Size(298, 403);
            TVQuery.TabIndex = 0;
            TVQuery.DrawNode += TVQuery_DrawNode;
            TVQuery.MouseDoubleClick += TVQuery_MouseDoubleClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 130);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 3;
            label3.Text = "Description";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 44);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 2;
            label2.Text = "Short Name";
            // 
            // txtQueryDescription
            // 
            txtQueryDescription.Location = new Point(17, 147);
            txtQueryDescription.Multiline = true;
            txtQueryDescription.Name = "txtQueryDescription";
            txtQueryDescription.Size = new Size(570, 59);
            txtQueryDescription.TabIndex = 1;
            // 
            // txtQueryShortName
            // 
            txtQueryShortName.Location = new Point(17, 61);
            txtQueryShortName.Name = "txtQueryShortName";
            txtQueryShortName.Size = new Size(570, 23);
            txtQueryShortName.TabIndex = 0;
            // 
            // tabFinder
            // 
            tabFinder.BorderStyle = BorderStyle.FixedSingle;
            tabFinder.Controls.Add(splitContainer10);
            tabFinder.Location = new Point(4, 24);
            tabFinder.Name = "tabFinder";
            tabFinder.Padding = new Padding(5);
            tabFinder.Size = new Size(915, 489);
            tabFinder.TabIndex = 5;
            tabFinder.Text = "Finder";
            tabFinder.UseVisualStyleBackColor = true;
            // 
            // splitContainer10
            // 
            splitContainer10.BorderStyle = BorderStyle.FixedSingle;
            splitContainer10.Dock = DockStyle.Fill;
            splitContainer10.FixedPanel = FixedPanel.Panel1;
            splitContainer10.IsSplitterFixed = true;
            splitContainer10.Location = new Point(5, 5);
            splitContainer10.Name = "splitContainer10";
            splitContainer10.Orientation = Orientation.Horizontal;
            // 
            // splitContainer10.Panel1
            // 
            splitContainer10.Panel1.Controls.Add(btnFinderFind);
            // 
            // splitContainer10.Panel2
            // 
            splitContainer10.Panel2.Controls.Add(splitContainer11);
            splitContainer10.Size = new Size(903, 477);
            splitContainer10.SplitterDistance = 80;
            splitContainer10.TabIndex = 0;
            // 
            // btnFinderFind
            // 
            btnFinderFind.Location = new Point(12, 27);
            btnFinderFind.Name = "btnFinderFind";
            btnFinderFind.Size = new Size(75, 23);
            btnFinderFind.TabIndex = 0;
            btnFinderFind.Text = "Find";
            btnFinderFind.UseVisualStyleBackColor = true;
            btnFinderFind.Click += btnFinderFind_Click;
            // 
            // splitContainer11
            // 
            splitContainer11.BorderStyle = BorderStyle.FixedSingle;
            splitContainer11.Dock = DockStyle.Fill;
            splitContainer11.FixedPanel = FixedPanel.Panel1;
            splitContainer11.IsSplitterFixed = true;
            splitContainer11.Location = new Point(0, 0);
            splitContainer11.Name = "splitContainer11";
            // 
            // splitContainer11.Panel1
            // 
            splitContainer11.Panel1.Controls.Add(TVFinderQueries);
            // 
            // splitContainer11.Panel2
            // 
            splitContainer11.Panel2.Controls.Add(dgvFinder);
            splitContainer11.Size = new Size(903, 393);
            splitContainer11.SplitterDistance = 300;
            splitContainer11.TabIndex = 0;
            // 
            // TVFinderQueries
            // 
            TVFinderQueries.Dock = DockStyle.Fill;
            TVFinderQueries.DrawMode = TreeViewDrawMode.OwnerDrawText;
            TVFinderQueries.HideSelection = false;
            TVFinderQueries.Location = new Point(0, 0);
            TVFinderQueries.Name = "TVFinderQueries";
            TVFinderQueries.Size = new Size(298, 391);
            TVFinderQueries.TabIndex = 0;
            TVFinderQueries.DrawNode += TVFinderQueries_DrawNode;
            // 
            // dgvFinder
            // 
            dgvFinder.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFinder.Dock = DockStyle.Fill;
            dgvFinder.Location = new Point(0, 0);
            dgvFinder.Name = "dgvFinder";
            dgvFinder.Size = new Size(597, 391);
            dgvFinder.TabIndex = 0;
            // 
            // tabDataViewer
            // 
            tabDataViewer.BorderStyle = BorderStyle.FixedSingle;
            tabDataViewer.Controls.Add(splitContainer8);
            tabDataViewer.Location = new Point(4, 24);
            tabDataViewer.Name = "tabDataViewer";
            tabDataViewer.Padding = new Padding(5);
            tabDataViewer.Size = new Size(915, 489);
            tabDataViewer.TabIndex = 4;
            tabDataViewer.Text = "Data Viewer";
            tabDataViewer.UseVisualStyleBackColor = true;
            // 
            // splitContainer8
            // 
            splitContainer8.BorderStyle = BorderStyle.FixedSingle;
            splitContainer8.Dock = DockStyle.Fill;
            splitContainer8.FixedPanel = FixedPanel.Panel1;
            splitContainer8.IsSplitterFixed = true;
            splitContainer8.Location = new Point(5, 5);
            splitContainer8.Name = "splitContainer8";
            splitContainer8.Orientation = Orientation.Horizontal;
            // 
            // splitContainer8.Panel1
            // 
            splitContainer8.Panel1.Controls.Add(btnDVDelete);
            splitContainer8.Panel1.Controls.Add(label4);
            // 
            // splitContainer8.Panel2
            // 
            splitContainer8.Panel2.Controls.Add(splitContainer9);
            splitContainer8.Size = new Size(903, 477);
            splitContainer8.SplitterDistance = 80;
            splitContainer8.TabIndex = 0;
            // 
            // btnDVDelete
            // 
            btnDVDelete.Location = new Point(814, 23);
            btnDVDelete.Name = "btnDVDelete";
            btnDVDelete.Size = new Size(75, 23);
            btnDVDelete.TabIndex = 2;
            btnDVDelete.Text = "Delete";
            btnDVDelete.UseVisualStyleBackColor = true;
            btnDVDelete.Click += btnDVDelete_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(304, 22);
            label4.Name = "label4";
            label4.Size = new Size(311, 25);
            label4.TabIndex = 1;
            label4.Text = "Database Viewer for Administration";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // splitContainer9
            // 
            splitContainer9.BorderStyle = BorderStyle.FixedSingle;
            splitContainer9.Dock = DockStyle.Fill;
            splitContainer9.FixedPanel = FixedPanel.Panel1;
            splitContainer9.IsSplitterFixed = true;
            splitContainer9.Location = new Point(0, 0);
            splitContainer9.Name = "splitContainer9";
            // 
            // splitContainer9.Panel1
            // 
            splitContainer9.Panel1.Controls.Add(TVDataViewer);
            // 
            // splitContainer9.Panel2
            // 
            splitContainer9.Panel2.Controls.Add(dgvDataViewer);
            splitContainer9.Size = new Size(903, 393);
            splitContainer9.SplitterDistance = 300;
            splitContainer9.TabIndex = 0;
            // 
            // TVDataViewer
            // 
            TVDataViewer.Dock = DockStyle.Fill;
            TVDataViewer.DrawMode = TreeViewDrawMode.OwnerDrawText;
            TVDataViewer.HideSelection = false;
            TVDataViewer.Location = new Point(0, 0);
            TVDataViewer.Name = "TVDataViewer";
            TVDataViewer.Size = new Size(298, 391);
            TVDataViewer.TabIndex = 0;
            TVDataViewer.DrawNode += TVDataViewer_DrawNode;
            TVDataViewer.AfterSelect += TVDataViewer_AfterSelect;
            // 
            // dgvDataViewer
            // 
            dgvDataViewer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDataViewer.Dock = DockStyle.Fill;
            dgvDataViewer.Location = new Point(0, 0);
            dgvDataViewer.Name = "dgvDataViewer";
            dgvDataViewer.Size = new Size(597, 391);
            dgvDataViewer.TabIndex = 0;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 527);
            Controls.Add(TC);
            Name = "frmMain";
            Padding = new Padding(5);
            Text = "Smart Inbox AI by Gayatri Chougule";
            Load += frmMain_Load;
            TC.ResumeLayout(false);
            tabSettings.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabDownload.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            tabEmbed.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            splitContainer5.Panel1.ResumeLayout(false);
            splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer5).EndInit();
            splitContainer5.ResumeLayout(false);
            tabQuery.ResumeLayout(false);
            splitContainer6.Panel1.ResumeLayout(false);
            splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer6).EndInit();
            splitContainer6.ResumeLayout(false);
            splitContainer7.Panel1.ResumeLayout(false);
            splitContainer7.Panel2.ResumeLayout(false);
            splitContainer7.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer7).EndInit();
            splitContainer7.ResumeLayout(false);
            tabFinder.ResumeLayout(false);
            splitContainer10.Panel1.ResumeLayout(false);
            splitContainer10.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer10).EndInit();
            splitContainer10.ResumeLayout(false);
            splitContainer11.Panel1.ResumeLayout(false);
            splitContainer11.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer11).EndInit();
            splitContainer11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvFinder).EndInit();
            tabDataViewer.ResumeLayout(false);
            splitContainer8.Panel1.ResumeLayout(false);
            splitContainer8.Panel1.PerformLayout();
            splitContainer8.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer8).EndInit();
            splitContainer8.ResumeLayout(false);
            splitContainer9.Panel1.ResumeLayout(false);
            splitContainer9.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer9).EndInit();
            splitContainer9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDataViewer).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl TC;
        private TabPage tabSettings;
        private TabPage tabDownload;
        private SplitContainer splitContainer1;
        private Label lblEmail;
        private TextBox txtSettingsEmail;
        private TextBox txtSettingsPassword;
        private Label lblPassword;
        private Button btnSettingsEmailConnect;
        private Label lblSettingsEmailProvider;
        private ComboBox cmbSettingsEmailProvider;
        private CheckBox ChkSettingsPwdShow;
        private Button btnDownloadDownloadEmails;
        private SplitContainer splitContainer2;
        private TreeView TVDownload;
        private CheckBox chkSettingsEmailSave;
        private Label lblDownloadKeyWords;
        private CheckBox chkDownloadKeyWords;
        private TextBox txtDownloadKeyWords;
        private TabPage tabEmbed;
        private SplitContainer splitContainer3;
        private DateTimePicker dateTimePicker1;
        private SplitContainer splitContainer5;
        private Button btnEmbedEmbedEmails;
        private TreeView TVEmbed;
        private TabPage tabQuery;
        private SplitContainer splitContainer6;
        private SplitContainer splitContainer7;
        private TreeView TVQuery;
        private Button btnQueryAdd;
        private TextBox txtQueryDescription;
        private TextBox txtQueryShortName;
        private Label label3;
        private Label label2;
        private Button btnQueryDelete;
        private TabPage tabDataViewer;
        private SplitContainer splitContainer8;
        private Label label4;
        private SplitContainer splitContainer9;
        private TreeView TVDataViewer;
        private DataGridView dgvDataViewer;
        private TreeView TVEmbedMonths;
        private Button btnDVDelete;
        private TabPage tabFinder;
        private SplitContainer splitContainer10;
        private SplitContainer splitContainer11;
        private TreeView TVFinderQueries;
        private Button btnFinderFind;
        private Button btnQueryUpdate;
        private DataGridView dgvFinder;
    }
}
