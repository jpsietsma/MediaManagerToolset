using System;
using System.Collections.Generic;

namespace DataServices
{
    public abstract class ApiDataService<T>
    {
        public string RawJsonResonse { get; set; }
        public string BaseUrl { get; set; }
        public string RequestUrl { get; set; }


        public abstract T1 GetShowDetails<T1>( string showName);

        public abstract List<T> GetShowEpisodes<T1>(string showName);
        public abstract List<T> GetShowEpisodes<T1>(int seasonNumber);

        public enum ExternalIdSources
        {
            MediaToolset,
            Imdb,
            TheMovieDb,
            TvMaze
        }
    }
}
