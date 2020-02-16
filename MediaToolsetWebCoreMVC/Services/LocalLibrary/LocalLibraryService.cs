using Entities.Configuration;
using Entities.Data.EF_Core.DatabaseEntities;
using MediaToolsetWebCoreMVC.Areas.Identity.Data;
using MediaToolsetWebCoreMVC.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MediaToolsetWebCoreMVC.Services.LocalLibrary
{
    public class LocalLibraryService : ILocalLibraryService
    {
        private readonly IHttpContextAccessor HttpContext;
        private readonly ProgramConfiguration AppSettings;
        private readonly IdentityDatabaseContext DatabaseContext;
        private readonly AuthenticatedUserInfo RequestUser;
        private readonly IHttpClientFactory HttpClientFactory;
        //private readonly ILogger Logger;

        public LocalLibraryService(IHttpContextAccessor _httpContext, IHttpClientFactory _webclientFactory, ProgramConfiguration _programSettings, IdentityDatabaseContext _dbContext, AuthenticatedUserInfo _userInfo)
        {
            HttpContext = _httpContext;
            AppSettings = _programSettings;
            DatabaseContext = _dbContext;
            RequestUser = _userInfo;
            HttpClientFactory = _webclientFactory;
        }

        public List<TelevisionShow> GetLocalLibrary()
        {
            var shows = DatabaseContext
                .TelevisionShows
                .Include(s => s.TelevisionSeasons)
                .ThenInclude(e => e.TelevisionEpisodes)
                .ToList();

            return shows;
        }

        public List<TelevisionSeason> GetLocalSeasons(int showId)
        {
            var seasons = DatabaseContext.TelevisionSeasons.Where(s => s.TelevisionShowId == showId).ToList();

            return seasons;
        }

        public List<TelevisionEpisode> GetLocalEpisodes(int seasonId)
        {
            var episodes = DatabaseContext.TelevisionEpisodes.Where(e => e.TelevisionSeasonId == seasonId).ToList();

            return episodes;
        }

        public List<TelevisionShow> GetPriorityShows()
        {
            var priorityShows = DatabaseContext.PriorityShows.ToList();
            var Shows = new List<TelevisionShow>();

            foreach (PriorityShow priority in priorityShows)
            {
                Shows.Add(DatabaseContext.TelevisionShows.Where(s => s.Id == priority.Id).FirstOrDefault());
            }

            return Shows;
        }

        public TelevisionShow GetLocalShow(int showId)
        {
            TelevisionShow Show = DatabaseContext.TelevisionShows.Where(s => s.Id == showId).FirstOrDefault();

            return Show;
        }

        public TelevisionShow GetLocalShow(string showName)
        {
            TelevisionShow Show = DatabaseContext.TelevisionShows.Where(s => s.ShowName == showName).FirstOrDefault();

            return Show;
        }
    }
}
