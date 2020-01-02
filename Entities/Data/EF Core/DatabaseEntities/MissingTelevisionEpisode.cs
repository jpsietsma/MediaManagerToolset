using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.EF_Core.DatabaseEntities
{
    public class MissingTelevisionEpisode
    {
        public int Id { get; set; }
        public int TelevisionShowId { get; set; }
        public int TelevisionSeasonId { get; set; }
        public int TelevisionEpisodeId { get; set; }
        public string TelevisionEpisodeFilePath { get; set; }
        public string TelevisionEpisodeImdbId { get; set; }
        public string TelevisionEpisodeTvMazeId { get; set; }
        public string TelevisionEpisodeTheMovieDbId { get; set; }

    }
}
