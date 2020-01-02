using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibraryMVC.Models
{
    public class TelevisionShowViewModel
    {
        public int Id { get; set; }
        public int TvMazeId { get; set; }
        public string ImdbId { get; set; }
        public string ShowName { get; set; }
        public string ShowPath { get; set; }
    }
}
