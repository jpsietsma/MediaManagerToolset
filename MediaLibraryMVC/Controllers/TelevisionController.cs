using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Entities.Configuration;
using Entities.Data;
using Entities.Data.EF_Core;
using Entities.Data.TmDB;
using Entities.Television;
using MediaLibraryMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MediaLibraryMVC.Controllers
{
    public class TelevisionController : Controller
    {
        DatabaseContext DbContext;
        ProgramConfiguration AppSettings;

        public IActionResult Index(DatabaseContext _context, ProgramConfiguration _settings)
        {
            DbContext = _context;
            AppSettings = _settings;

            return View();            
        }

        public IActionResult Library()
        {            
            List<TelevisionShow> _libraryContents = new List<TelevisionShow>();

            using (var client = new WebClient())
            {
                string RequestUrl = @"http://api.sietsmadevelopment.com/TelevisionLibrary/";
                client.BaseAddress = RequestUrl;

                client.Headers.Clear();
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                //GET Method  
                string response = client.DownloadString(RequestUrl);

                _libraryContents = JsonConvert.DeserializeObject<List<TelevisionShow>>(response);
                _libraryContents = _libraryContents.OrderBy(p => p.ShowName).ToList();
            }

            ViewBag.DataSource = _libraryContents;

            return View();
        }

        public IActionResult ShowDetails(int _showId)
        {
            TelevisionShow Show = new TelevisionShow();
            Show.ShowName = "Test Show";
            Show.ShowPath = @"\\JimmyBeast-sdn\F\Test Show";

            return View(Show);
        }
    }
}