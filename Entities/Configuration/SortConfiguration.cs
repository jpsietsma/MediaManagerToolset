using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class SortConfiguration
    {
        public string LocalSortDirectory { get; set; }
        public string LocalSortDownloadDirectory { get; set; }
        public string RemoteSortDirectory { get; set; }
        public string RemoteSortDownloadDirectory { get; set; }
    }
}
