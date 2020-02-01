using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Television.ViewModels
{
    public class TelevisionEpisodeViewModel
    {
        public int Id { get; set; }
        public string EpisodeName { get; set; }
        public string EpisodeNumber { get; set; }
        public string EpisodeFilePath { get; set; }
        public string EpisodeOverview { get; set; }
        public string EpisodeRuntime { get; set; }
        public bool EpisodeWatched { get; set; }
    }
}
