using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Data.EF_Core;
using Entities.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaToolsetWebCoreMVC.Controllers
{
    [Authorize(Roles = "Administrator, SuperAdmin")]
    public class AdministratorController : Controller
    {
        private readonly DatabaseContext DatabaseContext;

        public AdministratorController(DatabaseContext _databaseContext)
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