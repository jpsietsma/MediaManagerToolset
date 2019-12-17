using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DownloadHelpers
{
    public class EztvDownloadRequest : ITorrentDownloadService
    {
        WebClient webClient;

        public string BaseUrl { get; set; }
        public string TorrentHash { get; set; }
        public string DestinationFolder { get; set; }
        public string DestinationFileName { get; set; }
        private string FinalDestination { get { return DestinationFolder + DestinationFileName; } }

        public EztvDownloadRequest(string torrentHash, string downloadLocation = null)
        {
            TorrentHash = torrentHash;
            BaseUrl = @"http://itorrents.org/torrent/";

            if (downloadLocation != null)
            {
                DestinationFileName = downloadLocation.Split("//").Last().Replace(downloadLocation.Split("//").Last().Split(".").Last(), "torrent");
                DestinationFolder = downloadLocation.Replace(downloadLocation.Split("//").Last(), "");
            }
            else
            {
                DestinationFolder = @"S:\";
                DestinationFileName = TorrentHash + ".torrent";
            }

        }


        public void GetTorrentFile(string torrentHash = null)
        {
            string hash = TorrentHash;

            if (!string.IsNullOrEmpty(torrentHash))
            {
                hash = torrentHash;
            }

            Uri _uriFinal = new Uri($@"{ FinalDestination }");
                        
            webClient = new WebClient();

            webClient.DownloadFile(new Uri($@"{ BaseUrl }{ TorrentHash }.torrent"), _uriFinal.ToString());   
            
        }
    }
}
