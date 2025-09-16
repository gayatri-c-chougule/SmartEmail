//using Microsoft.Data.Sqlite;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace SmartEmail
{
    internal class BaseFunctions
    {
        /// <summary>
        /// Populates the given TreeView with month folders found in the email download path.
        /// Clears existing nodes, retrieves all directories from GVar.PathEmailDL,
        /// and adds each folder name as a new node in the TreeView.
        /// </summary>
        public static void PopMonthFolders(TreeView TV)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(GVar.PathEmailDL) || !Directory.Exists(GVar.PathEmailDL))
                {
                    MessageBox.Show("Email download folder path is not set or does not exist.");
                    return;
                }
                TV.Nodes.Clear();
                List<string> lstMonths = Directory.GetDirectories(GVar.PathEmailDL).ToList();
                foreach (string T in lstMonths)
                {
                    string folderName = Path.GetFileName(T);
                    TV.Nodes.Add(folderName, folderName);
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Populates the ComboBox with predefined email providers.
        /// Clears existing items, adds common providers (Gmail, Outlook, Yahoo, etc.),
        /// and sets the default selection to the first provider.
        /// </summary>
        public static void PopCmbEmailProviders(ComboBox cmbSettingsEmailProvider)
        {
            try
            {
                if (cmbSettingsEmailProvider == null)
                {
                    MessageBox.Show("Email provider dropdown is not initialized.");
                    return;
                }

                cmbSettingsEmailProvider.Items.Clear();
                string[] emailProviders = { "Gmail", "Outlook/Hotmail/Live (Microsoft)", "Yahoo", "Rediffmail", "Zoho mail" };
                cmbSettingsEmailProvider.Items.AddRange(emailProviders);
                cmbSettingsEmailProvider.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        /// <summary>
        /// Customizes how TreeView nodes are drawn when selected but unfocused.
        /// If a node is selected and the TreeView is not focused, 
        /// it draws the node manually with highlight colors.
        /// Otherwise, it falls back to the default rendering.
        /// </summary>
        public static void TVDrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node == null) return;
            var selected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected;
            var unfocused = !e.Node.TreeView.Focused;
            if (selected && unfocused)
            {
                var font = e.Node.NodeFont ?? e.Node.TreeView.Font;
                e.Graphics.FillRectangle(System.Drawing.SystemBrushes.Highlight, e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, SystemColors.HighlightText, TextFormatFlags.GlyphOverhangPadding);
            }
            else
            {
                e.DrawDefault = true;
            }
        }
        /// <summary>
        /// Computes the cosine similarity between two vectors of strings.
        /// Each string is converted to a double before calculation.
        /// Returns a value between -1 and 1, where 1 means identical direction
        /// and -1 means opposite direction. If vectors cannot be computed, returns -2.
        /// </summary>
        public static double GetCosineSimilarity(List<string> xV1, List<string> xV2)
        {
            try
            {
                if (xV1 == null || xV2 == null || xV1.Count != xV2.Count)
                {
                    MessageBox.Show("Cosine similarity cannot be computed: invalid or mismatched vectors.");
                    return -2;
                }
                double dot = 0, norm1 = 0, norm2 = 0;
                for (int i = 0; i < xV1.Count; i++)
                {
                    double val1 = Convert.ToDouble(xV1[i]);
                    double val2 = Convert.ToDouble(xV2[i]);

                    dot += val1 * val2;
                    norm1 += val1 * val1;
                    norm2 += val2 * val2;
                }

                double denominator = (double)(Math.Sqrt(norm1) * Math.Sqrt(norm2));
                if (denominator == 0) return 0;
                return dot / denominator;
            }
            catch
            {
                //cosine similarity ranges between -1 and 1
                return -2;
            }
        }
    }
}
