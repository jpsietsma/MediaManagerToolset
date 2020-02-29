using Entities.Configuration;
using Entities.Configuration.Identity.User;
using Entities.Data.EF_Core;
using Entities.Data.TmDB;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MediaToolsetWebCoreMVC.Services.MetaData
{
    public class MetaDataApiSvc : IMetaDataApiSvc
    {
        private readonly IHttpContextAccessor HttpContext;
        private readonly ProgramConfiguration AppSettings;
        private readonly DatabaseContext DatabaseContext;
        private readonly AuthenticatedUserInfo RequestUser;
        private readonly IHttpClientFactory HttpClientFactory;
        //private readonly ILogger Logger;
        private HttpClient CurrentRequestClient;

        public dynamic Result { get; private set; }
        public string RequestUrl { get; private set; }

        public MetaDataApiSvc(IHttpContextAccessor _httpContext, IHttpClientFactory _webclientFactory, ProgramConfiguration _programSettings, DatabaseContext _dbContext, AuthenticatedUserInfo _userInfo)
        {
            HttpContext = _httpContext;
            AppSettings = _programSettings;
            DatabaseContext = _dbContext;
            HttpClientFactory = _webclientFactory;
            //Logger = _logger;
        }

        /// <summary>
        /// Return the information of the search using a show name string
        /// </summary>
        /// <typeparam name="T">Raw API data return typ</typeparam>
        /// <typeparam name="T1">Method data return type</typeparam>
        /// <param name="showName">Show name to query for information</param>
        public async Task<T1> GetShowResultAsync<T,T1>(string showName)
            where T : class
            where T1 : IApiCallResult
        {
            Result = default(T1);
            CurrentRequestClient = HttpClientFactory.CreateClient("TheMovieDBShowQuery");

            using (CurrentRequestClient)
            {
                CurrentRequestClient.BaseAddress = new Uri(CurrentRequestClient.BaseAddress.ToString().Replace("ShowQueryName", showName).Replace("imdb=ShowImDb&", ""));
                RequestUrl = CurrentRequestClient.BaseAddress.ToString();

                try
                {                    
                    using (var clientRequest = await CurrentRequestClient.GetAsync(CurrentRequestClient.BaseAddress))
                    {
                        Result = JsonConvert.DeserializeObject<T>(await clientRequest.Content.ReadAsStringAsync());
                        Result = (Result.results as List<T1>).First();
                    }
                }
                catch (Exception ex)
                {
                    //Logger.LogInformation(new EventId(5000, "Error making API Call"), ex, RequestUser.UserName + " threw an exception while searching for: " + showName);
                }

            }

            //Logger.LogInformation(new EventId(6000, "Successful API call"), null, RequestUser.UserName + " successfully searched for: " + showName);
            return Result;
        }

        /// <summary>
        /// Return the information of the search using an ImdbId int
        /// </summary>
        /// <typeparam name="T">Type to hold our return result information</typeparam>
        /// <param name="TheMovieDbId">Integer Imdb ID to query for information</param>
        public async Task<T1> GetShowResultAsync<T, T1>(int TheMovieDbId)
            where T : class
            where T1 : IApiCallResult
        {
            Result = default(T);
            CurrentRequestClient = HttpClientFactory.CreateClient("TheMovieDBQueryById");

            using (CurrentRequestClient)
            {
                //Include our ImdbId in the base address, and remove default query info about show name from url
                CurrentRequestClient.BaseAddress = new Uri(CurrentRequestClient.BaseAddress.ToString().Replace("ShowId", TheMovieDbId.ToString()));
                RequestUrl = CurrentRequestClient.BaseAddress.ToString();

                try
                {
                    using (var request = await CurrentRequestClient.GetAsync(CurrentRequestClient.BaseAddress))
                    {
                        Result = JsonConvert.DeserializeObject<T>(await request.Content.ReadAsStringAsync());
                    }
                }
                catch (Exception ex)
                {
                    //Logger.LogInformation(new EventId(5000, "Error making API Call"), ex, RequestUser.UserName + " threw an exception while searching using ImdbID: " + ImdbId);
                }
            }

            //Logger.LogInformation(new EventId(6000, "Successful API call"), null, RequestUser.UserName + " successfully searched using Imdb ID: " + ImdbId);
            return Result;
        }

        /// <summary>
        /// Return the information of the search using an ObjectParameter array
        /// </summary>
        /// <typeparam name="T">Type to hold our return result information</typeparam>
        /// <param name="_parameters">Array of object parameters to pass into the query for information</param>
        /// <returns></returns>
        public async Task<T1> GetShowResultAsync<T, T1>(params ObjectParameter[] _parameters) 
            where T : class 
            where T1 : IApiCallResult
        {
            Result = default(T);
            CurrentRequestClient = HttpClientFactory.CreateClient("TheMovieDBShowQuery");

            string showName = _parameters.Where(p => p.Name == "Query").FirstOrDefault().Value.ToString();
            string imdbId = _parameters.Where(p => p.Name == "ImdbId").FirstOrDefault().Value.ToString();

            //check to make sure either show name or imdb is passed as parameter
            if (showName != null || imdbId != null)
            {
                foreach (var parameter in _parameters)
                {

                    //If our parameters include an ImdbId then include that in our base address in our web client for the query
                    //Then remove the show name query info from the base url
                    if (parameter.Name == "ImdbId" && parameter.Value != null)
                    {
                        CurrentRequestClient.BaseAddress = new Uri(CurrentRequestClient.BaseAddress.ToString().Replace("ShowImdDb", imdbId).Replace("query=ShowQueryName&", ""));
                        RequestUrl = CurrentRequestClient.BaseAddress.ToString();
                    }

                    //If our parameters include a show name string to search, and if our ImdbId hasn't already been set as providing this takes priority as it's 
                    //a more exact search, then include that in our base address
                    //Then remove the ImdbId query inform the from base url
                    if (parameter.Name == "Query" && parameter.Value != null && imdbId == null)
                    {
                        CurrentRequestClient.BaseAddress = new Uri(CurrentRequestClient.BaseAddress.ToString().Replace("ShowQueryName", showName).Replace("imdb=ShowImDb&", ""));
                        RequestUrl = CurrentRequestClient.BaseAddress.ToString();
                    }
                }

                using (var client = HttpClientFactory.CreateClient("TheMovieDBShowQuery"))
                {
                    client.BaseAddress = new Uri(client.BaseAddress.ToString().Replace("ShowQueryName", showName));

                    try
                    {
                        using (var request = await client.GetAsync(client.BaseAddress))
                        {
                            Result = JsonConvert.DeserializeObject<T>(await request.Content.ReadAsStringAsync());
                        }
                    }
                    catch (Exception ex)
                    {
                        string searchMethod = showName ?? imdbId;
                        string errorMessage;

                        if (searchMethod == null)
                        {
                            errorMessage = " didn't provide show name or Imdb ID for query";

                            //Logger.LogInformation(new EventId(5000, "Error making API Call"), ex, errorMessage);
                        }

                    }

                }
            }            

            //Logger.LogInformation(new EventId(6000, "Successful API call"), null, RequestUser.UserName + " successfully searched for: " + showName);
            return Result;
        }

        /// <summary>
        /// Return all results of the search using a string show name 
        /// </summary>
        /// <typeparam name="T">Type of API call return</typeparam>
        /// <typeparam name="T1">Type of return result information</typeparam>
        /// <param name="showName">Name of the show to search</param>
        /// <returns></returns>
        public async Task<List<T1>> GetManyShowResultsAsync<T, T1>(string showName) 
            where T: IApiCallMultipleResultset
            where T1: IApiCallResult
        {
            Result = new List<T1>();

            using (var client = HttpClientFactory.CreateClient("TheMovieDBShowQuery"))
            {
                client.BaseAddress = new Uri(client.BaseAddress.ToString().Replace("ShowQueryName", showName).Replace("imdb=ShowImDb&", ""));

                try
                {
                    using (var request = await client.GetAsync(client.BaseAddress))
                    {
                        var requ = (await request.Content.ReadAsStringAsync());
                        var res = JsonConvert.DeserializeObject<T>(await request.Content.ReadAsStringAsync());//.GetResults().Tolist();

                        Result = res.GetResults();
                    }
                }
                catch (Exception ex)
                {                                   
                    //Logger.LogInformation(new EventId(5000, "Error making API Call"), ex, errorMessage);                    
                }

            }
                        
            return Result;
        }

        public async Task<TheMovieDbExternalIds> GetExternalIds(int theMovieDbId)
        {
            CurrentRequestClient = HttpClientFactory.CreateClient("TheMovieDBExternalIDQuery");

            using (CurrentRequestClient)
            {
                CurrentRequestClient.BaseAddress = new Uri(CurrentRequestClient.BaseAddress.ToString().Replace("ShowID", theMovieDbId.ToString()));
                RequestUrl = CurrentRequestClient.BaseAddress.ToString();

                try
                {
                    using (var clientRequest = await CurrentRequestClient.GetAsync(CurrentRequestClient.BaseAddress))
                    {
                        Result = JsonConvert.DeserializeObject<TheMovieDbExternalIds>(await clientRequest.Content.ReadAsStringAsync());
                    }
                }
                catch (Exception ex)
                {
                    //Logger.LogInformation(new EventId(5000, "Error making API Call"), ex, RequestUser.UserName + " threw an exception while searching for: " + showName);
                }

            }

            //Logger.LogInformation(new EventId(6000, "Successful API call"), null, RequestUser.UserName + " successfully searched for: " + showName);
            return Result;

        }

        /// <summary>
        /// Get the HttpClient object from the most recent request.
        /// </summary>
        public HttpClient GetRequestClient()
        {
            return CurrentRequestClient;
        }

    }
}
