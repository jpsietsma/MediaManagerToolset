using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities.Abstract;
using Entities.Configuration;
using Entities.Data.EF_Core;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Sort;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MediaToolsetAPI.Controllers
{    
    [Route("[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        ProgramConfiguration AppSettings;
        DatabaseContext DbContext;

        public SortController(ProgramConfiguration _programSettings, DatabaseContext _databaseContext)
        {
            AppSettings = _programSettings;
            DbContext = _databaseContext;
        }

        // GET: api/Sort
        [HttpGet]
        public List<string> Get()
        {
            string username = "jpsietsma@gmail.com";
            string password = "A!12@lop^6";
            List<string> sortFiles = new List<string>();

            UnMountDrive("\\\\jimmybeast-sdn\\s\\");

            MountDrive('S', "\\\\jimmybeast-sdn\\s\\", username, password);

            foreach (string file in Directory.GetFiles("\\\\jimmybeast-sdn\\s\\"))
            {
                sortFiles.Add(file);
            }

            UnMountDrive("\\\\jimmybeast-sdn\\s\\");

            return sortFiles;
        }

        public int ExecuteCommand(string command, int timeout)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/C " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = "C:\\",
            };

            var process = Process.Start(processInfo);
            process.WaitForExit(timeout);
            var exitCode = process.ExitCode;
            process.Close();
            return exitCode;
        }

        public void UnMountDrive(string directory)
        {
            var command = "NET USE " + directory + " /delete";
            ExecuteCommand(command, 5000);
        }

        public void MountDrive(char letter, string directory, string username, string password)
        {
            var command = @$"NET USE { letter }: { directory } /user: { username } { password }";
            ExecuteCommand(command, 5000);
        }

        public void ScanUpdateSortLibraryDatabase(bool _replaceAll = false)
        {

            if (_replaceAll)
            {
                List<SortFile> newSortFiles = new List<SortFile>();

                // Delete all existing entities from the database on a scan with forced replacement.
                if (DbContext.SortFiles.Count() > 0)
                {
                    DbContext.SortFiles.RemoveRange(DbContext.SortFiles.ToList());
                }
                
                foreach (string sortFile in Directory.GetFiles(AppSettings.SortConfiguration.LocalSortDirectory))
                {
                    newSortFiles.Add(
                        AutoMapper.Map<TelevisionShowViewModel>(
                            new Entities.Television.TelevisionLibraryScannedDirectory(_showDirectory)
                            )
                        );
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
