using Entities.Abstract;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.EF_Core.DatabaseEntities
{
    public class Movie : IClassifiableMediaFile
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public MediaClassificationTypes ClassificationType { get; set; } = MediaClassificationTypes.MOVIE;

        public Movie()
        {

        }
    }
}
