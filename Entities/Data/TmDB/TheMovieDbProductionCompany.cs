using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.TmDB
{
    public class TheMovieDbProductionCompany
    {
        public int id { get; set; }
        public object logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }
}
