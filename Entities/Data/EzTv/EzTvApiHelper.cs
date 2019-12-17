using Entities.Abstract;
using Entities.Data.EF_Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Entities.Data.EzTv
{
    public class EzTvApiHelper : IApiHelper
    {
        public DatabaseContext DatabaseContext { get; set; }

        public string Response { get; private set; }

        public string RequestUrl { get; set; }

        public dynamic MakeAPICall(string ImdbQueryId = "6048596", string TheMovieDbQueryId = "44", string language = "en-US")
        {
            dynamic _result = null;
            
            using (var client = new WebClient())
            {
                RequestUrl = @"https://eztv.io/api/get-torrents?imdb_id=" + ImdbQueryId;
                client.BaseAddress = RequestUrl;

                client.Headers.Clear();
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                //GET Method  
                Response = client.DownloadString(RequestUrl);

                _result = JsonConvert.DeserializeObject<EztvResult>(Response);
            }

            return _result;
        }

        public enum ApiRequestTypes
        {
            GET_ALLTORRENTS,
            GET_ALLTORRENTS_ID
        }
    }
}
