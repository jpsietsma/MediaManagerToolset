using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Sort
{
    public class SortFileInformationViewModel
    {
        public int FileId { get; set; } = 123;
        public string FileName { get; set; } = "Test.Show.Forever.S01E14.XvId.TRUMP.mkv";
        public long FileSize { get; set; } = 1235433632;
        public string FilePath { get; set; } = "S:\\Test.Show.Forever.S01E14";
        public DateTime DownloadStartDate { get; set; } = DateTime.Now;
        public DateTime DateSynchronized { get; set; } = DateTime.Now;
        public string PriorityLevel { get; set; } = "Alpha 1";
        public string SanitizedFileName { get; set; } = "Test.Show.Forever.S01E14.mkv";
        public string SanitizedFilePath { get; set; } = "S:\\Test.Show.Forever.S01E14";
        public string FinalClassificationFilePath { get; set; } = "E:\\TV Shows\\Test Show Forever\\Season 1\\Test.Show.Forever.S01E14.mkv";

        //If Classified as TV Show, show classification information
        public string ShowName { get; set; } = "Test Show Forever";
        public string SeasonNumber { get; set; } = "1";
        public string EpisodeNumber { get; set; } = "14";

        public SortFileInformationViewModel()
        {

        }

    }


}
