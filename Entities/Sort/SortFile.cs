using Entities.Abstract;
using Entities.Ext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Entities.Sort
{  
    public class SortFile : IMediaFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FilePath { get; set; }
        public DateTime DownloadStartDate { get; set; }
        public DateTime DateSynchronized { get; set; }
        public string PriorityLevel { get; set; } = "Unscanned - Unknown";
        public Type FileClassification { get; set; } = Type.GetType("Entities.Sort.SortFile");
        public string SanitizedFileName { get; set; }
        public string SanitizedFilePath { get; set; }
        public string FinalClassificationFilePath { get; set; }
        public SortFileInfo SortFileInfo { get; set; }
        public string SanitizedShowName { get; set; }

        public SortFile()
        {

        }

        public SortFile(string SortFilePath)
        {
            FilePath = SortFilePath;

            DateSynchronized = new FileInfo(FilePath).CreationTime;
            SortFileInfo = this.PopulateFileInfo();

            FileName = SortFileInfo.Name;
            FilePath = SortFileInfo.FullName;
            FileSize = SortFileInfo.Length;
        }

        public bool IsFileNameSanitized()
        {
            return false;
        }
    }
}
