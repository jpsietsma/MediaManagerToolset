using Entities.Data.EF_Core;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Data.EzTv;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Services.Download
{
    public class EztvDownloadAPISvc : IDownloadAPISvc
    {
        private IHttpClientFactory HttpClientFactory;
        private DatabaseContext DbContext;

        public EztvDownloadAPISvc(DatabaseContext _dbContext, IHttpClientFactory _clientFactory)
        {
            DbContext = _dbContext;
            HttpClientFactory = _clientFactory;
        }

        //ability to limit data send back with pagination using ?limit=# page=#
        //tie this to grid pagination

        /// <summary>
        /// Return all the Eztv torrent downloads that are available for a given show.
        /// </summary>
        /// <param name="imdbId">IMDB Id of the show</param>
        public async Task<List<EztvResultTorrentDetails>> GetAvailableShowDownloadsAsync(string imdbId)
        {
            List<EztvResultTorrentDetails> availableDownloads = new List<EztvResultTorrentDetails>();

            await Task.Run(async () =>
            {
                lock(availableDownloads);

                var client = HttpClientFactory.CreateClient("EztvShowDownloads");

                using (client)
                {
                    client.BaseAddress = new Uri(client.BaseAddress.ToString().Replace("IMDBID", imdbId));

                    try
                    {
                        using (var clientRequest = await client.GetAsync(client.BaseAddress))
                        {
                            var results = JsonConvert.DeserializeObject<EztvResult>(await clientRequest.Content.ReadAsStringAsync());

                            availableDownloads = results.torrents;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Logger.LogInformation(new EventId(5000, "Error making API Call"), ex, RequestUser.UserName + " threw an exception while searching for: " + showName);
                    }
                }                
            });

            //Filter down list to include only one download per episode
            var tmpDownloads = new List<EztvResultTorrentDetails>();

            foreach (EztvResultTorrentDetails download in availableDownloads)
            {
                if (tmpDownloads.Where(d => d.season == download.season && d.episode == download.episode).Count() < 1)
                {
                    tmpDownloads.Add(download);
                }                                
            }

            return tmpDownloads;
            //return availableDownloads;
        }

        /// <summary>
        /// Return all the Eztv torrent downloads that are available for a given show using TelevisionShows Id
        /// </summary>
        /// <param name="id">Id of the show</param>
        public async Task<List<EztvResultTorrentDetails>> GetAvailableShowDownloadsByIdAsync(int id)
        {
            List<EztvResultTorrentDetails> availableDownloads = new List<EztvResultTorrentDetails>();

            await Task.Run(async () =>
            {
                lock(availableDownloads);

                var client = HttpClientFactory.CreateClient("EztvShowDownloads");

                TelevisionShow show = DbContext.TelevisionShows.Where(s => s.Id == id).FirstOrDefault();

                if (show != null && show.imdbId != null)
                {
                    using (client)
                    {
                        client.BaseAddress = new Uri(client.BaseAddress.ToString().Replace("IMDBID", show.imdbId));

                        try
                        {
                            using (var clientRequest = await client.GetAsync(client.BaseAddress))
                            {
                                var results = JsonConvert.DeserializeObject<EztvResult>(await clientRequest.Content.ReadAsStringAsync());

                                availableDownloads = results.torrents;
                            }
                        }
                        catch (Exception ex)
                        {
                            //Logger.LogInformation(new EventId(5000, "Error making API Call"), ex, RequestUser.UserName + " threw an exception while searching for: " + showName);
                        }
                    }                  
                }
                                
            });

            return availableDownloads;
        }

        /// <summary>
        /// Return all the Eztv torrent downloads that are available for a given show using TelevisionShows Id
        /// </summary>
        /// <param name="showName">Name of the show</param>
        public async Task<List<EztvResultTorrentDetails>> GetAvailableShowDownloadsByNameAsync(string showName)
        {
            List<EztvResultTorrentDetails> availableDownloads = new List<EztvResultTorrentDetails>();

            await Task.Run(async () =>
            {
                lock (availableDownloads) ;

                var client = HttpClientFactory.CreateClient("EztvShowDownloads");

                TelevisionShow show = DbContext.TelevisionShows.Where(s => s.ShowName == showName).FirstOrDefault();

                if (show != null && show.imdbId != null)
                {
                    using (client)
                    {
                        client.BaseAddress = new Uri(client.BaseAddress.ToString().Replace("IMDBID", show.imdbId));

                        try
                        {
                            using (var clientRequest = await client.GetAsync(client.BaseAddress))
                            {
                                var results = JsonConvert.DeserializeObject<EztvResult>(await clientRequest.Content.ReadAsStringAsync());

                                availableDownloads = results.torrents;
                            }
                        }
                        catch (Exception ex)
                        {
                            //Logger.LogInformation(new EventId(5000, "Error making API Call"), ex, RequestUser.UserName + " threw an exception while searching for: " + showName);
                        }
                    }
                }

            });

            return availableDownloads;
        }
    }
}
