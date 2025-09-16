using Microsoft.Data.Sqlite;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

namespace SmartEmail
{
    /// <summary>
    /// Provides CRUD operations for managing query definitions in the database.
    /// Each query consists of an ID, short name, description, and a generated embedding.
    /// Includes validation checks and helper methods for populating UI controls.
    /// </summary>
    internal class Queries
    {
        /// <summary>
        /// Adds a new query or updates an existing one.
        /// 
        /// Steps:
        /// 1. Trims inputs and validates short name/description.
        /// 2. If ID is empty, generates a new one; otherwise deletes the old entry.
        /// 3. Generates embedding for the description using AI.EmbedQuery.
        /// 4. Inserts the query into the Queries table.
        /// </summary>
        public static bool AddUpdate(string xDBPath, string xShortName, string xDescription, string xID = "")
        {
            try
            {

                string xQEmbedding = AI.EmbedQuery(xDescription);
                if (string.IsNullOrEmpty(xQEmbedding))
                {
                    MessageBox.Show("Embedding could not be generated. Query not saved.");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(xDBPath))
                {
                    MessageBox.Show("Database path is not set.");
                    return false;
                }

                xShortName = xShortName.Trim();
                xDescription = xDescription.Trim();
                if (!Validate(xDBPath, xShortName, xDescription, xID)) return false;

                if (xID == "")
                {
                    xID = GetNewID(xDBPath);
                }
                else
                {
                    Delete(xDBPath, xID);
                }

                string connString = $"Data Source={xDBPath}";
                using SqliteConnection conn = new SqliteConnection(connString);
                conn.Open();

                string xInsertCmd = @"INSERT INTO Queries (ID, ShortName, Description, QEmbedding)
                VALUES (@ID, @ShortName, @Description, @QEmbedding);";

                using SqliteCommand cmd = new SqliteCommand(xInsertCmd, conn);
                cmd.Parameters.AddWithValue("@ID", xID);
                cmd.Parameters.AddWithValue("@ShortName", xShortName);
                cmd.Parameters.AddWithValue("@Description", xDescription);
                cmd.Parameters.AddWithValue("@QEmbedding", xQEmbedding);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Query added successfully!");
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Validates that short name and description are not empty, 
        /// and checks that the short name does not already exist for another query.
        /// </summary>
        private static bool Validate(string xDBPath, string xShortName, string xDescription, string xID)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xDBPath))
                {
                    MessageBox.Show("Database path is not set.");
                    return false;
                }

                if (xShortName == "" || xDescription == "")
                {
                    MessageBox.Show("Short Name and Description fields must not be empty");
                    return false;
                }
                if (ShortNameExists(xDBPath, xShortName.ToLower(), xID))
                {
                    MessageBox.Show("Short Name: \"" + xShortName + "\" " + "already exists.");
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves the next available query ID by taking the current max ID in the database
        /// and incrementing it. Returns empty string if it fails.
        /// </summary>
        private static string GetNewID(string xDBPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xDBPath))
                {
                    MessageBox.Show("Database path is not set.");
                    return null;
                }

                string connString = $"Data Source={xDBPath}";
                using SqliteConnection conn = new SqliteConnection(connString);
                conn.Open();
                string query = "SELECT MAX(CAST(ID AS INTEGER)) FROM Queries;";
                using SqliteCommand cmd = new SqliteCommand(query, conn);
                object result = cmd.ExecuteScalar();
                int maxID = (result == DBNull.Value || result == null) ? 0 : Convert.ToInt32(result);
                return (maxID + 1).ToString();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Populates a TreeView with all queries from the database, 
        /// showing the ID as the node name and short name as the display text.
        /// </summary>
        public static void PopTV(TreeView TV, string xDBPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xDBPath))
                {
                    MessageBox.Show("Database path is not set.");
                    return;
                }

                TV.Nodes.Clear();
                string connString = $"Data Source={xDBPath}";
                using SqliteConnection conn = new SqliteConnection(connString);
                conn.Open();

                string selectCmd = "SELECT ID, ShortName FROM Queries ORDER BY ID;";
                using SqliteCommand cmd = new SqliteCommand(selectCmd, conn);
                using SqliteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string ID = reader.GetString(0);
                    string shortName = reader.GetString(1);

                    TreeNode xNode = new TreeNode
                    {
                        Name = ID,
                        Text = shortName
                    };
                    TV.Nodes.Add(xNode);
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Populates text fields with the short name and description of the query 
        /// selected in the TreeView. Reads the description from the database.
        /// </summary>
        public static void PopInRev(string xDBPath, TreeView TV, TextBox txtShortName, TextBox txtDescription)
        {
            try
            {
                if (TV == null || txtShortName == null || txtDescription == null) return;
                if (TV.SelectedNode == null) return;
                if (string.IsNullOrWhiteSpace(xDBPath))
                {
                    MessageBox.Show("Database path is not set.");
                    return;
                }

                string xID = TV.SelectedNode.Name;
                string xShortName = TV.SelectedNode.Text;
                string connString = $"Data Source={xDBPath}";
                using SqliteConnection conn = new SqliteConnection(connString);
                conn.Open();

                string query = "SELECT Description FROM Queries WHERE ID = @ID;";
                using SqliteCommand cmd = new SqliteCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", xID);

                object result = cmd.ExecuteScalar();
                txtShortName.Text = xShortName;
                txtDescription.Text = result?.ToString() ?? "-";
            }
            catch
            {
            }
        }
        /// <summary>
        /// Checks whether a short name already exists in the Queries table, 
        /// excluding the current ID (for updates). 
        /// Returns true if a duplicate exists.
        /// </summary>
        public static bool ShortNameExists(string xDBPath, string xShortName, string xID)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xDBPath))
                {
                    MessageBox.Show("Database path is not set.");
                    return false;
                }

                string connString = $"Data Source={xDBPath}";
                using SqliteConnection conn = new SqliteConnection(connString);
                conn.Open();

                string xCheckCmd = "SELECT COUNT(*) FROM Queries WHERE LOWER(ShortName) = @ShortName AND ID != @ID;";
                using SqliteCommand cmd = new SqliteCommand(xCheckCmd, conn);
                cmd.Parameters.AddWithValue("@ShortName", xShortName.ToLower());
                cmd.Parameters.AddWithValue("@ID", xID);

                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Deletes a query from the database by its ID.
        /// If ID is null or empty, no action is performed.
        /// </summary>
        public static void Delete(string xDBPath, string xID)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xDBPath))
                {
                    MessageBox.Show("Database path is not set.");
                    return;
                }

                if (string.IsNullOrEmpty(xID)) { return; }
                string connString = $"Data Source={xDBPath}";
                using SqliteConnection conn = new SqliteConnection(connString);
                conn.Open();
                string xDeleteCmd = @"DELETE FROM QUERIES WHERE ID = @ID;";
                using SqliteCommand cmd = new SqliteCommand(xDeleteCmd, conn);
                cmd.Parameters.AddWithValue("@ID", xID);
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
        }
    }
}




















