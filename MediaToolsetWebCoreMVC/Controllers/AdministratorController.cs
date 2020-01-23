using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}