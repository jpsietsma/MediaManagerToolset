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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MediaLibraryMVC.Controllers
{
    public class HomeController : Controller
    {
        List<TelevisionShowViewModel> _dataList;
        WebClient _webClient;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _dataList = new List<TelevisionShowViewModel>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllShows()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://api.sietsmadevelopment.com/");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync($@"TelevisionLibrary").Result;
            if (response.IsSuccessStatusCode)
            {
                string str = response.Content.ReadAsStringAsync().Result;
                _dataList = JsonConvert.DeserializeObject<List<TelevisionShowViewModel>>(str);
            }

            ViewBag.dataSource = _dataList;

            return View();
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
