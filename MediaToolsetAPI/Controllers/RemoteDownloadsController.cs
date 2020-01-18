using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaToolsetAPI.Controllers
{
    [Route("Sort/[controller]")]
    [ApiController]
    public class RemoteDownloadsController : ControllerBase
    {
        ProgramConfiguration AppSettings;

        public RemoteDownloadsController(ProgramConfiguration _programSettings)
        {
            AppSettings = _programSettings;
        }

        // GET: api/RemoteDownloads
        [HttpGet]
        public string[] Get()
        {
            return Directory.GetFiles(AppSettings.SortConfiguration.RemoteSortDownloadDirectory);
        }
        
    }
}
