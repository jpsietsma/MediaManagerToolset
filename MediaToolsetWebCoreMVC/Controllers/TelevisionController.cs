﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Configuration;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Data.TmDB;
using Entities.Television.ViewModels;
using MediaToolsetWebCoreMVC.Data;
using MediaToolsetWebCoreMVC.Services.MetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MediaToolsetWebCoreMVC.Controllers
{
    [Authorize]
    public class TelevisionController : Controller
    {
        IdentityDatabaseContext DbContext;
        ProgramConfiguration AppSettings;
        IHttpClientFactory HttpClientFactory;
        IMapper AutoMapper;
        IMetaDataApiSvc MetaDataSvc;

        public TelevisionController(MvcProgramConfiguration _settings, IdentityDatabaseContext _context, IMapper _mapper, IMetaDataApiSvc _apiSvc, IHttpClientFactory _clientFactory)
        {
            AppSettings = _settings.ProgramConfiguration;
            DbContext = _context;
            AutoMapper = _mapper;
            MetaDataSvc = _apiSvc;
            HttpClientFactory = _clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Library()
        {
            ViewData["Title"] = "Television Shows";
            List<TelevisionShow> _libraryContents = new List<TelevisionShow>();

            //Get our television show library contents using a named httpclient from our injected factory
            var client = HttpClientFactory.CreateClient("SDNTelevisionLibraryQuery");

            using (var request = new HttpRequestMessage() { Method = HttpMethod.Get })
            {
                request.Headers.Add("Accept", "application/json");
                request.RequestUri = client.BaseAddress;
  
                var response = await client.SendAsync(request);                

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    _libraryContents = JsonConvert.DeserializeObject<List<TelevisionShow>>(content).ToList();

                    foreach (TelevisionShow Show in _libraryContents)
                    {
                        Show.ShowPath = FormatShowPath(Show.ShowPath);
                    }

                    ViewBag.DataSource = _libraryContents.OrderBy(o => o.Id);                    
                }
            }
            return View(_libraryContents);
        }

        public async Task<IActionResult> MovieDbSearch(string id)
        {
            var result = await MetaDataSvc.GetShowResultAsync<TheMovieDbShowSearchResults, TheMovieDbShowResult>(id);

            return View(result);
        }

        public async Task<IActionResult> MovieDbSearchMultiple(string id)
        {
            var results = await MetaDataSvc.GetManyShowResultsAsync<TheMovieDbShowSearchResults, TheMovieDbShowResult>(id);

            ViewBag.DataSource = results;
            ViewBag.ShowQuery = id;

            return View(results);
        }

        public IActionResult ShowDetails(int id)
        {
            //using API:
            //https://api.themoviedb.org/3/tv/3670?api_key=c0604d69b7df230f03504bdc8475887a&language=en-US
            
            
            //using EF core local sql server
            Entities.Data.EF_Core.DatabaseEntities.TelevisionShow Show = DbContext.TelevisionShows.Where(x => x.Id == id).FirstOrDefault();

            return View(Show);
        }                

        private string FormatShowPath(string showPath)
        {
            string final = showPath;

            final = final.Replace(@"\\JIMMYBEAST-SDN\", "");
            final = final.Replace(final[0], char.Parse(final[0].ToString().ToUpper()));
            final = final.Insert(1, @":");

            return final;
        }
    }
}