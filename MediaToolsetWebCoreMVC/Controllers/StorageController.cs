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

            return View("Overview", models);
        }

    }
}