using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Television
{
    public class TelevisionShow
    {
        public int Id { get; set; }

        [Display(Name = "Television Show")]
        public string ShowName { get; set; }

        [Display(Name = "Library Path")]
        public string ShowPath { get; set; }

        [Display(Name = "IMDB ID")]
        public string imdbId { get; set; }

        [Display(Name = "TvMaze ID")]
        public string tvMazeId { get; set; }

        [Display(Name = "TheMovieDB ID")]
        public string theMovieDbId { get; set; }

        [Display(Name = "Image Path")]
        public string PosterImage { get; set; }
    }
}
