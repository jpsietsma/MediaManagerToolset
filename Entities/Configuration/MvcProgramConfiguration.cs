using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class MvcProgramConfiguration
    {
        public MvcProgramConfigurationLogging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public ProgramConfiguration ProgramConfiguration { get; set; }
    }
}
