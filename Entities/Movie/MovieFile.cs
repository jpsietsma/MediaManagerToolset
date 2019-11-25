using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Movie
{
    public class MovieFile : IClassifiedMediaFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string PriorityLevel { get; set; }

        public Type FileClassification { get; set; }

        public MovieFile()
        {
            FileClassification = this.GetType();
        }

        public virtual bool SanitizeTelevisionEpisode(out string sans) { sans = null; return false; }

        public bool IsFileNameSanitized()
        {
            return false;
        }
    }
}
