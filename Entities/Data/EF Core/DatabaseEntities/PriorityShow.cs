using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.EF_Core.DatabaseEntities
{
    public class PriorityShow
    {
        public int Id { get; set; }
        public int TelevisionShowId { get; set; }
        public int PriorityLevelId { get; set; }
        public PriorityLevel PriorityLevel { get; set; }
    }
}
