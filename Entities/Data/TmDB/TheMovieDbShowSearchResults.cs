﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.TmDB
{
    public class TheMovieDbShowSearchResults : IApiCallMultipleResultset
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<TheMovieDbShowResult> results { get; set; }

        public dynamic GetResults()
        {
            return results;
        }
    }
}
