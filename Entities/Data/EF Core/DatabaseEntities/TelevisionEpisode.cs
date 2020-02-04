using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Data.EF_Core.DatabaseEntities
{
    public class TelevisionEpisode
    {
        public int Id { get; set; }
        public string EpisodeNumber { get; set; }
        public string EpisodePath { get; set; }
        public string ImdbId { get; set; }
        public string TvMazeId { get; set; }
        public string TheMovieDbId { get; set; }

        public int TelevisionShowId { get; set; }
        public TelevisionShow TelevisionShow { get; set; }

        public int TelevisionSeasonId { get; set; }              
        public TelevisionSeason TelevisionSeason { get; set; }

        public TelevisionEpisode()
        {

        }

    }
}
