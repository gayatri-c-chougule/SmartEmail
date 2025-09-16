//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mail;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using System.Text.Json;
//using System.Collections.Generic;

namespace SmartEmail
{
    /// <summary>
    /// Global configuration variables used across the SmartEmail project.
    /// Contains static paths for email downloads, Python executables, 
    /// Python scripts for embeddings, and the embeddings database.
    /// These are centralized constants/variables.
    /// Any hard-coded paths should ideally be moved to a config file or environment variables
    /// to make the application portable and secure.
    /// </summary>
    public static class GVar
    {
        public static string PathEmailDL = @"D:\100_SmartEmail_exe\DATA\100_Emails_DL\";
        public static string MonthFolderName = "";
        public static string PathPythonExe = @"D:\100_SmartEmail_exe\Python\.venv\Scripts\python.exe";
        public static string PathPythonProgEmail = @"D:\100_SmartEmail_exe\Python\generate_embeddings.py";
        public static string PathPythonProgQuery = @"D:\100_SmartEmail_exe\Python\generate_query_embeddings.py";
        public static string PathDB = @"D:\100_SmartEmail_exe\DATA\100_DataBase\Embeddings.db";
    }
    /// <summary>
    /// Represents the basic information of an email.
    /// Includes sender, subject, and date metadata.
    /// This is a lightweight model primarily used 
    /// for listing or identifying emails without loading full content/attachments.
    /// </summary>
    public class EmailInfo
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public EmailInfo()
        {
        }
    }
    /// <summary>
    /// Represents a fully processed email with embeddings.
    /// Contains metadata, content, attachments, and its vector embedding.
    /// - Embeddings (List&lt;float&gt;) are used for semantic search in the database.
    /// - UID uniquely identifies the email and must stay consistent for retrieval.
    /// - Attachments are stored as a list of lightweight "Attachment" objects.
    /// </summary>
    public class EmailEmbedding
    {
        public string UID { get; set; }
        public string Date_yyyyMMdd { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string EmailContent { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<float> Embedding { get; set; }
    }
    /// <summary>
    /// Represents an email attachment.
    /// Contains the file name and its content (likely base64 encoded or plain text).
    /// Attachments are simplified into a string "Content" property.
    /// This avoids dealing with raw binary streams in early stages, 
    /// but might require conversion when handling real files.
    /// </summary>
    public class Attachment
    {
        public string FileName { get; set; }
        public string Content { get; set; }
    }
}
