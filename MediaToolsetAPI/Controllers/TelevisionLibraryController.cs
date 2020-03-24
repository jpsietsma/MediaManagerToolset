using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using AutoMapper;
using Entities.Configuration;
using Entities.Data.EF_Core;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Television.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Entities.Configuration.Grid;
using Entities.Custom;

namespace MediaToolsetAPI.Controllers
{
    [Route("TV/[controller]")]
    [ApiController]
    public class TelevisionLibraryController : ControllerBase
    {
        PagedList<TelevisionShow> _dataList;
        ProgramConfiguration AppSettings;
        DatabaseContext DbContext;
        IMapper AutoMapper;


        public TelevisionLibraryController(ProgramConfiguration _programSettings, DatabaseContext _context, IMapper _mapper)
        {
            AppSettings = _programSettings;
            DbContext = _context;
            AutoMapper = _mapper;
        }

        // GET: TV/TelevisionLibrary
        [HttpGet]
        public PagedList<TelevisionShow> Get([FromQuery] TelevisionLibraryGridParameters _gridParams)
        {
            if (DbContext.TelevisionShows.Count() == 0)
            {
                ScanUpdateTelevisionLibraryDatabase(true);
            }                     

            var results = GetPagedResults(_gridParams);

            Response.Headers.Add("X-Pagination", results.GetPagedMetaData());

            return results;                        
        }                     

        public PagedList<TelevisionShow> GetPagedResults(TelevisionLibraryGridParameters _gridParams)
        {
            return PagedList<TelevisionShow>
                            .ToPagedList(DbContext.TelevisionShows
                                .OrderBy(x => x.ShowName)                
                                .Include(seasons => seasons.TelevisionSeasons)
                                    .ThenInclude(episodes => episodes.TelevisionEpisodes),
                            _gridParams.PageNumber,
                            _gridParams.PageSize);
        }
               
        public void ScanUpdateTelevisionLibraryDatabase(bool _replaceAll = false)
        {      
            
            if (_replaceAll)
            {
                List<TelevisionShowViewModel> modelList = new List<TelevisionShowViewModel>();
                List<TelevisionShow> newShows = new List<TelevisionShow>();

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
                                new Entities.Television.TelevisionLibraryScannedDirectory(_showDirectory)
                                )
                            );
                    }                    
                }

                foreach (TelevisionShowViewModel vm in modelList)
                {
                    newShows.Add(AutoMapper.Map<TelevisionShow>(vm));
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
