using Entities.Abstract;
using Entities.Data.EF_Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Entities.Data.OpenMovieDb
{
    public class OpenMovieDbApiHelper : IApiHelper
    {
        public DatabaseContext DatabaseContext { get; private set; }

        public string Response { get; private set; }
        public string RequestUrl { get; set; }

        public dynamic MakeAPICall(string ImdbQueryId = "6048596", string TheMovieDbQueryId = "44", string language = "en-US")
        {
            dynamic _result = null;

            //
            //
            //
            //Change API key to injected key!
            string api = "bc384b1b";
            //
            //
            //
            using (var client = new WebClient())
            {
                RequestUrl = @$"http://www.omdbapi.com/?apikey={ api }&i=tt" + ImdbQueryId;
                client.BaseAddress = RequestUrl;

                client.Headers.Clear();
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                //GET Method  
                Response = client.DownloadString(RequestUrl);

                _result = JsonConvert.DeserializeObject<OpenMovieDbMovieResult>(Response);
            }

            return _result;
        }

        public enum OpenMovieDbRequestTypes
        {
            MOVIE_INFO
        }
    }
}
