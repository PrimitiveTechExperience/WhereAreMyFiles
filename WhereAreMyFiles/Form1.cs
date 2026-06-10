using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ReaLTaiizor.Forms;

namespace WhereAreMyFiles
{
    public partial class Form1 : ReaLTaiizor.Forms.MaterialForm
    {
        // To optimize performance, we maintain 2 caches:
        /*
         * The first cachie is a simple in-memory cache of directory sizes already calculated in session. 
         * This is keyed by normalized path and stores the size along with the last write time of the directory. 
         * When we need to get a directory size, we first check this cache and if the last write time matches, we can return the cached size immediately without recalculating.
         * 
         * The second cache is a simple on-disk cache that is loaded at startup and saved at shutdown. It uses the same key, value structure as the in-memory cache.
         * 
         * We also reference the write time of the directory for this caching mechanism. This way, if a directory's contents change (files added/removed/modified), 
         * the last write time will update, and we will know to invalidate the cache entry and recalculate the size on the next access.
         */
        private readonly Stack<string> navigationStack = new Stack<string>();
        private readonly Dictionary<string, DirectorySizeCacheEntry> directorySizeCache = new Dictionary<string, DirectorySizeCacheEntry>(StringComparer.OrdinalIgnoreCase);
        private readonly object cacheLock = new object();
        private readonly string cacheFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WhereAreMyFiles", "folder-size-cache.txt");
        private CancellationTokenSource loadCancellation;
        private string currentPath;

        public Form1()
        {
            InitializeComponent();
            LoadCache();
            FormClosed += Form1_FormClosed;
            listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            listView1.ColumnClick += ListView1_ColumnClick;
            buttonBack.Click += ButtonBack_Click;

            listView1.View = View.Details;
            listView1.Columns.Add("Name", 240);
            listView1.Columns.Add("Size", 110);
            listView1.Columns.Add("Date Modified", 200);
            listView1.Columns.Add("Type", 90);
            listView1.FullRowSelect = true;
            // use Chart control to display folder sizes as a pie chart.
            //chart1.Series["FolderSizes"].ChartType = SeriesChartType.Pie;
            //chart1.Series["FolderSizes"].IsValueShownAsLabel = true;
            //chart1.Series["FolderSizes"].Label = "#PERCENT{P0}";
            //chart1.Series["FolderSizes"].LegendText = "#VALX";
            //chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
            //chart1.MouseClick += Chart1_MouseClick;
            pieChart1.Series = new LiveCharts.SeriesCollection();
            pieChart1.LegendLocation = LiveCharts.LegendLocation.Right;
            pieChart1.HoverPushOut = 10;
            pieChart1.DataClick += PieChart1_DataClick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowDrives();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveCache();
            // Cancel any pending loads to avoid background work after form is closed
            if (loadCancellation != null)
            {
                loadCancellation.Cancel();
                loadCancellation.Dispose();
            }
        }

        private void ShowDrives()
        {
            CancelPendingLoad();
            navigationStack.Clear();
            currentPath = null;
            labelCurrentPath.Text = "This PC";
            buttonBack.Enabled = false;
            listView1.Items.Clear();
            StartLoadForPath(null);
        }

        private void PopulateChartForDrives()
        {
            //chart1.Series["FolderSizes"].Points.Clear();
            //foreach (string drive in Environment.GetLogicalDrives())
            //{
            //    try
            //    {
            //        // Get drives and their used space for the pie chart. We show used space instead of free space to make it more visually intuitive.
            //        DriveInfo info = new DriveInfo(drive);
            //        if (info.IsReady)
            //        {
            //            long used = info.TotalSize - info.TotalFreeSpace;
            //            int idx = chart1.Series["FolderSizes"].Points.AddXY(info.Name, used > 0 ? used : 1);
            //            chart1.Series["FolderSizes"].Points[idx].LegendText = info.Name;
            //            chart1.Series["FolderSizes"].Points[idx].ToolTip = info.Name;
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}

