using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Abstract;
using Entities.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaToolsetWebCoreMVC.Controllers
{
    [Authorize]
    public class StorageController : Controller
    {
        private readonly ILibraryStorageSvc StorageSvc;

        public StorageController(ILibraryStorageSvc _storageSvc)
        {
            StorageSvc = _storageSvc;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Overview()
        {
            var models = StorageSvc.GetStorageInfo();
            var chartData = new List<StoragePieData>();

            chartData.Add(
                new StoragePieData
                {
                    xValue = "Available",
                    yValue = models.First().DriveSpaceRemaining,
                    text = string.Concat(models.First().DriveSpaceRemaining, " MB")
                });

            ViewBag.DataSource = chartData;
            return View("Overview", models);
        }

    }

    public class StoragePieData
    {
        public string xValue { get; set; }
        public double yValue { get; set; }
        public string text { get; set; }                
    }
}