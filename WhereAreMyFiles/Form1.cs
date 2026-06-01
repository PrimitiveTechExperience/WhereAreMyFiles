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
            listView1.View = View.List;
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
