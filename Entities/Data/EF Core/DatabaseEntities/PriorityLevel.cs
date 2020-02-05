using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.EF_Core.DatabaseEntities
{
    public class PriorityLevel
    {
        public int Id { get; set; }
        public string Prioritylevel { get; set; }
        public int PriorityLevelCode { get; set; }
    }
}
