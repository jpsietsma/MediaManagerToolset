using Entities.Data.EzTv;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entities.Services.Download
{
    public interface IDownloadAPISvc
    {
        /// <summary>
        /// Return all the Eztv torrent downloads that are available for a given show.
        /// </summary>
        /// <param name="imdbId">IMDB Id of the show</param>
        Task<List<EztvResultTorrentDetails>> GetAvailableShowDownloadsAsync(string imdbId);

        /// <summary>
        /// Return all the Eztv torrent downloads that are available for a given show using TelevisionShows Id
        /// </summary>
        /// <param name="id">Id of the show</param>
        Task<List<EztvResultTorrentDetails>> GetAvailableShowDownloadsByIdAsync(int id);

        /// <summary>
        /// Return all the Eztv torrent downloads that are available for a given show using TelevisionShows Id
        /// </summary>
        /// <param name="showName">Name of the show</param>
        Task<List<EztvResultTorrentDetails>> GetAvailableShowDownloadsByNameAsync(string showName);
    }
}