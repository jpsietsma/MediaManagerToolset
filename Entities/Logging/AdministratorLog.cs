using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Logging
{
    public class AdministratorLog
    {
        public int Id { get; set; }

        [Display(Name = "Log Message")]
        public string MessageText { get; set; }

        [Display(Name = "Action User")]
        public string UserName { get; set; }

        [Display(Name = "Action Page")]
        public string MessagePage { get; set; }

        [Display(Name = "Type")]
        public string MessageType { get; set; }

        [Display(Name = "Message Date")]
        public DateTime MessageDate { get; set; }

    }
}
