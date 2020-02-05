using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.EF_Core.DatabaseEntities
{
    public class TelevisionShowAiringSchedule
    {
        public int Id { get; set; }
        public string AiringDay { get; set; }
        public int AiringDOW { get; set; }
        public string AiringTime { get; set; }
        public string AiringNext { get; set; }

        public int TelevisionShowId { get; set; }
        public TelevisionShow TelevisionShow { get; set; }

        public TelevisionShowAiringSchedule()
        {

        }
    }
}
