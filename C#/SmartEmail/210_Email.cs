using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
//using System;
//using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
//using System.IO;
//using System.Windows.Forms;

namespace SmartEmail
{
    /// <summary>
    /// Provides methods for connecting to email servers, downloading messages,
    /// saving them locally, and populating UI controls with email data.
    /// Relies on MailKit/MimeKit for IMAP access and message handling.
    /// </summary>
    public static class Emails
    {
        static ImapClient mImapClient = new ImapClient();
        /// <summary>
        /// Maps a known email provider name to its IMAP server address.
        /// Returns an empty string if the provider is not supported.
        /// </summary>
        private static string GetImap(string xProviderName)
        {
            try
            {
                string retImap = string.Empty;

                if (xProviderName == "Gmail")
                    retImap = "imap.gmail.com";
                else if (xProviderName == "Outlook/Hotmail/Live (Microsoft)")
                    retImap = "imap-mail.outlook.com";
                else if (xProviderName == "Yahoo")
                    retImap = "imap.mail.yahoo.com";
                else if (xProviderName == "Rediffmail")
                    retImap = "imap.rediffmail.com";
                else if (xProviderName == "Zoho mail")
                    retImap = "imap.zoho.com";

                return retImap;
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Establishes a secure IMAP connection to the specified email provider
        /// using the provided email address and password.
        /// If already connected, returns false and shows a warning.
        /// </summary>
        public static Boolean Connect(string xEmailProvider, string xEmail, string xPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xEmailProvider))
                {
                    MessageBox.Show("Please select a valid email provider.");
                    return false;
                }
                string imapServer = GetImap(xEmailProvider);
                if (string.IsNullOrWhiteSpace(imapServer))
                {
                    MessageBox.Show("Unsupported email provider: " + xEmailProvider);
                    return false;
                }
                int port = 993;
                xEmail = xEmail.Trim();
                xPassword = xPassword.Trim();

                if (string.IsNullOrEmpty(xEmail) || string.IsNullOrEmpty(xPassword))
                {
                    MessageBox.Show("Please enter both email and password.");
                    return false;
                }
                if (mImapClient.IsConnected)
                {
                    MessageBox.Show("Connection already established");
                    return false;
                }
                mImapClient.Connect(imapServer, port, SecureSocketOptions.SslOnConnect);
                mImapClient.Authenticate(xEmail, xPassword);

                return true;
            }
            catch (Exception ex)
            {
                if (mImapClient.IsConnected)
                    mImapClient.Disconnect(true);
                MessageBox.Show("Connection failed: " + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Saves an email message to a local .eml file.
        /// The filename is built from the message date and UID to ensure uniqueness.
        /// If the file already exists, the method exits without overwriting.
        /// </summary>
        public static void SaveToFile(MimeMessage xMessage, UniqueId xUid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(GVar.MonthFolderName) || !Directory.Exists(GVar.MonthFolderName))
                {
                    MessageBox.Show("Email folder is not set or does not exist.");
                    return;
                }
                string timestamp = xMessage.Date.ToString("yyyyMMdd_HHmmss");
                string fileName = $"{timestamp}_{xUid}.eml";
                string fullPath = Path.Combine(GVar.MonthFolderName, fileName);

                if (File.Exists(fullPath))
                    return;
                using (var stream = File.Create(fullPath))
                {
                    xMessage.WriteTo(stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save email:\n" + ex.Message);
            }
        }
        /// <summary>
        /// Downloads emails for a specific date, optionally filtering by subject keywords.
        /// 
        /// Steps:
        /// 1. Searches the inbox for messages within the given date range.
        /// 2. Filters results if subject keywords are provided.
        /// 3. Skips emails already saved locally (via DownLoadCheck).
        /// 4. Saves new messages to disk and returns their metadata (sender, subject, date).
        /// </summary>
        public static List<EmailInfo> DownLoadByDateAndKeyWords(DateTime xDate, List<string> subjectKeyWords)
        {
            List<EmailInfo> lstEmails = new List<EmailInfo>();
            try
            {
                if (mImapClient == null || !mImapClient.IsAuthenticated)
                {
                    MessageBox.Show("Not connected to the email server.");
                    return lstEmails;
                }
                if (subjectKeyWords == null) subjectKeyWords = new List<string>();

                IMailFolder inBox = mImapClient.Inbox;
                inBox.Open(FolderAccess.ReadOnly);

                DateTime dayStart = xDate.Date;
                DateTime dayEnd = dayStart.AddDays(1);

                SearchQuery query = SearchQuery.DeliveredAfter(dayStart).And(SearchQuery.DeliveredBefore(dayEnd));
                List<UniqueId> uids = inBox.Search(query).ToList();

                List<IMessageSummary> summaries = inBox.Fetch(uids, MessageSummaryItems.Envelope).ToList();
                List<UniqueId> selectedUids = new List<UniqueId>();

                if (subjectKeyWords.Count > 0)
                {
                    foreach (IMessageSummary T in summaries)
                    {
                        string subject = T.Envelope.Subject ?? "(No Subject)";
                        if (subjectKeyWords.Any(p => subject.IndexOf(p, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            selectedUids.Add(T.UniqueId);
                        }
                    }
                }
                else
                {
                    selectedUids = uids;
                }
                selectedUids = DownLoadCheck(selectedUids);
                foreach (UniqueId T in selectedUids)
                {
                    MimeMessage message = inBox.GetMessage(T);
                    {
                        lstEmails.Add(new EmailInfo
                        {
                            Sender = message.From.ToString(),
                            Subject = message.Subject,
                            Date = message.Date.DateTime
                        });
                        SaveToFile(message, T);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving emails: " + ex.Message);
            }
            return lstEmails;
        }
        /// <summary>
        /// Filters out emails that are already downloaded.
        /// Checks local .eml files in the month folder and removes matching UIDs.
        /// Returns only the UIDs of messages that still need downloading.
        /// </summary>
        private static List<UniqueId> DownLoadCheck(List<UniqueId> xLst)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(GVar.MonthFolderName) || !Directory.Exists(GVar.MonthFolderName))
                {
                    MessageBox.Show("Month folder not set or does not exist.");
                    return xLst;
                }

                List<string> lst = Directory.GetFiles(GVar.MonthFolderName, "*.eml").ToList();
                lst = lst.Select(p => Path.GetFileNameWithoutExtension(p).Substring(16)).ToList();
                xLst.RemoveAll(p => lst.Contains(p.Id.ToString()));
                return xLst;
            }
            catch
            {
                return xLst;
            }
        }
        /// <summary>
        /// Populates a TreeView with locally saved email files for a given month.
        /// Each child node displays UID, date, sender, and subject (or a shorter format if requested).
        /// </summary>
        public static void PopTV(TreeView xTV, bool xIsShort = false)
        {
            try
            {
                if (xTV.SelectedNode == null || xTV.SelectedNode.Level == 1) return;

                string yyyyMM = xTV.SelectedNode.Text;
                string monthPath = Path.Combine(GVar.PathEmailDL, yyyyMM);

                if (!Directory.Exists(monthPath))
                {
                    MessageBox.Show("Email folder does not exist: " + monthPath);
                    return;
                }

                List<string> lstFiles = Directory.GetFiles(monthPath, "*.eml").ToList();
                TreeNode xNode = xTV.SelectedNode;
                xNode.Nodes.Clear();

                foreach (string fileNameWPath in lstFiles)
                {
                    MimeMessage message;
                    using (FileStream stream = File.OpenRead(fileNameWPath))
                    {
                        message = MimeMessage.Load(stream);
                        string sender = message.From.ToString();
                        string subject = string.IsNullOrWhiteSpace(message.Subject) ? "(No Subject)" : message.Subject;
                        string date = message.Date.ToString("yyyy-MM-dd HH:mm");
                        string uid = Path.GetFileNameWithoutExtension(fileNameWPath).Substring(16);
                        string displayText = $"{uid} | {date} | {sender} | {subject}";
                        if (xIsShort) displayText = $"{uid} | {date} | {sender}";
                        xNode.Nodes.Add(uid, displayText);
                    }
                }
                xTV.SelectedNode.Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading emails:\n" + ex.Message);
            }
        }
    }
}


