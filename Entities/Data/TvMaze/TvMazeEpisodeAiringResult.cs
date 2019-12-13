using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.TvMaze
{
    public class TvMazeEpisodeAiringResult
{
        public string url { get; set; }
        public string name { get; set; }
        public int season { get; set; }
        public int? number { get; set; }
        public string airdate { get; set; }
        public string airtime { get; set; }
        public DateTime airstamp { get; set; }
        public int? runtime { get; set; }
        public string summary { get; set; }
        public TvMazeShowDetails show { get; set; }
    }
}
