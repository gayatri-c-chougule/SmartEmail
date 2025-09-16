//using Microsoft.Data.Sqlite;
//using System.Collections.Generic;
using System.Diagnostics;
//using System.Net.Http.Headers;
//using System.Text;
using System.Text.Json;

namespace SmartEmail
{
    /// <summary>
    /// Provides methods to generate embeddings for emails and queries 
    /// by invoking external Python scripts. 
    /// Handles execution, error capture, JSON parsing, and saving results to the database.
    /// </summary>
    internal static class AI
    {
        /// <summary>
        /// Embeds all emails for a given month that are not yet embedded.
        /// 
        /// Steps:
        /// 1. Identifies .eml files in the month folder that are missing from the database.
        /// 2. Runs the Python embedding script with the list of UIDs and month path.
        /// 3. Captures output and error streams for diagnostics.
        /// 4. Expects the script to generate a temp.json file with embeddings.
        /// 5. Loads the JSON, deserializes into EmailEmbedding objects, 
        ///    and inserts them into the database.
        /// </summary>
        public static void EmbedEmails(string yyyyMM)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(yyyyMM) || yyyyMM.Length != 6)
                {
                    MessageBox.Show("Invalid month format. Expected yyyyMM.");
                    return;
                }
                string pathMonth = GVar.PathEmailDL + yyyyMM;
                if (!Directory.Exists(pathMonth))
                {
                    MessageBox.Show("Email folder does not exist: " + pathMonth);
                    return;
                }
                List<string> LstToBeEmbedded = MySQLite.GetToBeEmbeddedEmails(pathMonth);
                string ToBeEmbeddedUIDs = string.Join(",", LstToBeEmbedded);
                if (ToBeEmbeddedUIDs == "")
                {
                    MessageBox.Show("All emails are embedded.");
                    return;
                }
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = GVar.PathPythonExe,
                    Arguments = $"\"{GVar.PathPythonProgEmail}\" \"{ToBeEmbeddedUIDs.Replace("\"", "\\\"")}\" \"{pathMonth}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                var process = Process.Start(psi);
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrWhiteSpace(errors))
                {
                    MessageBox.Show("Python script error:\n" + errors);
                    return;
                }
                if (!File.Exists(pathMonth + "\\temp.json"))
                {
                    MessageBox.Show("JSON file not found. Make sure the script created it.");
                    return;
                }
                string jsonText = File.ReadAllText(pathMonth + "\\temp.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<EmailEmbedding> emails = JsonSerializer.Deserialize<List<EmailEmbedding>>(jsonText, options) ?? new List<EmailEmbedding>();
                MySQLite.AddEmails(GVar.PathDB, emails);
                MessageBox.Show($"Loaded {emails.Count} embeddings.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        /// <summary>
        /// Generates an embedding for a single query string by calling the Python script.
        /// 
        /// Steps:
        /// 1. Escapes quotes in the query text.
        /// 2. Runs the Python query embedding script with the query as argument.
        /// 3. Captures output and error streams.
        /// 4. Returns the JSON-formatted embedding string, or null if an error occurs.
        /// </summary>
        public static string EmbedQuery(string xQuery)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xQuery))
                {
                    MessageBox.Show("Query text cannot be empty.");
                    return null;
                }
                string xInput = xQuery.Replace("\"", "\\\"");
                var psi = new ProcessStartInfo
                {
                    FileName = GVar.PathPythonExe,
                    Arguments = $"\"{GVar.PathPythonProgQuery}\" \"{xInput}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using var process = Process.Start(psi);
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();
                process.WaitForExit();
                if (!string.IsNullOrWhiteSpace(errors))
                {
                    MessageBox.Show("Python Error:\n" + errors);
                    return null;
                }
                string xEmbeddingJson = output.Trim();
                return xEmbeddingJson;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected Error: " + ex.Message);
                return null;
            }
        }
    }
}
