using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using Entities.Ext;
using Entities.Television;

namespace Entities.Sort
{
    public class SortQueue
    {
        public ObservableCollection<IMediaFile> CompletedDownloads { get; private set; }
        public ObservableCollection<IMediaFile> DownloadingFiles { get; private set; }
        public string StorageSpaceTotal { get; private set; }        
        public string StorageSpaceRemaining { get; private set; }
        public string DriveLetter { get; private set; }
        public string SortPath { get; set; }       

        /// <summary>
        /// Instantiate an empty SortQueue
        /// </summary>
        public SortQueue()
        {
            CompletedDownloads = new ObservableCollection<IMediaFile>();
            DownloadingFiles = new ObservableCollection<IMediaFile>();

        }

        /// <summary>
        /// Instantiate a new sort queue object based off of the provided path
        /// </summary>
        /// <param name="_path">Root sort folder path to scan</param>
        public SortQueue(string _path, bool _scanDirectory = true)
        {
            CompletedDownloads = new ObservableCollection<IMediaFile>();
            DownloadingFiles = new ObservableCollection<IMediaFile>();

            SortPath = _path;

            if (_scanDirectory)
            {
                PopulateDriveLetter();
                ScanPopulateQueues();

                DriveInfo SortDriveInfo = new DriveInfo(DriveLetter);
                    StorageSpaceTotal = CalculateDriveSpaceString(SortDriveInfo.TotalSize);
                    StorageSpaceRemaining = CalculateDriveSpaceString(SortDriveInfo.AvailableFreeSpace);                                                      
            }
        }      

        /// <summary>
        /// Determine and set DriveLetter property based on SortPath property value
        /// </summary>
        private void PopulateDriveLetter()
        {
            if (string.IsNullOrEmpty(SortPath))
            {
                DriveLetter = null;
            }
            else
            {
                DriveLetter = SortPath.Split(":")[0];
            }
        }

        /// <summary>
        /// Scan and populate the CompletedDownloads property based on SortPath
        /// </summary>
        private void ScanPopulateQueues()
        {

            if (!string.IsNullOrEmpty(SortPath))
            {
                List<FileInfo> _completedDirectoryFiles = new List<FileInfo>();
                List<FileInfo> _downloadingDirectoryFiles = new List<FileInfo>();

                //Scan for completed files in queue
                foreach (string _file in Directory.GetFiles(SortPath))
                {
                    _completedDirectoryFiles.Add(new FileInfo(_file));
                }

                //populate CompletedDownloads property if any results
                if (_completedDirectoryFiles.Count > 0)
                {
                    ObservableCollection<IMediaFile> _mediaFiles = new ObservableCollection<IMediaFile>();

                    foreach (FileInfo _info in _completedDirectoryFiles)
                    {
                        SortFile _file = new SortFile { FileName = _info.Name, FilePath = _info.FullName, FileSize = _info.Length };
                        _file.PopulateFileInfo();

                        if (_file.SanitizeTelevisionEpisode(out string SanitizedfileName, out string SanitizedShowName, out string ShowSeasonNumber, out string ShowEpisodeNumber))
                        {
                            var classification = Type.GetType("Entities.Television.TelevisionEpisode");

                            TelevisionEpisode Episode = new TelevisionEpisode
                            {
                                FileName = _file.FileName,
                                ShowName = SanitizedShowName,
                                SeasonNumber = ShowSeasonNumber,
                                EpisodeNumber = ShowEpisodeNumber,
                                FilePath = _file.FilePath,
                                FileSize = _file.FileSize,
                                PriorityLevel = _file.PriorityLevel,
                                ClassificationType = classification,
                                SanitizedFileName = SanitizedfileName
                            };

                            _mediaFiles.Add(Episode);
                        }
                        else
                        {
                            _mediaFiles.Add(_file);
                        }                        
                    }

                    CompletedDownloads = _mediaFiles;
                }

                //Scan for files being downloaded
                foreach (string _file in Directory.GetFiles(SortPath + "~downloading"))
                {
                    _downloadingDirectoryFiles.Add(new FileInfo(_file));
                }
              
                //populate DownloadingFiles property if any results
                if (_downloadingDirectoryFiles.Count > 0)
                {
                    ObservableCollection<IMediaFile> _mediaFiles = new ObservableCollection<IMediaFile>();

                    foreach (FileInfo _info in _downloadingDirectoryFiles)
                    {
                        SortFile _file = new SortFile { FileName = _info.Name, FilePath = _info.FullName, FileSize = _info.Length };
                        _file.PopulateFileInfo();

                        _mediaFiles.Add(_file);
                    }

                    DownloadingFiles = _mediaFiles;
                }
            }            
        }

        private string CalculateDriveSpaceString(double _storageSpaceValue)
        {
            string _final = string.Empty;

            double sizeMegabytes = Math.Round(_storageSpaceValue / 1000000, 2);
            double sizeGigabytes = Math.Round(sizeMegabytes / 1024, 2);

            _final = sizeGigabytes + $" ({ sizeMegabytes } MB)";


            return _final;
        }
        
    }
}
