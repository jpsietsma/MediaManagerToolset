using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public abstract class LibraryDrive
    {
        public string DriveName { get; set; }
        public string DriveLetter { get; set; }
        public double DriveSpaceTotal { get; set; }
        public double DriveSpaceRemaining { get; set; }

    }
}
