using Entities.Data.TvMaze;
using Newtonsoft.Json;
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
        private string query = string.Empty;

        public ApiHelper(string _query, string url = "http://api.tvmaze.com/search/shows")
        {
            query = _query;
            Url = url + "?q=" + _query;
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
    }    
}
