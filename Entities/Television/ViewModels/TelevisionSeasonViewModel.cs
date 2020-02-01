using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Television.ViewModels
{
    public class TelevisionSeasonViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Season Name")]
        public string SeasonName { get; set; }

        [Display(Name = "Season Number")]
        public string SeasonNumber { get; set; }

        [Display(Name = "TvMaze Season ID")]
        public string TvMazeId { get; set; }

        [Display(Name = "IMDB Season ID")]
        public string ImdbId { get; set; }

        [Display(Name = "TheMovieDb Season ID")]
        public int TheMovieDbId { get; set; }

        [Display(Name = "Episodes Available")]
        public string EpisodeCount { get { return Episodes.Count.ToString(); } }

        public List<TelevisionEpisodeViewModel> Episodes { get; set; } = new List<TelevisionEpisodeViewModel>();

    }
}
