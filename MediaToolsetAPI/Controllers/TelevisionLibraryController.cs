using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Configuration;
using Entities.Data.EF_Core;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Television;
using Entities.Television.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MediaToolsetAPI.Controllers
{
    [Route("TV/[controller]")]
    [ApiController]
    public class TelevisionLibraryController : ControllerBase
    {
        List<Entities.Data.EF_Core.DatabaseEntities.TelevisionShow> _dataList;
        ProgramConfiguration AppSettings;
        DatabaseContext DbContext;
        IMapper AutoMapper;


        public TelevisionLibraryController(ProgramConfiguration _programSettings, DatabaseContext _context, IMapper _mapper)
        {
            AppSettings = _programSettings;
            DbContext = _context;
            AutoMapper = _mapper;

            _dataList = new List<Entities.Data.EF_Core.DatabaseEntities.TelevisionShow>();
        }

        // GET: api/TelevisionLibrary
        [HttpGet]
        public List<Entities.Data.EF_Core.DatabaseEntities.TelevisionShow> Get()
        {
            if (DbContext.TelevisionShows.Count() == 0)
            {
                ScanUpdateTelevisionLibraryDatabase(true);
            }            

            PopulateDataList();

            var data = _dataList;
            data = data.OrderBy(x => x.ShowName).ToList();

            return data;

            //             maybe try this?
            //
            //    JsonSerializerSettings config = new JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore };
            //   this.json = JsonConvert.SerializeObject(YourObject, Formatting.Indented, config);
            //
            //
        }

        // GET: api/TelevisionLibrary/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            if (id == 0)
            {
                if (DbContext.TelevisionShows.Count() == 0)
                {
                    ScanUpdateTelevisionLibraryDatabase(true);
                }

                PopulateDataList();

                var data = _dataList;
                data = data.OrderBy(x => x.ShowName).ToList();

                return JsonConvert.SerializeObject(_dataList);
            }
            else
            {
                return "null";
            }

            //TelevisionShowViewModel vm = _dataList.Where(x => x.Id == id).FirstOrDefault();
            //return JsonConvert.SerializeObject(vm);
        }
      

        public void PopulateDataList()
        {
            var shows = DbContext.TelevisionShows.ToList();

            foreach (Entities.Data.EF_Core.DatabaseEntities.TelevisionShow _show in shows)
            {
                var seasons = DbContext.TelevisionSeasons.Where(s => s.TelevisionShowId == _show.Id).ToList();
                _show.TelevisionShowAiringSchedule = DbContext.TelevisionShowAiringSchedules.Where(s => s.TelevisionShowId == _show.Id).FirstOrDefault();

                foreach (Entities.Data.EF_Core.DatabaseEntities.TelevisionSeason _season in seasons)
                {
                    _season.TelevisionEpisodes = DbContext.TelevisionEpisodes.Where(e => e.TelevisionSeasonId == _season.Id).ToList();
                }

                _show.TelevisionSeasons = seasons;
                _dataList.Add(_show);
            }                                       
        }
               
        public void ScanUpdateTelevisionLibraryDatabase(bool _replaceAll = false)
        {      
            
            if (_replaceAll)
            {
                List<TelevisionShowViewModel> modelList = new List<TelevisionShowViewModel>();
                List<Entities.Data.EF_Core.DatabaseEntities.TelevisionShow> newShows = new List<Entities.Data.EF_Core.DatabaseEntities.TelevisionShow>();

                //    Code will delete all existing entities from the database on a scan with forced replacement.
                    if (DbContext.TelevisionShows.Count() > 0)
                    {
                        DbContext.TelevisionShows.RemoveRange(DbContext.TelevisionShows.ToList());                        
                    }
                              

                foreach (string _showLibraryDriveRoot in AppSettings.TelevisionLibraryConfiguration.TelevisionLibrary.LibraryFolders)
                {
                    foreach (string _showDirectory in Directory.GetDirectories(_showLibraryDriveRoot))
                    {
                        modelList.Add(
                            AutoMapper.Map<TelevisionShowViewModel>(
                                new TelevisionLibraryScannedDirectory(_showDirectory)
                                )
                            );
                    }                    
                }

                foreach (TelevisionShowViewModel vm in modelList)
                {
                    newShows.Add(AutoMapper.Map<Entities.Data.EF_Core.DatabaseEntities.TelevisionShow>(vm));
                }

                DbContext.TelevisionShows.AddRange(newShows);
                DbContext.SaveChanges();                

            }
            else
            {
                //logic for when we want to only add new show entries to the library without truncating current library contents.
            }
            
        }

    }
}
