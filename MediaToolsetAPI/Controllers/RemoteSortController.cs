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
    public class RemoteSortController : ControllerBase
    {
        ProgramConfiguration AppSettings;

        public RemoteSortController(ProgramConfiguration _programSettings)
        {
            AppSettings = _programSettings;
        }

        // GET: api/RemoteSort
        [HttpGet]
        public string[] Get()
        {           
            return Directory.GetFiles(AppSettings.SortConfiguration.RemoteSortDirectory);
        }
        
    }
}
