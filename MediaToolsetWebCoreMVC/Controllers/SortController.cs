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

        public async Task<IActionResult> RescanContents()
        {       
            await PopulateTestSort();

            return View("Index", DbContext.SortFiles.ToList());
        }

        private async Task PopulateTestSort()
        {
            //Clear the current queue
            DbContext.SortFiles.RemoveRange(DbContext.SortFiles.ToList());
            await DbContext.SaveChangesAsync();

            List<SortFile> _newfiles = new List<SortFile>
            {
             new SortFile
                {
                    FileName = @"TestShow.S01E01.xViD.MkTz[eztv].mkv",
                    FileSize = 123.56,
                    FilePath = @"S:\TestShow.S01E01.xViD.MkTz[eztv].mkv",
                    DownloadStartDate = DateTime.Now,
                    DownloadSynchronized = null,
                    PriorityLevel = "Unknown"
                },
             new SortFile
                {
                    FileName = @"TestShow.S01E02.xViD.MkTz[eztv].mkv",
                    FileSize = 123.56,
                    FilePath = @"S:\TestShow.S01E02.xViD.MkTz[eztv].mkv",
                    DownloadStartDate = DateTime.Now,
                    DownloadSynchronized = null,
                    PriorityLevel = "Unknown"
                },
             new SortFile
                {
                    FileName = @"TestShow.S01E03.xViD.MkTz[eztv].mkv",
                    FileSize = 123.56,
                    FilePath = @"S:\TestShow.S01E03.xViD.MkTz[eztv].mkv",
                    DownloadStartDate = DateTime.Now,
                    DownloadSynchronized = null,
                    PriorityLevel = "Unknown"
                },
             new SortFile
                {
                    FileName = @"TestShow.S01E04.xViD.MkTz[eztv].mkv",
                    FileSize = 123.56,
                    FilePath = @"S:\TestShow.S01E04.xViD.MkTz[eztv].mkv",
                    DownloadStartDate = DateTime.Now,
                    DownloadSynchronized = null,
                    PriorityLevel = "Unknown"
                },
             new SortFile
                {
                    FileName = @"TestShow.S01E05.xViD.MkTz[eztv].mkv",
                    FileSize = 123.56,
                    FilePath = @"S:\TestShow.S01E05.xViD.MkTz[eztv].mkv",
                    DownloadStartDate = DateTime.Now,
                    DownloadSynchronized = null,
                    PriorityLevel = "Unknown"
                }
            };

            await DbContext.SortFiles.AddRangeAsync(_newfiles);
            await DbContext.SaveChangesAsync();
        }

        private async Task RepopulateSortQueue()
        {

        }
    }
}