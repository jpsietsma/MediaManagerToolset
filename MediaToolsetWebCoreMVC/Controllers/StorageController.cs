using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaToolsetWebCoreMVC.Controllers
{
    [Authorize]
    public class StorageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Overview()
        {
            var models = GetStorageInfo();

            return View("Overview", models);
        }

        public List<MediaLibraryStorageDrive> GetStorageInfo()
        {
            List<MediaLibraryStorageDrive> _storageDrivesFinal = new List<MediaLibraryStorageDrive>();



            return _storageDrivesFinal;
        }
    }
}