using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
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
        IMapper AutoMapper;

        public TelevisionController(ProgramConfiguration _settings, DatabaseContext _context, IMapper _mapper)
        {
            AppSettings = _settings;
            DbContext = _context;
            AutoMapper = _mapper;
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

            return View();
        }


        // Library action method uses API which now uses this method as opposed to scanning directories directly
        //public IActionResult LibraryFromDatabase()
        //{
        //    List<Entities.Television.ViewModels.TelevisionShowViewModel> data = new List<Entities.Television.ViewModels.TelevisionShowViewModel>();

        //    foreach (Entities.Data.EF_Core.DatabaseEntities.TelevisionShow _show in DbContext.TelevisionShowLibrary.ToList())
        //    {
        //        var x = AutoMapper.Map<Entities.Television.ViewModels.TelevisionShowViewModel>(_show);
        //        data.Add(x);
        //    }

        //    ViewBag.DataSource = data;

        //    return View("Library");
        //}

        public IActionResult ShowDetails(int id)
        {
            Entities.Data.EF_Core.DatabaseEntities.TelevisionShow Show = DbContext.TelevisionShowLibrary.Where(x => x.Id == id).FirstOrDefault();

            return View(Show);
        }        

    }
}