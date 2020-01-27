using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Configuration;
using Entities.Data.TmDB;
using MediaToolsetWebCoreMVC.Data;
using MediaToolsetWebCoreMVC.Services.MetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MediaToolsetWebCoreMVC.Controllers
{
    public class TelevisionController : Controller
    {
        IdentityDatabaseContext DbContext;
        ProgramConfiguration AppSettings;
        IHttpClientFactory HttpClientFactory;
        IMapper AutoMapper;
        IMetaDataApiSvc MetaDataSvc;

        public TelevisionController(MvcProgramConfiguration _settings, IdentityDatabaseContext _context, IMapper _mapper, IMetaDataApiSvc _apiSvc)
        {
            AppSettings = _settings.ProgramConfiguration;
            DbContext = _context;
            AutoMapper = _mapper;
            MetaDataSvc = _apiSvc;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Library([FromServices] IHttpClientFactory _clientFactory, HttpRequestMessage _requestMessage)
        {
            HttpClientFactory = _clientFactory;

            ViewData["Title"] = "Television Shows";
            List<Entities.Television.ViewModels.TelevisionShowViewModel> _libraryContents = new List<Entities.Television.ViewModels.TelevisionShowViewModel>();

            //Get our television show library contents using a named httpclient from our injected factory
            var client = HttpClientFactory.CreateClient("SDNTelevisionLibraryQuery");

            var libscanClient = HttpClientFactory.CreateClient("TvMazeLibraryScan");

            var request = _requestMessage;
            request.RequestUri = client.BaseAddress;

            //GET Method  
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                _libraryContents = JsonConvert.DeserializeObject<List<Entities.Television.ViewModels.TelevisionShowViewModel>>(await response.Content.ReadAsStringAsync()).OrderBy(p => p.ShowName).ToList();

                foreach (Entities.Television.ViewModels.TelevisionShowViewModel Show in _libraryContents)
                {
                    Show.ShowPath = Show.ShowPath.Replace(@"\\JimmyBeast-sdn\", "");
                    Show.PosterImage = Show.PosterImage;
                    //Show.Seasons.Add(new Entities.Television.ViewModels.TelevisionSeasonViewModel());
                }
            }

            ViewBag.DataSource = _libraryContents;

            return View(_libraryContents);
        }


        [Authorize]
        public async Task<IActionResult> MovieDbSearch()
        {
            var result = await MetaDataSvc.GetShowResultAsync<TheMovieDbShowSearchResults, TheMovieDbShowResult>("FBI");

            return View(result);
        }               

        public IActionResult ShowDetails(int id)
        {
            Entities.Data.EF_Core.DatabaseEntities.TelevisionShow Show = DbContext.TelevisionShowLibrary.Where(x => x.Id == id).FirstOrDefault();

            return View(Show);
        }

    }
}