using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Configuration;
using Entities.Configuration.Identity.User;
using Entities.Data.EF_Core;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Services.Sort;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaToolsetWebCoreMVC.Controllers
{
    public class SortController : Controller
    {
        DatabaseContext DbContext;
        ProgramConfiguration AppSettings;
        ISortClassificationSvc SortSvc;
        AuthenticatedUserInfo UserDetails;

        public SortController(DatabaseContext _dbContext, ProgramConfiguration _programSettings, ISortClassificationSvc _sortClassification, AuthenticatedUserInfo _userInfo)
        {
            DbContext = _dbContext;
            AppSettings = _programSettings;
            SortSvc = _sortClassification;
            UserDetails = _userInfo;
        }

        public IActionResult Index()
        {
            List<SortFile> _sortFiles = DbContext.SortFiles.ToList();

            return View(_sortFiles);
        }

        public IActionResult EditSortFile(int Id)
        {
            SortFile SortFile = DbContext.SortFiles.Where(x => x.Id == Id).FirstOrDefault();                                   

            return PartialView("_EditSortFile", SortFile);
        }

        [Authorize(Roles = "Administrator, SuperAdmin, ContentModerator, ContentAdministrator")]
        public IActionResult Dashboard()
        {
            ViewBag.SessionUserDetails = UserDetails;

            return View();            
        }

    }
}