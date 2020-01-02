using System.Threading.Tasks;

namespace DownloadHelpers
{
    public interface ITorrentDownloadService
    {
        string BaseUrl { get; set; }
        string DestinationFileName { get; set; }
        string DestinationFolder { get; set; }
        string TorrentHash { get; set; }

        void GetTorrentFile(string torrentHash);
    }
}