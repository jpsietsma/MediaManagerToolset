using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.EF_Core.DatabaseEntities
{
    public class TelevisionShow
    {
        public int Id { get; set; }
        public string TelevisionShowName { get; set; }
        public string TelevisionShowLibraryPath { get; set; }
        public string TvMazeId { get; set; }
        public string ImdbId { get; set; }
    }
}
