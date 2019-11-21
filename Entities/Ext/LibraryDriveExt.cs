using Entities.Library;
using System;
using System.IO;
using System.Linq;

namespace Entities.Ext
{
    public static class LibraryDriveExt
    {

        public static double GetFreeDriveSpace(this MediaLibrary _lib)
        {
            double _final = 0.00;

            if (!string.IsNullOrEmpty(_lib.LibraryRootPath))
            {
                string driveLetter = _lib.LibraryRootPath.First().ToString();

                DriveInfo _info = new DriveInfo(driveLetter);

                _final = _info.TotalFreeSpace;
                
            }       
                                 
            return _final;
        }

        public static double GetTotalDriveSpace(this MediaLibrary _lib)
        {
            double _final = 0.00;

            if (!string.IsNullOrEmpty(_lib.LibraryRootPath))
            {
                string driveLetter = _lib.LibraryRootPath.First().ToString();

                DriveInfo _info = new DriveInfo(driveLetter);

                _final = _info.TotalSize;

            }

            return _final;
        }

    }
}