            //if (chart1.Series["FolderSizes"].Points.Count == 0)
            //{
            //    chart1.Series["FolderSizes"].Points.AddXY("No drives", 1);
            //}
            pieChart1.Series = new LiveCharts.SeriesCollection();
            foreach (string drive in Environment.GetLogicalDrives())
            {
                try
                {
                    DriveInfo info = new DriveInfo(drive);
                    if (info.IsReady)
                    {
                        long used = info.TotalSize - info.TotalFreeSpace;
                        var pieSeries = new LiveCharts.Wpf.PieSeries
                        {
                            Title = info.Name,
                            Values = new LiveCharts.ChartValues<long> { used > 0 ? used : 1 },
                            DataLabels = true,
                            LabelPoint = chartPoint => $"{chartPoint.Y} bytes ({chartPoint.Participation:P})",
                            ToolTip = info.Name
                        };
                        pieChart1.Series.Add(pieSeries);
                    }
                }
                catch
                {
                }
            }
            if(pieChart1.Series.Count == 0)
            {
                var pieSeries = new LiveCharts.Wpf.PieSeries
                {
                    Title = "No drives",
                    Values = new LiveCharts.ChartValues<long> { 1 },
                    DataLabels = true,
                    LabelPoint = chartPoint => "No drives",
                    ToolTip = "No drives"
                };
                pieChart1.Series.Add(pieSeries);
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
            // Clear existing, then add to selected.
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

        private void CancelPendingLoad()
        {
            if (loadCancellation != null)
            {
                loadCancellation.Cancel();
                loadCancellation.Dispose();
                loadCancellation = null;
            }
        }

        private void StartLoadForPath(string path)
        {
            CancelPendingLoad();
            loadCancellation = new CancellationTokenSource();
            CancellationToken token = loadCancellation.Token;

            listView1.Items.Clear();
            pieChart1.Series = new LiveCharts.SeriesCollection();
            labelCurrentPath.Text = path ?? "This PC";
            buttonBack.Enabled = !string.IsNullOrEmpty(path) || navigationStack.Count > 0;

            Task.Run(() => LoadPathData(path, token), token);
        }

        private void NavigateTo(string path, bool pushCurrent)
        {
            if (pushCurrent && !string.IsNullOrEmpty(currentPath))
            {
                navigationStack.Push(currentPath);
                buttonBack.Enabled = true;
            }

            currentPath = path;
            StartLoadForPath(path);
        }

        // Make a function to round up the sizes of files to kb, gb, tb, etc... for better readability in the UI, but keep the actual size in bytes for sorting and tooltips.
        private void FormatSizeForDisplay(ListViewItem item)
        {
            if (item.SubItems.Count < 2)
            {
                return;
            }
            string sizeText = item.SubItems[1].Text;
            if (!sizeText.EndsWith(" bytes"))
            {
                return;
            }
            string numberPart = sizeText.Substring(0, sizeText.Length - " bytes".Length);
            if (!long.TryParse(numberPart, out long sizeInBytes))
            {
                return;
            }
            string[] suffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            int suffixIndex = 0;
            double displaySize = sizeInBytes;
            while (displaySize >= 1024 && suffixIndex < suffixes.Length - 1)
            {
                displaySize /= 1024;
                suffixIndex++;
            }
            item.SubItems[1].Text = $"{displaySize:0.##} {suffixes[suffixIndex]}";
        }

        private void LoadPathData(string path, CancellationToken token)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    BeginInvoke((Action)(() => PopulateChartForDrives()));
                    return;
                }

                List<ListViewItem> items = new List<ListViewItem>();
                List<Tuple<string, long>> chartEntries = new List<Tuple<string, long>>();

                foreach (string directory in Directory.GetDirectories(path))
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    DirectoryInfo dirInfo = new DirectoryInfo(directory);
                    long size = GetDirectorySize(directory, token);
                    ListViewItem item = new ListViewItem(dirInfo.Name);
                    item.Tag = directory;
                    //keep this but eventually replace with formatted after adding it to chartEntries.
                    item.SubItems.Add(size.ToString() + " bytes");
                    item.SubItems.Add(dirInfo.LastWriteTime.ToString());
                    item.SubItems.Add("Folder");
                    items.Add(item);
                    if (size > 0)
                    {
                        chartEntries.Add(Tuple.Create(dirInfo.Name, size));
                    }
                }

