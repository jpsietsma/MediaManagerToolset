using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public static class ApiRequestTypes
    {
        public static Dictionary<string, string> GetRequestTypes()
        {
            Dictionary<string, string> _requestTypes = new Dictionary<string, string>();

            string baseUrl = @"https://api.themoviedb.org/3/";
            string apiKey = @"c0604d69b7df230f03504bdc8475887a";
            string language = @"en-US";

            //search various Television endpoints
            _requestTypes.Add("TvMaze_ShowInfo", @$"{ baseUrl }tv/44?api_key={ apiKey }&language={ language }");
            _requestTypes.Add("TvMaze_GetExternalIds", @$"{ baseUrl }tv/44/external_ids?api_key={ apiKey }&language={ language }");
            _requestTypes.Add("TvMaze_GetTvAiringToday", @$"{ baseUrl }tv/airing_today?api_key={ apiKey }&language={ language }&page=1");
            _requestTypes.Add("TvMaze_GetTvStillAiring", @$"{ baseUrl }tv/on_the_air?api_key={ apiKey }&language={ language }&page=1");

            //Search query results
            _requestTypes.Add("TvMaze_TvSearch", @$"{ baseUrl }search/tv?api_key={ apiKey }&language={ language }&query=Cops&page=1");
            _requestTypes.Add("TvMaze_MovieSearch", @$"{ baseUrl }search/movie?api_key={ apiKey }&language={ language }&query=Die%20Hard&page=1&include_adult=false");

            return _requestTypes;
        }
    }
}
