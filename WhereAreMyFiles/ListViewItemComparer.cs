using System;
using System.Collections;
using System.Windows.Forms;

namespace WhereAreMyFiles
{
    public class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;

        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }

        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }

        public int Compare(object x, object y)
        {
            int returnVal = -1;

            if (col == 1) // Size column
            {
                long sizeX = GetSizeInTree(((ListViewItem)x).SubItems[col].Text);
                long sizeY = GetSizeInTree(((ListViewItem)y).SubItems[col].Text);
                returnVal = sizeX.CompareTo(sizeY);
            }
            else // Name column
            {
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }

            if (order == SortOrder.Descending)
                returnVal *= -1;

            return returnVal;
        }

        private long GetSizeInTree(string sizeString)
        {
            if (string.IsNullOrEmpty(sizeString)) return 0;
            string numPart = sizeString.Replace(" bytes", "").Trim();
            if (long.TryParse(numPart, out long result))
                return result;
            return 0;
        }
    }
}