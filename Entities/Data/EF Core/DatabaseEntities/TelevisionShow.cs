using Entities.Television.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Data.EF_Core.DatabaseEntities
{
    public class TelevisionShow
    {
        public int Id { get; set; }
        public string ShowName { get; set; }
        public string ShowPath { get; set; }
        public string tvMazeId { get; set; }
        public string imdbId { get; set; }
        public string theMovieDbId { get; set; }
        public string PosterImage { get; set; }

        //public TelevisionShowAiringSchedule? TelevisionShowAiringSchedule { get; set; }

        public List<TelevisionSeason> TelevisionSeasons { get; set; }

        public TelevisionShow()
        {
            TelevisionSeasons = new List<TelevisionSeason>();
        }

    }

}
