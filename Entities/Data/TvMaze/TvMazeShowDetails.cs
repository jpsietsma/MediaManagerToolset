using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.TvMaze
{
    public class TvMazeShowDetails
    {
        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string language { get; set; }
        public List<string> genres { get; set; }
        public string status { get; set; }
        public int? runtime { get; set; }
        public string premiered { get; set; }
        public string officialSite { get; set; }
        public TvMazeShowSchedule schedule { get; set; }
        public int weight { get; set; }
        public TvMazeNetwork network { get; set; }
        public TvMazeExternalShowIds externals { get; set; }
        public TvMazeResultShowImage image { get; set; }
        public string summary { get; set; }
        public int updated { get; set; }
    }
}
