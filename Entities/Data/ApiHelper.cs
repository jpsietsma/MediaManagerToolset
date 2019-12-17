using Entities.Abstract;
using Entities.Configuration;
using Entities.Data.EF_Core;
using Entities.Data.EzTv;
using Entities.Data.OpenMovieDb;
using Entities.Data.TmDB;
using Entities.Data.TvMaze;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Entities.Data
{
    public abstract class ApiHelper: IApiHelper
    {
        public readonly ProgramConfiguration AppSettings;

        public DatabaseContext DatabaseContext { get; set; }

        public string Response { get; private set; }
        public string RequestUrl { get; set; }

        public ApiHelper(ProgramConfiguration _settings, DatabaseContext _dbContext)
        {
            AppSettings = _settings;
            DatabaseContext = _dbContext;

        }

        public abstract dynamic MakeAPICall(string ImdbQueryId = "6048596", string TheMovieDbQueryId = "44", string language = "en-US");             
    }    
}
