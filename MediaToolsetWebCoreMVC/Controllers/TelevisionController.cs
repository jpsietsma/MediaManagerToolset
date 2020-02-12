using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Configuration;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Data.TmDB;
using MediaToolsetWebCoreMVC.Data;
using MediaToolsetWebCoreMVC.Services.MetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MediaToolsetWebCoreMVC.Controllers
{
    [Authorize]
    public class TelevisionController : Controller
    {
        IdentityDatabaseContext DbContext;
        ProgramConfiguration AppSettings;
        IHttpClientFactory HttpClientFactory;
        IMapper AutoMapper;
        IMetaDataApiSvc MetaDataSvc;

        public TelevisionController(MvcProgramConfiguration _settings, IdentityDatabaseContext _context, IMapper _mapper, IMetaDataApiSvc _apiSvc, IHttpClientFactory _clientFactory)
        {
            AppSettings = _settings.ProgramConfiguration;
            DbContext = _context;
            AutoMapper = _mapper;
            MetaDataSvc = _apiSvc;
            HttpClientFactory = _clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Library()
        {
            ViewData["Title"] = "Television Shows";

            return View(await GetLibraryContents());
        }

        public async Task<IActionResult> MovieDbSearch(string id)
        {
            var result = await MetaDataSvc.GetShowResultAsync<TheMovieDbShowSearchResults, TheMovieDbShowResult>(id);

            return View(result);
        }

        public async Task<IActionResult> MovieDbSearchMultiple(string id)
        {
            var results = await MetaDataSvc.GetManyShowResultsAsync<TheMovieDbShowSearchResults, TheMovieDbShowResult>(id);

            ViewBag.DataSource = results;
            ViewBag.ShowQuery = id;

            return View(results);
        }

        public async Task<IActionResult> ShowDetails(int Id)
        {
            TelevisionShow Show = DbContext.TelevisionShows.Where(x => x.Id == Id).FirstOrDefault();
            TheMovieDbShowResult result = await MetaDataSvc.GetShowResultAsync<TheMovieDbShowSearchResults, TheMovieDbShowResult>(Show.ShowName);

            //If our show doesn't have an IMDB Id set, then set it here and save the changes to the database
            //This makes for simpler calls later on as we known the value will be present.
            if (Show.theMovieDbId == null)
            {
                Show.theMovieDbId = result.id.ToString();
                DbContext.TelevisionShows.Update(Show);

                await DbContext.SaveChangesAsync();
            }

            ViewBag.ShowInfo = result;

            return PartialView("_ShowDetails", Show);
        }                

        public IActionResult SeasonDetails(int id)
        {
            //using EF core sql server
            TelevisionSeason Season = DbContext.TelevisionSeasons.Where(s => s.Id == id).FirstOrDefault();

            return PartialView("_SeasonDetails", Season);
        }

        public IActionResult EpisodeDetails(int id)
        {
            //using EF core sql server
            TelevisionEpisode Episode = DbContext.TelevisionEpisodes.Where(e => e.Id == id).FirstOrDefault();

            return PartialView("_EpisodeDetails", Episode);
        }

        [Authorize(Roles = "SuperAdmin, Administrator, ContentAdministrator")]
        public async Task<IActionResult> RemoveShow(int id)
        {
            TelevisionShow show = DbContext.TelevisionShows.Where(s => s.Id == id).FirstOrDefault();
            
            if (show != null)
            {
                try
                {
                    DbContext.TelevisionShows.Remove(show);
                    await DbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        throw new Exception(ex.InnerException.Message);
                    }

                    throw new Exception(ex.Message);
                }
            }

            return View("Library", await GetLibraryContents());
        }

        public async Task<IActionResult> RescanLibrary()
        {
            //Clean out the existing Shows, Seasons, and Episodes in database
            DbContext.TelevisionEpisodes.RemoveRange(DbContext.TelevisionEpisodes.ToList());
            DbContext.TelevisionSeasons.RemoveRange(DbContext.TelevisionSeasons.ToList());
            DbContext.TelevisionShows.RemoveRange(DbContext.TelevisionShows.ToList());                     
            await DbContext.SaveChangesAsync();

            var dirs = AppSettings.TelevisionLibraryConfiguration.TelevisionLibrary.LibraryFolders;
            var shows = new List<TelevisionShow>();

            //Rescan our directories for shows 
            foreach (var dir in dirs)
            {
                //then create them as TelevisionShow objects
                foreach (var show in Directory.GetDirectories(dir))
                {
                    TelevisionShow newShow = new TelevisionShow
                    {
                        ShowName = show.Split('\\').Last(),
                        ShowPath = show
                    };

                    newShow.TelevisionSeasons = new List<TelevisionSeason>();

                    //then create each season as a TelevisionSeason object
                    foreach (var season in Directory.GetDirectories(show))
                    {
                        TelevisionSeason newSeason = new TelevisionSeason
                        {
                            SeasonNumber = season.Split('\\').Last().Replace("Season ", ""), 
                            SeasonPath = season,
                            TelevisionShowId = newShow.Id
                        };
                        
                        newSeason.TelevisionEpisodes = new List<TelevisionEpisode>();

                        //then create each episode as a TelevisionEpisode object
                        foreach (var episode in Directory.GetFiles(season))
                        {
                            Regex epNum = new Regex(@"E\d\d");

                            TelevisionEpisode newEpisode = new TelevisionEpisode
                            {
                                EpisodePath = episode,
                                EpisodeNumber = epNum.Match(episode.Split('\\').Last()).Value.Replace("E", "").Replace(01.ToString(), "1"),
                                TelevisionSeasonId = newSeason.Id
                            };

                            newSeason.TelevisionEpisodes.Add(newEpisode);
                        }

                        newSeason.TelevisionEpisodes = newSeason.TelevisionEpisodes.OrderBy(o => o.EpisodeNumber).ToList();
                        newShow.TelevisionSeasons.Add(newSeason);
                    }

                    shows.Add(newShow);
                }               
            }

            //Add our new objects to the database and try to save them
            try
            {                    
                await DbContext.AddRangeAsync(shows);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw new Exception(ex.InnerException.Message);
                }

                throw new Exception(ex.Message);
            }                      

            return View("Library", await GetLibraryContents());
        }        

        private string FormatShowPath(string showPath)
        {
            string final = showPath;

            final = final.Replace(@"\\JIMMYBEAST-SDN\", "");

            if (final[1] != ':')
            {
                final = final.Insert(1, @":");
            }
            
            final = UppercaseFirst(final);

            return final;
        }

        private string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
                       
        private async Task<List<TelevisionShow>> GetLibraryContents()
        {
            List<TelevisionShow> _libraryContents = new List<TelevisionShow>();

            //Get our television show library contents using a named httpclient from our injected factory
            var client = HttpClientFactory.CreateClient("SDNTelevisionLibraryQuery");

            using (var request = new HttpRequestMessage() { Method = HttpMethod.Get })
            {
                request.Headers.Add("Accept", "application/json");
                request.RequestUri = client.BaseAddress;

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    _libraryContents = JsonConvert.DeserializeObject<List<TelevisionShow>>(content).ToList();

                    foreach (TelevisionShow Show in _libraryContents)
                    {
                        Show.ShowPath = FormatShowPath(Show.ShowPath);
                    }                    
                }
            }

            return _libraryContents.OrderBy(o => o.Id).ToList();
        }
    }
}