using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities.Configuration;
using Entities.Data.EF_Core;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Enums;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<List<SortFile>> Get()
        {
            if (DbContext.SortFiles.ToList().Count == 0)
            {
                await ScanUpdateSortLibraryDatabase();
            }

            return DbContext.SortFiles.ToList();
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

        public async Task ScanUpdateSortLibraryDatabase(bool _replaceAll = false)
        {
            List<SortFile> newSortFiles = new List<SortFile>();

            if (_replaceAll && DbContext.SortFiles.Count() > 0)
            {               
                DbContext.SortFiles.RemoveRange(DbContext.SortFiles.ToList());                              
            }

            foreach (string sortFile in Directory.GetFiles(AppSettings.SortConfiguration.LocalSortDirectory))
            {
                FileInfo fileInfo = new FileInfo(sortFile);

                SortFile newFile = new SortFile
                {
                    ClassificationType = MediaClassificationTypes.UNDETERMINED,
                    DownloadSynchronized = DateTime.Now,
                    FileName = fileInfo.Name,
                    FilePath = fileInfo.FullName,
                    FileSize = fileInfo.Length,
                    DownloadStartDate = fileInfo.CreationTime
                };              
                
                //Add each processed string as a sort file to the newSortFiles
                newSortFiles.Add(newFile);
            }

            DbContext.SortFiles.UpdateRange(newSortFiles);
            await DbContext.SaveChangesAsync();
        }
    }
}