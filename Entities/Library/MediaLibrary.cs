using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Library
{
    public class MediaLibrary : IMediaLibrary
    {
        public int Id { get; set; }
        public string LibraryName { get; set; }
        public string LibraryRootPath { get; set; }
        public List<string> LibraryShows { get; set; }

        public MediaLibrary()
        {

        }

        public double LibraryFreeSpace()
        {
            double _final = 0.00;




            return _final;
        }


    }


}
