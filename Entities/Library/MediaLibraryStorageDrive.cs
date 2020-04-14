using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Library
{
    public class MediaLibraryStorageDrive : LibraryDrive
    {
        public MediaLibraryStorageDrive(double? _driveSpaceTotal, double? _driveSpaceRemaining) : base(_driveSpaceTotal ?? 0, _driveSpaceRemaining ?? 0)
        {

        }
    }
}
