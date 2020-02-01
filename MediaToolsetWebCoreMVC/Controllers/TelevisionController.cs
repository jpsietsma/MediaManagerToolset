using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Configuration;
using Entities.Data.TmDB;
using Entities.Television.ViewModels;
using MediaToolsetWebCoreMVC.Data;
using MediaToolsetWebCoreMVC.Services.MetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MediaToolsetWebCoreMVC.Controllers
{
    public class TelevisionController : Controller
    {
        IdentityDatabaseContext DbContext;
        ProgramConfiguration AppSettings;
        IHttpClientFactory HttpClientFactory;
        IMapper AutoMapper;
        IMetaDataApiSvc MetaDataSvc;

        public TelevisionController(MvcProgramConfiguration _settings, IdentityDatabaseContext _context, IMapper _mapper, IMetaDataApiSvc _apiSvc)
        {
            AppSettings = _settings.ProgramConfiguration;
            DbContext = _context;
            AutoMapper = _mapper;
            MetaDataSvc = _apiSvc;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Library([FromServices] IHttpClientFactory _clientFactory, [FromServices] HttpRequestMessage _requestMessage)
        {
            HttpClientFactory = _clientFactory;
            List<TelevisionSeasonViewModel> seasons = new List<TelevisionSeasonViewModel>();

            ViewData["Title"] = "Television Shows";
            List<TelevisionShowViewModel> _libraryContents = new List<TelevisionShowViewModel>();

            //Get our television show library contents using a named httpclient from our injected factory
            var client = HttpClientFactory.CreateClient("SDNTelevisionLibraryQuery");

            using (var request = _requestMessage)
            {
                request.RequestUri = client.BaseAddress;

                //GET Method  
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    _libraryContents = JsonConvert.DeserializeObject<List<TelevisionShowViewModel>>(await response.Content.ReadAsStringAsync()).OrderBy(p => p.ShowName).ToList();

                    foreach (TelevisionShowViewModel Show in _libraryContents)
                    {
                        seasons = new List<TelevisionSeasonViewModel>();

                        Show.ShowPath = Show.ShowPath.Replace(@"\\\\JIMMYBEAST-SDN\\", "");
                        Show.PosterImage = Show.PosterImage;

                        TelevisionSeasonViewModel vm = new TelevisionSeasonViewModel
                        {
                            Id = 12,
                            SeasonName = "Season 1",
                            SeasonNumber = "1",
                            Episodes = new List<TelevisionEpisodeViewModel>()
                        {
                            new TelevisionEpisodeViewModel
                            {
                                Id = 1,
                                EpisodeNumber = "01",
                                EpisodeName = "Test Episode 1",
                                EpisodeRuntime = "32",
                                EpisodeFilePath = @$"\\JimmyBeast-sdn\e\TV Shows\ {Show.ShowName}\Season 1\{Show.ShowName.Replace(' ', '.')}.S01E01.mkv"
                            },
                            new TelevisionEpisodeViewModel
                            {
                                Id = 2,
                                EpisodeNumber = "02",
                                EpisodeName = "Test Episode 2",
                                EpisodeRuntime = "32",
                                EpisodeFilePath = @$"\\JimmyBeast-sdn\e\TV Shows\ {Show.ShowName}\Season 1\{Show.ShowName.Replace(' ', '.')}.S01E02.mkv"
                            },
                            new TelevisionEpisodeViewModel
                            {
                                Id = 3,
                                EpisodeNumber = "03",
                                EpisodeName = "Test Episode 3",
                                EpisodeRuntime = "32",
                                EpisodeFilePath = @$"\\JimmyBeast-sdn\e\TV Shows\ {Show.ShowName}\Season 1\{Show.ShowName.Replace(' ', '.')}.S01E03.mkv"
                            },
                            new TelevisionEpisodeViewModel
                            {
                                Id = 4,
                                EpisodeNumber = "04",
                                EpisodeName = "Test Episode 4",
                                EpisodeRuntime = "32",
                                EpisodeFilePath = @$"\\JimmyBeast-sdn\e\TV Shows\ {Show.ShowName}\Season 1\{Show.ShowName.Replace(' ', '.')}.S01E04.mkv"
                            },
                            new TelevisionEpisodeViewModel
                            {
                                Id = 5,
                                EpisodeNumber = "05",
                                EpisodeName = "Test Episode 5",
                                EpisodeRuntime = "32",
                                EpisodeFilePath = @$"\\JimmyBeast-sdn\e\TV Shows\ {Show.ShowName}\Season 1\{Show.ShowName.Replace(' ', '.')}.S01E05.mkv"
                            },
                            new TelevisionEpisodeViewModel
                            {
                                Id = 6,
                                EpisodeNumber = "06",
                                EpisodeName = "Test Episode 6",
                                EpisodeRuntime = "32",
                                EpisodeFilePath = @$"\\JimmyBeast-sdn\e\TV Shows\ {Show.ShowName}\Season 1\{Show.ShowName.Replace(' ', '.')}.S01E06.mkv"
                            },
                            new TelevisionEpisodeViewModel
                            {
                                Id = 7,
                                EpisodeNumber = "07",
                                EpisodeName = "Test Episode 7",
                                EpisodeRuntime = "32",
                                EpisodeFilePath = @$"\\JimmyBeast-sdn\e\TV Shows\ {Show.ShowName}\Season 1\{Show.ShowName.Replace(' ', '.')}.S01E07.mkv"
                            },
                            new TelevisionEpisodeViewModel
                            {
                                Id = 8,
                                EpisodeNumber = "08",
                                EpisodeName = "Test Episode 8",
                                EpisodeRuntime = "32",
                                EpisodeFilePath = @$"\\JimmyBeast-sdn\e\TV Shows\ {Show.ShowName}\Season 1\{Show.ShowName.Replace(' ', '.')}.S01E08.mkv"
                            },
                            new TelevisionEpisodeViewModel
                            {
                                Id = 9,
                                EpisodeNumber = "09",
                                EpisodeName = "Test Episode 9",
                                EpisodeRuntime = "32",
                                EpisodeFilePath = @$"\\JimmyBeast-sdn\e\TV Shows\ {Show.ShowName}\Season 1\{Show.ShowName.Replace(' ', '.')}.S01E09.mkv"
                            },
                            new TelevisionEpisodeViewModel
                            {
                                Id = 10,
                                EpisodeNumber = "10",
                                EpisodeName = "Test Episode 10",
                                EpisodeRuntime = "32",
                                EpisodeFilePath = @$"\\JimmyBeast-sdn\e\TV Shows\ {Show.ShowName}\Season 1\{Show.ShowName.Replace(' ', '.')}.S01E010.mkv"
                            },
                            new TelevisionEpisodeViewModel
                            {
                                Id = 11,
                                EpisodeNumber = "11",
                                EpisodeName = "Test Episode 11",
                                EpisodeRuntime = "32",
                                EpisodeFilePath = @$"\\JimmyBeast-sdn\e\TV Shows\ {Show.ShowName}\Season 1\{Show.ShowName.Replace(' ', '.')}.S01E011.mkv"
                            },
                            new TelevisionEpisodeViewModel
                            {
                                Id = 12,
                                EpisodeNumber = "12",
                                EpisodeName = "Test Episode 12",
                                EpisodeRuntime = "32",
                                EpisodeFilePath = @$"\\JimmyBeast-sdn\e\TV Shows\ {Show.ShowName}\Season 1\{Show.ShowName.Replace(' ', '.')}.S01E012.mkv"
                            }
                        }
                        };

                        seasons.Add(vm);
                        Show.Seasons = seasons;
                    }
                }
            }
            
            

            ViewBag.DataSource = _libraryContents;

            return View(_libraryContents);
        }


        [Authorize]
        public async Task<IActionResult> MovieDbSearch(string id)
        {
            var result = await MetaDataSvc.GetShowResultAsync<TheMovieDbShowSearchResults, TheMovieDbShowResult>(id);

            return View(result);
        }

        [Authorize]
        public async Task<IActionResult> MovieDbSearchMultiple(string id)
        {
            var results = await MetaDataSvc.GetManyShowResultsAsync<TheMovieDbShowSearchResults, TheMovieDbShowResult>(id);

            ViewBag.DataSource = results;
            ViewBag.ShowQuery = id;

            return View(results);
        }

        public IActionResult ShowDetails(int id)
        {
            //using API:
            //https://api.themoviedb.org/3/tv/3670?api_key=c0604d69b7df230f03504bdc8475887a&language=en-US
            
            
            //using EF core local sql server
            Entities.Data.EF_Core.DatabaseEntities.TelevisionShow Show = DbContext.TelevisionShowLibrary.Where(x => x.Id == id).FirstOrDefault();

            return View(Show);
        }                
    }
}