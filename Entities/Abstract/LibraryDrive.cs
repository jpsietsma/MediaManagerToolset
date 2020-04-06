using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public abstract class LibraryDrive
    {
        public string DriveName { get; set; }
        public string DriveLetter { get; set; }
        public string DriveStatus { get; set; }
        public double DriveSpaceTotal { get; private set; }
        public double DriveSpaceRemaining { get; private set; }
        public double DriveSpaceUsed { get { return (DriveSpaceTotal - DriveSpaceRemaining) > 0 ? (DriveSpaceTotal - DriveSpaceRemaining) : 0; } }

        public LibraryDrive(double _DriveSpaceTotal = 0, double _DriveSpaceRemaining = 0)
        {
            DriveSpaceTotal = _DriveSpaceTotal;
            DriveSpaceRemaining = _DriveSpaceRemaining;
        }
    }
}
