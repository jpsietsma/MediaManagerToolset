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

namespace MediaToolsetAPI.Controllers
{
    [Route("TV/[controller]")]
    [ApiController]
    public class TelevisionLibraryController : ControllerBase
    {
        List<TelevisionShow> _dataList;
        ProgramConfiguration AppSettings;
        DatabaseContext DbContext;
        IMapper AutoMapper;


        public TelevisionLibraryController(ProgramConfiguration _programSettings, DatabaseContext _context, IMapper _mapper)
        {
            AppSettings = _programSettings;
            DbContext = _context;
            AutoMapper = _mapper;

            _dataList = new List<TelevisionShow>();
        }

        // GET: TV/TelevisionLibrary
        [HttpGet]
        public List<TelevisionShow> Get([FromQuery] TelevisionLibraryGridParameters _gridParams)
        {
            if (DbContext.TelevisionShows.Count() == 0)
            {
                ScanUpdateTelevisionLibraryDatabase(true);
            }            

            PopulateDataList(_gridParams);

            return _dataList;                        
        }

        //// GET: TV/TelevisionLibrary/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    if (id == 0)
        //    {
        //        if (DbContext.TelevisionShows.Count() == 0)
        //        {
        //            ScanUpdateTelevisionLibraryDatabase(true);
        //        }

        //        PopulateDataList();

        //        var data = _dataList;
        //        data = data.OrderBy(x => x.ShowName).ToList();

        //        return JsonConvert.SerializeObject(_dataList);
        //    }
        //    else
        //    {
        //        return "null";
        //    }           
        //}
      

        public void PopulateDataList(TelevisionLibraryGridParameters _gridParams)
        {
            _dataList.Clear();

            var shows = DbContext.TelevisionShows
                .OrderBy(x => x.ShowName)
                .Skip((_gridParams.PageNumber - 1) * _gridParams.PageSize)
                .Take(_gridParams.PageSize)
                .Include(seasons => seasons.TelevisionSeasons)
                    .ThenInclude(episodes => episodes.TelevisionEpisodes)
                .ToList();

            foreach (TelevisionShow _show in shows)
            {                
                _dataList.Add(_show);
            }                                       
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
