using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Entities.Sort
{
    public class SortFileInfo
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string DirectoryName { get; set; }
        public string Extension { get; set; }
        public FileAttributes FileAttributes { get; set; }
        public DateTime CreationTime { get; set; }
        public DirectoryInfo Directory { get; set; }
        public DateTime LastWriteTime { get; set; }
        public long Length { get; set; }

        public SortFileInfo()
        {

        }

    }
}
