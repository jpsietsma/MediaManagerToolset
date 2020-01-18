using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities.Abstract;
using Entities.Configuration;
using Entities.Sort;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MediaToolsetAPI.Controllers
{    
    [Route("[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        ProgramConfiguration AppSettings;

        public SortController(ProgramConfiguration _programSettings)
        {
            AppSettings = _programSettings;
        }

        // GET: api/Sort
        [HttpGet]
        public string[] Get()
        {
            return Directory.GetFiles(AppSettings.SortConfiguration.LocalSortDirectory);
        }


    }
}
