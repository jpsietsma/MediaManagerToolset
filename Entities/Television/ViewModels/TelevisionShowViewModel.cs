using Entities.Data.EF_Core.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Television.ViewModels
{
    public class TelevisionShowViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Television Show")]
        public string ShowName { get; set; }

        [Display(Name = "Library Path")]
        public string ShowPath { get; set; }

        [Display(Name = "TvMaze Show ID")]
        public string TvMazeId { get; set; }

        [Display(Name = "IMDB Show ID")]
        public string ImdbId { get; set; }

        [Display(Name = "Show Genre")]
        public string ShowGenre { get; set; }

        [Display(Name = "TheMovieDb Show ID")]
        public string TheMovieDbId { get; set; }

        [Display(Name = "Poster URL")]
        public string PosterImage { get; set; }

        public List<Data.EF_Core.DatabaseEntities.TelevisionSeason> TelevisionSeasons { get; set; }

        public TelevisionShowViewModel()
        {

        }

    }
}
