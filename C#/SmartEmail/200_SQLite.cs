using Microsoft.Data.Sqlite;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Data.Sqlite;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Data.Sqlite;
using System.Text.Json;
namespace SmartEmail
{
    /// <summary>
    /// Provides utility methods for working with the SQLite database used in SmartEmail.
    /// Handles creation of tables, insertion of emails, and retrieval of embeddings and metadata.
    /// </summary>
    internal static class MySQLite
    {
        /// <summary>
        /// Creates the "Queries" table if it does not already exist.
        /// The table stores query metadata along with the serialized embedding vector.
        /// </summary>
        public static void CreateTableQueries(SqliteConnection conn)
        {
            try
            {
                string createTableSql = @"CREATE TABLE IF NOT EXISTS Queries (
                ID TEXT PRIMARY KEY,ShortName TEXT,Description TEXT,QEmbedding TEXT );";
                using SqliteCommand cmd = new SqliteCommand(createTableSql, conn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
        }
        /// <summary>
        /// Creates the "EmailEmbeddings" table if it does not already exist.
        /// The table stores email metadata, serialized attachments, and the embedding vector.
        /// </summary>
        public static void CreateTableEmails(SqliteConnection conn)
        {
            try
            {
                string createTableSql = @" CREATE TABLE IF NOT EXISTS EmailEmbeddings (
                UID INTEGER, Date_yyyyMMdd TEXT, Sender TEXT, Subject TEXT,            
                EmailContent TEXT, Attachments TEXT, Embedding TEXT );";
                using SqliteCommand cmd = new SqliteCommand(createTableSql, conn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
        }
        /// <summary>
        /// Loads the schema (list of user-defined tables) from the database into a TreeView.
        /// Excludes system tables that start with "sqlite_".
        /// </summary>
        public static void LoadSchema(TreeView TV)
        {
            try
            {
                string connectionString = "Data Source=" + GVar.PathDB;
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT name, sql FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";
                    using (SqliteCommand cmd = new SqliteCommand(query, connection))
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        TV.Nodes.Clear();

                        while (reader.Read())
                        {
                            string tableName = reader["name"].ToString();
                            TreeNode tableNode = new TreeNode(tableName);
                            TV.Nodes.Add(tableNode);
                        }
                        TV.ExpandAll();
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// Inserts a batch of emails into the EmailEmbeddings table.
        /// Serializes attachments and embeddings as JSON before storing them.
        /// Skips operation if the list is empty.
        /// </summary>
        public static void AddEmails(string xDBPath, List<EmailEmbedding> xEmails)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xDBPath))
                {
                    MessageBox.Show("Database path is not set.");
                    return;
                }
                if (xEmails == null || xEmails.Count == 0) return;
                string connString = $"Data Source={xDBPath}";
                using SqliteConnection conn = new SqliteConnection(connString);
                conn.Open();
                foreach (EmailEmbedding T in xEmails)
                {
                    string insertSql = @"
        INSERT INTO EmailEmbeddings (UID, Date_yyyyMMdd, Sender, Subject, EmailContent, Attachments, Embedding)
        VALUES (@UID, @Date_yyyyMMdd, @Sender, @Subject, @EmailContent, @Attachments, @Embedding);";

                    using var cmd = new SqliteCommand(insertSql, conn);
                    cmd.Parameters.AddWithValue("@UID", T.UID);  // if UID is stored as int
                    cmd.Parameters.AddWithValue("@Date_yyyyMMdd", T.Date_yyyyMMdd);
                    cmd.Parameters.AddWithValue("@Sender", T.Sender);
                    cmd.Parameters.AddWithValue("@Subject", T.Subject ?? "");
                    cmd.Parameters.AddWithValue("@EmailContent", T.EmailContent ?? "");

                    string embeddingJson = JsonSerializer.Serialize(T.Embedding);
                    string attachmentsJson = JsonSerializer.Serialize(T.Attachments);

                    cmd.Parameters.AddWithValue("@Attachments", attachmentsJson);
                    cmd.Parameters.AddWithValue("@Embedding", embeddingJson);

                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }
        /// <summary>
        /// Retrieves all email embeddings from the database.
        /// Each record returns a tuple: (UID, embedding as list of strings, default similarity value -2).
        /// Embeddings are stored as JSON arrays and split into string values.
        /// </summary>
        public static List<(string, List<string>, double)> GetEmailEmbedding()
        {
            try
            {
                List<(string, List<string>, double)> RetLst = new List<(string, List<string>, double)>();
                string connString = $"Data Source={GVar.PathDB}";
                using SqliteConnection conn = new SqliteConnection(connString);
                conn.Open();
                string xSql = @"SELECT UID, Embedding FROM EmailEmbeddings;";

                using var cmd = new SqliteCommand(xSql, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string uid = reader.GetString(0);
                    string embedding = reader.GetString(1);
                    List<string> embeddingLst = embedding.Substring(1, embedding.Length - 2).Split(",").ToList();
                    RetLst.Add((uid, embeddingLst, -2));
                }
                return RetLst;
            }
            catch
            {
                return new List<(string, List<string>, double)>();
            }
        }
        /// <summary>
        /// Retrieves the stored embedding vector for a specific query by ID.
        /// The embedding is returned as a list of string values.
        /// If no match is found, an empty list is returned.
        /// </summary>
        public static List<string> GetQueryEmbedding(string xID)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xID))
                {
                    MessageBox.Show("Query ID cannot be empty.");
                    return new List<string>();
                }
                string connString = $"Data Source={GVar.PathDB}";
                using SqliteConnection conn = new SqliteConnection(connString);
                conn.Open();
                string xSql = @"SELECT QEmbedding FROM QUERIES WHERE ID = @ID;";

