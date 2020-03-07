using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Configuration;
using Entities.Data.EF_Core;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Services.Sort;
using Microsoft.AspNetCore.Mvc;

namespace MediaToolsetWebCoreMVC.Controllers
{
    public class SortController : Controller
    {
        DatabaseContext DbContext;
        ProgramConfiguration AppSettings;
        ISortClassificationSvc SortSvc;

        public SortController(DatabaseContext _dbContext, ProgramConfiguration _programSettings, ISortClassificationSvc _sortClassification)
        {
            DbContext = _dbContext;
            AppSettings = _programSettings;
            SortSvc = _sortClassification;
        }

        public IActionResult Index()
        {
            List<SortFile> _sortFiles = DbContext.SortFiles.ToList();

            return View(_sortFiles);
        }

    }
}