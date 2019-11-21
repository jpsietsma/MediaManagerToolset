using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public interface IMediaLibrary
    {
        public int Id { get; set; }
        public string LibraryName { get; set; }
        public string LibraryRootPath { get; set; }
        public List<string> LibraryShows { get; set; }


        double LibraryFreeSpace();
    }
}
