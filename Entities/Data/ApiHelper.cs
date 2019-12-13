using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Data
{
    public class ApiHelper
    {
        public string Response { get; set; }
        public string Url { get; set; }

        public ApiHelper(string query, string url = "http://api.tvmaze.com/search/shows")
        {
            Url = url + "?q=" + query;
        }

        public async Task WebApiCall()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method  
                HttpResponseMessage response = await client.GetAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    Response = response.Content.ToString();
                }
            
            }

        }

        public async Task TvMazeCall(TvMazeApiCalls _callType, List<KeyValuePair<string, string>> _params)
        {
            switch (_callType)
            {
                case TvMazeApiCalls.SINGLE_SHOW:
                    {
                        Url = "single";
                        break;
                    }
                    
                case TvMazeApiCalls.RESULTS_SHOW:
                    {
                        Url = "result";
                        break;
                    }

                case TvMazeApiCalls.ADD_FOLLOWED:
                    {
                        Url = "follow show";
                        break;
                    }

                case TvMazeApiCalls.LIST_FOLLOWED:
                    {
                        Url = "list followed shows";
                        break;
                    }

                case TvMazeApiCalls.REMOVE_FOLLOWED:
                    {
                        Url = "remove followed show";
                        break;
                    }
            }

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method  
                HttpResponseMessage response = await client.GetAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    Response = response.Content.ToString();
                }

            }
        }


        public enum TvMazeApiCalls
        {
            SINGLE_SHOW,
            RESULTS_SHOW,
            ADD_FOLLOWED,
            LIST_FOLLOWED,
            REMOVE_FOLLOWED

        }
    }    
}
