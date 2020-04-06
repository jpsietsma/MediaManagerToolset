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
            //Replace with real code later on, but this will suffice as test data for generating graphs and charts
            //Begin test data
            List<MediaLibraryStorageDrive> _storageDrivesFinal = new List<MediaLibraryStorageDrive>
            {               
                new MediaLibraryStorageDrive(8192, 1233.56)
                {
                    DriveLetter = "e",
                    DriveName = "Television Shows",
                    DriveStatus = string.Concat("Status: ", "Active ", " Shows")
                },

                new MediaLibraryStorageDrive(2048, 1322.56)
                {
                    DriveLetter = "f",
                    DriveName = "Television Shows",
                    DriveStatus = string.Concat("Status: ", "Active", " Shows")
                },

                new MediaLibraryStorageDrive(4096, 1322.56)
                {
                    DriveLetter = "g",
                    DriveName = "Television Shows",
                    DriveStatus = string.Concat("Status: ", "Ended", " Shows")
                },

                new MediaLibraryStorageDrive(8192, 1322.56)
                {
                    DriveLetter = "h",
                    DriveName = "Television Shows",
                    DriveStatus = string.Concat("Status: ", "Active", " Shows")
                },

                new MediaLibraryStorageDrive(4096, 1322.56)
                {
                    DriveLetter = "i",

                    DriveName = "Television Shows",
                    DriveStatus = string.Concat("Status: ", "Ended", " Shows")
                }
            };
            //End test data

            return _storageDrivesFinal;
        }

    }
}
