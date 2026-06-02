using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreMyFiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            treeView1.BeforeExpand += TreeView1_BeforeExpand;
            treeView1.AfterSelect += TreeView1_AfterSelect;
            listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            listView1.ColumnClick += ListView1_ColumnClick;
            // List displays it as left-right scroll, details as vertical list with columns.
            listView1.View = View.Details;
            // Columns: Name, Size, Date Modified, Type.
            listView1.Columns.Add("Name", 200);
            listView1.Columns.Add("Size", 100);
            listView1.Columns.Add("Date Modified", 150);
            listView1.Columns.Add("Type", 100);
            listView1.FullRowSelect = true; // Enable full row selection for better user experience.
        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string filePath = listView1.SelectedItems[0].Tag.ToString();
                try
                {
                    System.Diagnostics.Process.Start(filePath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening file: " + ex.Message.ToString());
                }
            }
        }
        // Sort column index, -1 means no sorting applied.
        private int sortColumn = -1;

        private void UpdateColumnHeader(int column, SortOrder order)
        {
            // Reset all column headers
            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                string headerText = listView1.Columns[i].Text;
                // Remove any existing arrow indicators
                headerText = headerText.Replace(" ▲", "").Replace(" ▼", "");
                listView1.Columns[i].Text = headerText;
            }

            // Add arrow to selected column
            string indicator = order == SortOrder.Ascending ? " ▲" : " ▼";
            listView1.Columns[column].Text += indicator;
        }

        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Check for selected column. Different implies focus on that column.
            if (e.Column != sortColumn)
            {
                sortColumn = e.Column;
                listView1.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listView1.Sorting == SortOrder.Ascending)
                    listView1.Sorting = SortOrder.Descending;
                else
                    listView1.Sorting = SortOrder.Ascending;
            }

            UpdateColumnHeader(e.Column, listView1.Sorting);
            listView1.Sort();
            // Rebuild the list with new sorting order. ListViewItemComparer will handle the actual comparison logic.
            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, listView1.Sorting);
        }

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            node.Nodes.Clear();
            try
            {
                string[] directories = Directory.GetDirectories(node.Tag.ToString());
                foreach (string dir in directories)
                {
                    TreeNode subNode = new TreeNode(Path.GetFileName(dir));
                    subNode.Tag = dir;
                    subNode.Nodes.Add("Loading in Progress");
                    node.Nodes.Add(subNode);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error loading directories: " + ex.Message.ToString());
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = e.Node.Tag.ToString();
            listView1.Items.Clear();
            try
            {
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    ListViewItem item = new ListViewItem(Path.GetFileName(file));
                    item.Tag = file;
                    item.SubItems.Add(new FileInfo(file).Length.ToString() + " bytes");
                    item.SubItems.Add(new FileInfo(file).LastWriteTime.ToString());
                    item.SubItems.Add(new FileInfo(file).Extension);
                    listView1.Items.Add(item);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading files: " + ex.Message.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var drive in Environment.GetLogicalDrives())
            {
                TreeNode node = new TreeNode(drive);
                node.Tag = drive;
                node.Nodes.Add("Loading in Progress");
                treeView1.Nodes.Add(node);
            }
        }
    }
}
