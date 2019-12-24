using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class ProgramConfiguration
    {
        public DatabaseConfiguration DatabaseConfiguration { get; set; }
        public MediaAPIKeyConfiguration MediaAPIKeyConfiguration { get; set; }
        public SortConfiguration SortConfiguration { get; set; }
        public TelevisionLibraryConfiguration TelevisionLibraryConfiguration { get; set; }
        public MovieLibraryConfiguration MovieLibraryConfiguration { get; set; }        
        
    }    
    
}
