using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DownloadHelpers
{
    public abstract class TorrentDownloadService : ITorrentDownloadService
    {
        public string BaseUrl { get; set; }
        public string TorrentHash { get; set; }
        public string DestinationFolder { get; set; }
        public string DestinationFileName { get; set; }

        public abstract void GetTorrentFile(string torrentHash);
    }
}
