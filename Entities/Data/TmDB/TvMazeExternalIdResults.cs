using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.TvMaze
{
    public class TvMazeExternalIdResults
    {
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string freebase_mid { get; set; }
        public string freebase_id { get; set; }
        public int tvdb_id { get; set; }
        public int tvrage_id { get; set; }
        public object facebook_id { get; set; }
        public object instagram_id { get; set; }
        public object twitter_id { get; set; }
    }
}
