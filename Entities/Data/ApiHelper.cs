using Entities.Abstract;
using Entities.Configuration;
using Entities.Data.EF_Core;
using Entities.Data.EF_Core.DatabaseEntities;
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
    public class ApiHelper : IApiHelper
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

        public dynamic MakeAPICall(string ImdbQueryId = "6048596", string TheMovieDbQueryId = "44", string language = "en-US")
        {
            dynamic Result;

            using (var client = new WebClient())
            {
                string RequestUrl = @"http://api.sietsmadevelopment.com/TelevisionLibrary/";
                client.BaseAddress = RequestUrl;

                client.Headers.Clear();
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                //GET Method  
                string response = client.DownloadString(RequestUrl);

                Result = JsonConvert.DeserializeObject<TelevisionShow>(response);
            }

            return Result;
        }
        

    }    
}