                using (SqliteCommand cmd = new SqliteCommand(xSql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", xID);
                    object value = cmd.ExecuteScalar();
                    if (value == null || value == DBNull.Value) return new List<string>();

                    string valStr = value.ToString();
                    if (valStr != null && valStr.Length >= 2 && valStr.StartsWith("[") && valStr.EndsWith("]"))
                    {
                        valStr = valStr.Substring(1, valStr.Length - 2); // Remove first and last character
                        return valStr.Split(",").ToList();
                    }
                    return new List<string>();
                }
            }
            catch
            {
                return new List<string>();
            }
        }
        /// <summary>
        /// Identifies which emails from a given month folder still need to be embedded.
        /// Compares .eml files in the folder against UIDs already stored in the database.
        /// Returns only those UIDs that are not yet embedded.
        /// </summary>
        public static List<string> GetToBeEmbeddedEmails(string xPathMonth)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xPathMonth) || !Directory.Exists(xPathMonth))
                {
                    MessageBox.Show("Email folder path is invalid or does not exist.");
                    return new List<string>();
                }
                string yyyyMM = xPathMonth.Substring(xPathMonth.Length - 6);
                List<string> LstToBeEmbedded = Directory.GetFiles(xPathMonth, "*.eml").Select(p => Path.GetFileNameWithoutExtension(p).Substring(16)).ToList();
                string connString = $"Data Source={GVar.PathDB}";

                using (SqliteConnection conn = new SqliteConnection(connString))
                {
                    conn.Open();
                    string sql = "SELECT UID FROM EmailEmbeddings WHERE SUBSTR(Date_yyyyMMdd, 1, 6) = '" + yyyyMM + "'";

                    using (SqliteCommand cmd = new SqliteCommand(sql, conn))
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LstToBeEmbedded.RemoveAll(p => p == reader.GetString(0));
                        }
                    }
                }
                return LstToBeEmbedded;
            }
            catch
            {
                return new List<string>();
            }
        }
        /// <summary>
        /// Retrieves the list of UIDs of emails already embedded for a specific month.
        /// Queries the database for all emails where the Date_yyyyMMdd prefix matches yyyyMM.
        /// </summary>
        public static List<string> GetEmbeddedEmails(string yyyyMM)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(yyyyMM) || yyyyMM.Length != 6)
                {
                    MessageBox.Show("Invalid month format. Expected yyyyMM.");
                    return new List<string>();
                }
                List<string> LstEmbedded = new List<string>();
                string connString = $"Data Source={GVar.PathDB}";

                using (SqliteConnection conn = new SqliteConnection(connString))
                {
                    conn.Open();
                    string sql = "SELECT UID FROM EmailEmbeddings WHERE SUBSTR(Date_yyyyMMdd, 1, 6) = '" + yyyyMM + "'";

                    using (SqliteCommand cmd = new SqliteCommand(sql, conn))
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LstEmbedded.Add(reader.GetString(0));
                        }
                    }
                }
                return LstEmbedded;
            }
            catch
            {
                return new List<string>();
            }
        }
        /// <summary>
        /// Retrieves the subject line of an email by its UID.
        /// Returns an empty string if no subject is found, or "-1" if an error occurs.
        /// </summary>
        public static string GetSubjectByUID(string xUID)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xUID))
                {
                    MessageBox.Show("Email UID cannot be empty.");
                    return "";
                }
                string connString = $"Data Source={GVar.PathDB}";
                using SqliteConnection conn = new SqliteConnection(connString);
                conn.Open();

                string xSql = @"SELECT Subject FROM EmailEmbeddings WHERE UID = @UID;";
                using var cmd = new SqliteCommand(xSql, conn);
                cmd.Parameters.AddWithValue("@UID", xUID);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "";
            }
            catch
            {
                return "-1";
            }
        }
    }
}

