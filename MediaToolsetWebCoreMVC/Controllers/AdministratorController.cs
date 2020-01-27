using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Logging;
using MediaToolsetWebCoreMVC.Areas.Identity.Data;
using MediaToolsetWebCoreMVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaToolsetWebCoreMVC.Controllers
{
    [Authorize(Roles = "Administrator, SuperAdmin")]
    public class AdministratorController : Controller
    {
        private readonly IdentityDatabaseContext DatabaseContext;

        public AdministratorController(IdentityDatabaseContext _databaseContext)
        {
            DatabaseContext = _databaseContext;
        }

        public IActionResult AdminLogs()
        {
            return View(DatabaseContext.GetAdministrationLogs());
        }

        public IActionResult LogDetails(int Id)
        {
            AdministratorLog log = DatabaseContext.GetAdministrationLogs().Where(L => L.Id == Id).FirstOrDefault();

            return PartialView("_LogDetails", log);
        }
    }
}