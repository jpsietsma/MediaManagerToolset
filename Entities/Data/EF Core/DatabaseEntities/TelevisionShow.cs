using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Data.EF_Core.DatabaseEntities
{
    public class TelevisionShow
    {
        public int Id { get; set; }

        [Display(Name = "Television Show")]
        public string TelevisionShowName { get; set; }

        [Display(Name = "Library Path")]
        public string TelevisionShowLibraryPath { get; set; }

        [Display(Name = "TvMaze Show ID")]
        public string TvMazeId { get; set; }

        [Display(Name = "IMDB Show ID")]
        public string ImdbId { get; set; }
    }
}
