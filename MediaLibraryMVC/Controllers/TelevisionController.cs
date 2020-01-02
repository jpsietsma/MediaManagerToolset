using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Entities.Configuration;
using Entities.Data;
using Entities.Data.EF_Core;
using Entities.Data.TmDB;
using Entities.Television;
using MediaLibraryMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MediaLibraryMVC.Controllers
{
    public class TelevisionController : Controller
    {
        DatabaseContext DbContext;
        ProgramConfiguration AppSettings;
        IHttpClientFactory HttpClientFactory;

        public TelevisionController(ProgramConfiguration _settings)
        {
            AppSettings = _settings;            
        }

        public IActionResult Index()
        {

            var httpclient = HttpClientFactory.CreateClient();

            return View();            
        }

        public async Task<IActionResult> Library([FromServices] IHttpClientFactory _clientFactory)
        {
            HttpClientFactory = _clientFactory;

            ViewData["Title"] = "Television Shows";
            List<TelevisionShow> _libraryContents = new List<TelevisionShow>();

            //Get our television show library contents using a named httpclient from our injected factory
            var client = HttpClientFactory.CreateClient("SDNTelevisionLibraryQuery");

            var request = new HttpRequestMessage(HttpMethod.Get, client.BaseAddress);
            request.Headers.Add("Accept", "application/json");

            //GET Method  
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                _libraryContents = JsonConvert.DeserializeObject<List<TelevisionShow>>(await response.Content.ReadAsStringAsync()).OrderBy(p => p.ShowName).ToList();

                foreach (TelevisionShow Show in _libraryContents)
                {
                    Show.ShowPath = Show.ShowPath.Replace(@"\\JimmyBeast-sdn\", "");
                    Show.PosterImage = @"https://image.tmdb.org/t/p/w500" + Show.PosterImage;

                    ////Get TheMovieDb IDs for each show
                    //TheMovieDbShowResult result = new TheMovieDbShowResult();

                    //using (var client = new WebClient())
                    //{
                    //    string showname = Show.ShowName;
                    //    string apiKey = "c0604d69b7df230f03504bdc8475887a";

                    //    string RequestUrl = @$"https://api.themoviedb.org/3/search/tv?api_key={ apiKey }&language=en-US&query={ showname }&page=1";
                    //    client.BaseAddress = RequestUrl;

                    //    client.Headers.Clear();
                    //    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                    //    //GET Method
                    //    string response = client.DownloadString(RequestUrl);

                    //    if (JsonConvert.DeserializeObject<TheMovieDbShowSearchResults>(response).results.Count > 0)
                    //    {
                    //        result = JsonConvert.DeserializeObject<TheMovieDbShowSearchResults>(response).results.First();
                    //    }                   
                    //}

                    ////Get external Ids using TheMovieDb ID for each show
                    //using (var client = new WebClient())
                    //{
                    //    if (result != null)
                    //    {
                    //        string apiKey = "c0604d69b7df230f03504bdc8475887a";

                    //        string RequestUrl = @$"https://api.themoviedb.org/3/tv/{ result.id }/external_ids?api_key={ apiKey }&language=en-US&page=1";
                    //        client.BaseAddress = RequestUrl;

                    //        client.Headers.Clear();
                    //        client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                    //        try
                    //        {
                    //            //GET Method  
                    //            string response = client.DownloadString(RequestUrl);

                    //            var x = JsonConvert.DeserializeObject<TheMovieDbExternalIds>(response);
                    //            Show.imdbId = x.imdb_id;
                    //            Show.theMovieDbId = x.id.ToString();
                    //        }
                    //        catch (Exception)
                    //        {

                    //        }

                    //    } 

                    //}
                }
            }            

            ViewBag.DataSource = _libraryContents;

            return View();
        }

        public IActionResult ShowDetails(int _showId)
        {
            ViewBag.ShowName = "Test Show 1";

            TelevisionShow Show = new TelevisionShow();
            Show.ShowName = "Test Show";
            Show.ShowPath = @"\\JimmyBeast-sdn\F\Test Show";

            return View(Show);
        }
    }
}