﻿using Entities.Library;
using System;
using System.IO;

namespace Entities.Ext
{
    public static class LibraryDriveExt
    {

        public static double GetDriveSpace(this MediaLibrary _lib, string _driveLetter)
        {
            DriveInfo _info = new DriveInfo(_driveLetter);
            double _final = _info.AvailableFreeSpace;

            return _final;
        }
    }
}
