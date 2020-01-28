using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.TmDB
{
    public class TheMovieDbMovieSearchResults : IApiCallMultipleResultset
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<TheMovieDbMovieDetails> results { get; set; }

        public dynamic GetResults()
        {
            return results;
        }
    }
}
