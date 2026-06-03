using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WhereAreMyFiles
{
    public partial class Form1 : Form
    {
        private readonly Stack<string> navigationStack = new Stack<string>();
        private string currentPath;

        public Form1()
        {
            InitializeComponent();
            listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            listView1.ColumnClick += ListView1_ColumnClick;
            buttonBack.Click += ButtonBack_Click;

            listView1.View = View.Details;
            listView1.Columns.Add("Name", 240);
            listView1.Columns.Add("Size", 110);
            listView1.Columns.Add("Date Modified", 150);
            listView1.Columns.Add("Type", 90);
            listView1.FullRowSelect = true;

            chart1.Series["FolderSizes"].ChartType = SeriesChartType.Pie;
            chart1.Series["FolderSizes"].IsValueShownAsLabel = true;
            chart1.Series["FolderSizes"].Label = "#PERCENT{P0}";
            chart1.Series["FolderSizes"].LegendText = "#VALX";
            chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
            chart1.MouseClick += Chart1_MouseClick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            currentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            ShowDrives();
        }

        private void ShowDrives()
        {
            navigationStack.Clear();
            currentPath = null;
            labelCurrentPath.Text = "This PC";
            buttonBack.Enabled = false;
            listView1.Items.Clear();
            PopulateChartForDrives();
        }

        private void PopulateChartForDrives()
        {
            chart1.Series["FolderSizes"].Points.Clear();
            foreach (string drive in Environment.GetLogicalDrives())
            {
                try
                {
                    DriveInfo info = new DriveInfo(drive);
                    if (info.IsReady)
                    {
                        long size = info.TotalSize;
                        long used = info.TotalSize - info.TotalFreeSpace;
                        int idx = chart1.Series["FolderSizes"].Points.AddXY(info.Name, used > 0 ? used : 1);
                        chart1.Series["FolderSizes"].Points[idx].LegendText = info.Name;
                        chart1.Series["FolderSizes"].Points[idx].ToolTip = info.Name;
                    }
                }
                catch
                {
                    // Ignore drives that cannot be read.
                }
            }

            if (chart1.Series["FolderSizes"].Points.Count == 0)
            {
                chart1.Series["FolderSizes"].Points.AddXY("No drives", 1);
            }
        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }

            string selectedPath = listView1.SelectedItems[0].Tag as string;
            if (string.IsNullOrEmpty(selectedPath))
            {
                return;
            }

            if (Directory.Exists(selectedPath))
            {
                NavigateTo(selectedPath, true);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(selectedPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening file: " + ex.Message);
            }
        }

        private int sortColumn = -1;

        private void UpdateColumnHeader(int column, SortOrder order)
        {
            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                string headerText = listView1.Columns[i].Text.Replace(" ▲", "").Replace(" ▼", "");
                listView1.Columns[i].Text = headerText;
            }

            string indicator = order == SortOrder.Ascending ? " ▲" : " ▼";
            listView1.Columns[column].Text += indicator;
        }

        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn)
            {
                sortColumn = e.Column;
                listView1.Sorting = SortOrder.Ascending;
            }
            else
            {
                listView1.Sorting = listView1.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }

            UpdateColumnHeader(e.Column, listView1.Sorting);
            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, listView1.Sorting);
            listView1.Sort();
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            if (navigationStack.Count == 0)
            {
                ShowDrives();
                return;
            }

            string previousPath = navigationStack.Pop();
            NavigateTo(previousPath, false);
        }

        private void NavigateTo(string path, bool pushCurrent)
        {
            if (pushCurrent && !string.IsNullOrEmpty(currentPath))
            {
                navigationStack.Push(currentPath);
                buttonBack.Enabled = true;
            }

            currentPath = path;
            labelCurrentPath.Text = path;
            listView1.Items.Clear();
            chart1.Series["FolderSizes"].Points.Clear();

            try
            {
                foreach (string directory in Directory.GetDirectories(path))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(directory);
                    ListViewItem item = new ListViewItem(dirInfo.Name);
                    item.Tag = directory;
                    item.SubItems.Add(GetDirectorySize(directory).ToString() + " bytes");
                    item.SubItems.Add(dirInfo.LastWriteTime.ToString());
                    item.SubItems.Add("Folder");
                    listView1.Items.Add(item);
                }

                foreach (string file in Directory.GetFiles(path))
                {
                    FileInfo info = new FileInfo(file);
                    ListViewItem item = new ListViewItem(info.Name);
                    item.Tag = file;
                    item.SubItems.Add(info.Length.ToString() + " bytes");
                    item.SubItems.Add(info.LastWriteTime.ToString());
                    item.SubItems.Add(info.Extension);
                    listView1.Items.Add(item);
                }

                PopulatePieChartForCurrentFolder(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading path: " + ex.Message);
            }
        }

        private void PopulatePieChartForCurrentFolder(string path)
        {
            chart1.Series["FolderSizes"].Points.Clear();
            try
            {
                var entries = new List<Tuple<string, long>>();

                foreach (string directory in Directory.GetDirectories(path))
                {
                    long size = GetDirectorySize(directory);
                    if (size > 0)
                    {
                        entries.Add(Tuple.Create(Path.GetFileName(directory), size));
                    }
                }

                long fileBytes = 0;
                foreach (string file in Directory.GetFiles(path))
                {
                    fileBytes += new FileInfo(file).Length;
                }
                if (fileBytes > 0)
                {
                    entries.Add(Tuple.Create("Files in folder", fileBytes));
                }

                if (entries.Count == 0)
                {
                    chart1.Series["FolderSizes"].Points.AddXY("Empty", 1);
                    chart1.Series["FolderSizes"].Points[0].LegendText = "Empty";
                    return;
                }

                foreach (var entry in entries)
                {
                    int index = chart1.Series["FolderSizes"].Points.AddXY(entry.Item1, entry.Item2);
                    chart1.Series["FolderSizes"].Points[index].LegendText = entry.Item1;
                    chart1.Series["FolderSizes"].Points[index].ToolTip = entry.Item1 + ": " + entry.Item2 + " bytes";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating pie chart: " + ex.Message);
            }
        }

        private void Chart1_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult result = chart1.HitTest(e.X, e.Y);
            if (result.ChartElementType != ChartElementType.DataPoint || result.Series == null || result.PointIndex < 0)
            {
                return;
            }

            string selectedLabel = result.Series.Points[result.PointIndex].AxisLabel;
            if (selectedLabel == "Empty" || selectedLabel == "Files in folder")
            {
                return;
            }

            if (string.IsNullOrEmpty(currentPath))
            {
                string drivePath = Path.Combine(selectedLabel, Path.DirectorySeparatorChar.ToString());
                if (Directory.Exists(drivePath))
                {
                    NavigateTo(drivePath, true);
                }
                return;
            }

            string nextPath = Path.Combine(currentPath, selectedLabel);
            if (Directory.Exists(nextPath))
            {
                NavigateTo(nextPath, true);
            }
            else if (Directory.Exists(Path.Combine(currentPath, selectedLabel)))
            {
                NavigateTo(Path.Combine(currentPath, selectedLabel), true);
            }
        }

        private long GetDirectorySize(string path)
        {
            long size = 0;
            try
            {
                foreach (string file in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
                {
                    size += new FileInfo(file).Length;
                }
            }
            catch
            {
            }
            return size;
        }
    }
}
