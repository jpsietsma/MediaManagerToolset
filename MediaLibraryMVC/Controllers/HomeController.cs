using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediaLibraryMVC.Models;
using System.IO;
using Newtonsoft.Json;

namespace MediaLibraryMVC.Controllers
{
    public class HomeController : Controller
    {
        List<string> _dataList;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllShows()
        {
            _dataList = new List<string>();
                _dataList.AddRange(Directory.GetDirectories(@"\\jimmybeast-sdn\E\TV Shows"));
                _dataList.AddRange(Directory.GetDirectories(@"\\jimmybeast-sdn\F\TV Shows"));
                _dataList.AddRange(Directory.GetDirectories(@"\\jimmybeast-sdn\G\TV Shows"));
                _dataList.AddRange(Directory.GetDirectories(@"\\jimmybeast-sdn\H\TV Shows"));
                _dataList.AddRange(Directory.GetDirectories(@"\\jimmybeast-sdn\I\TV Shows"));


            List<JsonViewModel> _modelList = new List<JsonViewModel>();
            JsonViewModel _model = new JsonViewModel();

            _model.JsonData = JsonConvert.SerializeObject(_dataList);

            _modelList.Add(_model);            
            

            return View(_modelList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
