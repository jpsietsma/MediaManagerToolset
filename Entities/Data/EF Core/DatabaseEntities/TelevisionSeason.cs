using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Data.EF_Core.DatabaseEntities
{
    public class TelevisionSeason
    {
        public int Id { get; set; }
        public string SeasonNumber { get; set; }
        public string SeasonName { get; set; }
        public string SeasonPath { get; set; }
        public string ImdbId { get; set; }
        public string TvMazeId { get; set; }
        public string TheMovieDbId { get; set; }
        
        public int TelevisionShowId { get; set; }
        //public TelevisionShow TelevisionShow { get; set; }

        public virtual List<TelevisionEpisode> TelevisionEpisodes { get; set; }        

        public TelevisionSeason()
        {
            TelevisionEpisodes = new List<TelevisionEpisode>();
        }

    }
}