                long fileBytes = 0;
                foreach (string file in Directory.GetFiles(path))
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    FileInfo info = new FileInfo(file);
                    fileBytes += info.Length;
                    ListViewItem item = new ListViewItem(info.Name);
                    item.Tag = file;
                    item.SubItems.Add(info.Length.ToString() + " bytes");
                    item.SubItems.Add(info.LastWriteTime.ToString());
                    item.SubItems.Add(info.Extension);
                    items.Add(item);
                }

                if (fileBytes > 0)
                {
                    chartEntries.Insert(0, Tuple.Create("Files in current folder", fileBytes));
                }

                if (token.IsCancellationRequested)
                {
                    return;
                }

                BeginInvoke((Action)(() => ApplyLoadedData(path, items, chartEntries)));
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                {
                    BeginInvoke((Action)(() => MessageBox.Show("Error loading path: " + ex.Message)));
                }
            }
        }

        private void ApplyLoadedData(string path, List<ListViewItem> items, List<Tuple<string, long>> chartEntries)
        {
            if (!string.Equals(currentPath, path, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            listView1.BeginUpdate();
            try
            {
                listView1.Items.Clear();
                listView1.Items.AddRange(items.ToArray());
            }
            finally
            {
                listView1.EndUpdate();
            }

            PopulatePieChartForCurrentFolder(chartEntries);
        }

        private void PopulatePieChartForCurrentFolder(List<Tuple<string, long>> entries)
        {
            //chart1.Series["FolderSizes"].Points.Clear();
            //if (entries == null || entries.Count == 0)
            //{
            //    chart1.Series["FolderSizes"].Points.AddXY("Empty", 1);
            //    chart1.Series["FolderSizes"].Points[0].LegendText = "Empty";
            //    return;
            //}

            //foreach (var entry in entries)
            //{
            //    int index = chart1.Series["FolderSizes"].Points.AddXY(entry.Item1, entry.Item2);
            //    chart1.Series["FolderSizes"].Points[index].LegendText = entry.Item1;
            //    chart1.Series["FolderSizes"].Points[index].ToolTip = entry.Item1 + ": " + entry.Item2 + " bytes";
            //}
            pieChart1.Series = new LiveCharts.SeriesCollection();
            if (entries == null || entries.Count == 0)
            {
                var emptySeries = new LiveCharts.Wpf.PieSeries
                {
                    Title = "Empty",
                    Values = new LiveCharts.ChartValues<long> { 1 },
                    DataLabels = true,
                    LabelPoint = chartPoint => "Empty",
                    ToolTip = "Empty"
                };
                pieChart1.Series.Add(emptySeries);
                return;
            }
            foreach (var entry in entries)
            {
                var pieSeries = new LiveCharts.Wpf.PieSeries
                {
                    Title = entry.Item1,
                    Values = new LiveCharts.ChartValues<long> { entry.Item2 },
                    DataLabels = true,
                    LabelPoint = p => p.Participation.ToString("P0"),
                    Tag = entry.Item1
                };
                //pieSeries.DataClick += PieSlice_Click;
                pieChart1.Series.Add(pieSeries);
            }
        }

        private void PieChart1_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            var series = (LiveCharts.Wpf.PieSeries)chartPoint.SeriesView;

            string selectedLabel = series?.Title;

            if (string.IsNullOrEmpty(selectedLabel)) return;

            // Need to consider two cases: root level or normal level
            // Case 1: root level
            if (string.IsNullOrEmpty(currentPath))
            {
                string drivePath = selectedLabel.EndsWith("\\", StringComparison.Ordinal) ? selectedLabel : selectedLabel + "\\";
                if (Directory.Exists(drivePath))
                {
                    NavigateTo(drivePath, true);
                }
                return;
            }
            // case 2: normal level
            if (string.IsNullOrEmpty(currentPath)) return;

            string nextPath = Path.Combine(currentPath, selectedLabel);

            if (Directory.Exists(nextPath))
            {
                NavigateTo($"{nextPath}", true);
            }

        }

        //private void Chart1_MouseClick(object sender, MouseEventArgs e)
        //{
        //    HitTestResult result = chart1.HitTest(e.X, e.Y);
        //    if (result.ChartElementType != ChartElementType.DataPoint || result.Series == null || result.PointIndex < 0)
        //    {
        //        return;
        //    }

        //    string selectedLabel = result.Series.Points[result.PointIndex].AxisLabel;
        //    if (selectedLabel == "Empty" || selectedLabel == "Files in folder")
        //    {
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(currentPath))
        //    {
        //        string drivePath = selectedLabel.EndsWith("\\", StringComparison.Ordinal) ? selectedLabel : selectedLabel + "\\";
        //        if (Directory.Exists(drivePath))
        //        {
        //            NavigateTo(drivePath, true);
        //        }
        //        return;
        //    }

        //    string nextPath = Path.Combine(currentPath, selectedLabel);
        //    if (Directory.Exists(nextPath))
        //    {
        //        NavigateTo(nextPath, true);
        //    }
        //}

        private long GetDirectorySize(string path, CancellationToken token)
        {
            string cacheKey = NormalizePath(path);
            DirectorySizeCacheEntry cached;
            lock (cacheLock)
            {
                if (directorySizeCache.TryGetValue(cacheKey, out cached) && cached.LastWriteTimeUtc == GetDirectoryLastWriteTimeUtc(path))
                {
                    return cached.Size;
                }
            }

            long size = 0;
            try
            {
                foreach (string file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
                {
                    if (token.IsCancellationRequested)
                    {
                        return 0;
                    }

                    size += new FileInfo(file).Length;
                }
            }
            catch
            {
            }

            lock (cacheLock)
            {
                directorySizeCache[cacheKey] = new DirectorySizeCacheEntry
                {
                    Size = size,
                    LastWriteTimeUtc = GetDirectoryLastWriteTimeUtc(path)
                };
            }

            return size;
        }

        private DateTime GetDirectoryLastWriteTimeUtc(string path)
        {
            try
            {
                return Directory.GetLastWriteTimeUtc(path);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        private string NormalizePath(string path)
        {
            return Path.GetFullPath(path).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        private void LoadCache()
        {
            try
            {
                if (!File.Exists(cacheFilePath))
                {
                    return;
                }

                string directory = Path.GetDirectoryName(cacheFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                foreach (string line in File.ReadAllLines(cacheFilePath))
                {
                    string[] parts = line.Split('|');
                    if (parts.Length != 3)
                    {
                        continue;
                    }

                    if (!long.TryParse(parts[1], out long size) || !long.TryParse(parts[2], out long ticks))
                    {
                        continue;
                    }

                    directorySizeCache[parts[0]] = new DirectorySizeCacheEntry
                    {
                        Size = size,
                        LastWriteTimeUtc = new DateTime(ticks, DateTimeKind.Utc)
                    };
                }
            }
            catch
            {
            }
        }

        private void SaveCache()
        {
            try
            {
                string directory = Path.GetDirectoryName(cacheFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                List<string> lines = new List<string>();
                lock (cacheLock)
                {
                    foreach (KeyValuePair<string, DirectorySizeCacheEntry> entry in directorySizeCache)
                    {
                        lines.Add(entry.Key + "|" + entry.Value.Size + "|" + entry.Value.LastWriteTimeUtc.Ticks);
                    }
                }

                File.WriteAllLines(cacheFilePath, lines.ToArray());
            }
            catch
            {
            }
        }

        private class DirectorySizeCacheEntry
        {
            public long Size { get; set; }
            public DateTime LastWriteTimeUtc { get; set; }
        }

        private long GetDirectorySize(string path)
        {
            return GetDirectorySize(path, CancellationToken.None);
        }
    }
}
