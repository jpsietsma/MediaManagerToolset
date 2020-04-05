using Entities.Abstract;
using Entities.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration.Services
{
    public class LibraryStorageSvc : ILibraryStorageSvc
    {

        public LibraryStorageSvc()
        {

        }

        public List<MediaLibraryStorageDrive> GetStorageInfo()
        {
            List<MediaLibraryStorageDrive> _storageDrivesFinal = new List<MediaLibraryStorageDrive>();


            //Replace with real code later on, but this will suffice as test data for generating graphs and charts
            //Begin test data
            _storageDrivesFinal.Add(new MediaLibraryStorageDrive
            {
                DriveLetter = "E", 
                DriveName = "E: Television Shows", 
                DriveSpaceTotal = 8192, 
                DriveSpaceRemaining = 1322.56
            });

            _storageDrivesFinal.Add(new MediaLibraryStorageDrive
            {
                DriveLetter = "F",
                DriveName = "F: Television Shows",
                DriveSpaceTotal = 2048,
                DriveSpaceRemaining = 1322.56
            });

            _storageDrivesFinal.Add(new MediaLibraryStorageDrive
            {
                DriveLetter = "G",
                DriveName = "G: Television Shows",
                DriveSpaceTotal = 4096,
                DriveSpaceRemaining = 1322.56
            });

            _storageDrivesFinal.Add(new MediaLibraryStorageDrive
            {
                DriveLetter = "H",
                DriveName = "H: Television Shows",
                DriveSpaceTotal = 8192,
                DriveSpaceRemaining = 1322.56
            });

            _storageDrivesFinal.Add(new MediaLibraryStorageDrive
            {
                DriveLetter = "I",
                DriveName = "I: Television Shows",
                DriveSpaceTotal = 4096,
                DriveSpaceRemaining = 1322.56
            });

            _storageDrivesFinal.Add(new MediaLibraryStorageDrive
            {
                DriveLetter = "M",
                DriveName = "M: Movies",
                DriveSpaceTotal = 1024,
                DriveSpaceRemaining = 67.56
            });

            _storageDrivesFinal.Add(new MediaLibraryStorageDrive
            {
                DriveLetter = "S",
                DriveName = "S: Automation Sort",
                DriveSpaceTotal = 500,
                DriveSpaceRemaining = 23.21
            });
            //End test data

            return _storageDrivesFinal;
        }

    }
}
