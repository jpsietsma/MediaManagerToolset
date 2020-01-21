using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class EmailConfiguration : IEmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int SmptPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public bool UseSSL { get; set; }
    }
}
