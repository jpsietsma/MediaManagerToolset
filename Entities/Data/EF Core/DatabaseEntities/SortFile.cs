using Entities.Abstract;
using Entities.Enums;
using Entities.Ext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Entities.Data.EF_Core.DatabaseEntities
{  
    public class SortFile : IClassifiableMediaFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public double FileSize { get; set; }
        public string FilePath { get; set; }
        public DateTime DownloadStartDate { get; set; }
        public DateTime? DownloadSynchronized { get; set; }
        public string DownloadStatus { get; set; }
        public string PriorityLevel { get; set; } = "Unknown";        
        public string SanitizedFileName { get; set; }
        public string SanitizedFilePath { get; set; }
        public string FinalClassificationFilePath { get; set; }
        public string SanitizedShowName { get; set; }

        public int? TelevisionShowId { get; set; }
        public TelevisionShow TelevisionShow { get; set; }
        public MediaClassificationTypes ClassificationType { get; set; }

        public SortFile()
        {

        }                
    }
}
