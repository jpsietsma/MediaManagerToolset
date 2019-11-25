using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Entities.Television
{
    public class TelevisionEpisode : IClassifiedMediaFile
    {
        public int Id { get; set; }
        public string ShowName { get; set; }
        public string SeasonNumber { get; set; }
        public string EpisodeNumber { get; set; }

        public string FilePath { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string PriorityLevel { get; set; }

        public Type FileClassification { get; set; }

        public string SanitizedFileName { get; set; }

        public TelevisionEpisode()
        {

        }

        public bool IsFileNameSanitized()
        {
            string TelevisionPattern = @"(?<ShowName>.+)[.][S](?<ShowSeasonNumber>\d\d)[E](?<ShowEpisodeNumber>\d\d)[.](?<ShowFileExtension>mkv|avi|mp4)";

            return Regex.IsMatch(FileName, TelevisionPattern);
        }
    }
}
