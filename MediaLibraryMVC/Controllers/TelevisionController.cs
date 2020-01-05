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

        public TelevisionController(ProgramConfiguration _settings, DatabaseContext _context)
        {
            AppSettings = _settings;
            DbContext = _context;
        }

        public IActionResult Index()
        {

            var httpclient = HttpClientFactory.CreateClient();

            return View();            
        }

        public async Task<IActionResult> Library([FromServices] IHttpClientFactory _clientFactory, HttpRequestMessage _requestMessage)
        {
            HttpClientFactory = _clientFactory;

            ViewData["Title"] = "Television Shows";
            List<TelevisionShow> _libraryContents = new List<TelevisionShow>();

            //Get our television show library contents using a named httpclient from our injected factory
            var client = HttpClientFactory.CreateClient("SDNTelevisionLibraryQuery");

            var mdbClient = HttpClientFactory.CreateClient("TheMovieDBShowQuery");

            var request = _requestMessage;
                request.RequestUri = client.BaseAddress;

            //GET Method  
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                _libraryContents = JsonConvert.DeserializeObject<List<TelevisionShow>>(await response.Content.ReadAsStringAsync()).OrderBy(p => p.ShowName).ToList();

                foreach (TelevisionShow Show in _libraryContents)
                {
                    Show.ShowPath = Show.ShowPath.Replace(@"\\JimmyBeast-sdn\", "");
                    Show.PosterImage = @"https://image.tmdb.org/t/p/w500" + Show.PosterImage;                    
                }
            }            

            ViewBag.DataSource = _libraryContents;

            return View();
        }

        public IActionResult ShowDetails(int id)
        {           
            TelevisionShow Show = DbContext.TelevisionShowLibrary.Where(x => x.Id == id).FirstOrDefault();

            return View(Show);
        }        

    }
}