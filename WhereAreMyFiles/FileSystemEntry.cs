using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreMyFiles
{
    internal class FileSystemEntry
    {

        public string FullPath { get; set; }
        public long SizeInBytes { get; set; }
        public string Type { get; set; }
        public bool IsDirectory { get; set; }
        public bool IsHidden { get; set; }


        public void UpdateFullPath(string newFullPath)
        {
            FullPath = newFullPath;
        }

        public void UpdateSizeInBytes(long sizeInBytes) { SizeInBytes = sizeInBytes; }
        public void UpdateIsDirectory(bool isDirectory) {
            IsDirectory = isDirectory;
        }
        public void UpdateIsHidden(bool isHidden) { IsHidden = isHidden; }
        public void UpdateType(string type) { Type = type; }
    }
    
}
