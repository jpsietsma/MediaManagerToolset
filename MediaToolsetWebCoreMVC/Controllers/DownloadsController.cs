using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Configuration;
using Entities.Data.EzTv;
using Entities.Services.Download;
using Microsoft.AspNetCore.Mvc;

namespace MediaToolsetWebCoreMVC.Controllers
{
    public class DownloadsController : Controller
    {
        ProgramConfiguration AppSettings;
        IDownloadAPISvc TorrentApiSvc;

        public DownloadsController(ProgramConfiguration _appSettings, IDownloadAPISvc _downloadSvc)
        {
            AppSettings = _appSettings;
            TorrentApiSvc = _downloadSvc;
        }

        public async Task<IActionResult> Index(string Id)
        {
            List<EztvResultTorrentDetails> DownloadsAvailable = await TorrentApiSvc.GetAvailableShowDownloadsAsync(Id);

            return PartialView("_DownloadsAvailable", DownloadsAvailable);
        }
    }
}