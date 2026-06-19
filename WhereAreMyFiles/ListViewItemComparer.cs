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

        private long DetermineTrueSize(ListViewItem x)
        {
            // Object will be a ListViewItem, we get the true size from the SizeInBytes property of the FileSystemEntry object stored in the Tag property of the ListViewItem
            return x.Tag is FileSystemEntry entry ? entry.SizeInBytes : 0;
        }

        public int Compare(object x, object y)
        {
            int returnVal = -1;

            switch (col)
            {
                /*
                 * case 0: name column, compare as strings
                 * case 1: size column, compare as numbers (after parsing the " bytes" suffix)
                 * case 2: date modified column, compare as DateTime (after parsing the string)
                 * case 3: type column, compare as strings
                 */
                case 0: 
                    returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                    break;
                case 1:
                    long sizeX = GetSizeInTree(((ListViewItem)x).SubItems[col].Text);
                    long sizeY = GetSizeInTree(((ListViewItem)y).SubItems[col].Text);
                    returnVal = DetermineTrueSize((ListViewItem)x).CompareTo(DetermineTrueSize((ListViewItem)y));
                    break;
                case 2:
                    DateTime DateX, DateY;
                    DateX = DateTime.Parse(((ListViewItem)x).SubItems[col].Text);
                    DateY = DateTime.Parse(((ListViewItem)y).SubItems[col].Text);
                    returnVal = DateX.CompareTo(DateY);
                    break;
                case 3:
                    returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                    break;

            }
                    

            //if (col == 1) // Size column
            //{
            //    long sizeX = GetSizeInTree(((ListViewItem)x).SubItems[col].Text);
            //    long sizeY = GetSizeInTree(((ListViewItem)y).SubItems[col].Text);
            //    returnVal = sizeX.CompareTo(sizeY);
            //}
            //else // Name column
            //{
                
            //}

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