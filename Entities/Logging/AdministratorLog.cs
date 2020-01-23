using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Logging
{
    public class AdministratorLog
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public string UserId { get; set; }
        public string MessagePage { get; set; }
        public string MessageType { get; set; }
        public DateTime MessageDate { get; set; }

    }
}
