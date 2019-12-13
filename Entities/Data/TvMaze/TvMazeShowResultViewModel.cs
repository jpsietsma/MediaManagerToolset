using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.TvMaze
{
    public class TvMazeShowResultViewModel
    {
        public int Id { get; set; }
        public string? Imdb { get; set; }
        public int? Thetvdb { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Runtime { get; set; }
        //public string? Summary { get; set; }
        public string? Status { get; set; }
        public string? AiringDay { get; set; }
        public string? NetworkName { get; set; }
        public string? AiringTime { get; set; }
        public bool IsExistingShow { get; set; }
    }

}
