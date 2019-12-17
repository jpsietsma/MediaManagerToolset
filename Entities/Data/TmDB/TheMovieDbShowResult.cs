using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.TmDB
{
    public class TheMovieDbShowResult
    {
        public string backdrop_path { get; set; }
        public List<object> created_by { get; set; }
        public List<int> episode_run_time { get; set; }
        public string first_air_date { get; set; }
        public List<TheMovieDbShowGenre> genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public bool in_production { get; set; }
        public List<string> languages { get; set; }
        public string last_air_date { get; set; }
        public TheMovieDbLastEpisode last_episode_to_air { get; set; }
        public string name { get; set; }
        public object next_episode_to_air { get; set; }
        public List<TheMovieDbNetwork> networks { get; set; }
        public int number_of_episodes { get; set; }
        public int number_of_seasons { get; set; }
        public List<string> origin_country { get; set; }
        public string original_language { get; set; }
        public string original_name { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public List<TheMovieDbProductionCompany> production_companies { get; set; }
        public List<TheMovieDbSeason> seasons { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }
}
