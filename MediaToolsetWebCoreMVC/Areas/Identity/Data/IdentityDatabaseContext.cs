using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Logging;
using MediaToolsetWebCoreMVC.Areas.Identity.Data;
using MediaToolsetWebCoreMVC.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediaToolsetWebCoreMVC.Data
{
    public class IdentityDatabaseContext : IdentityDbContext<AuthenticatedUser>
    {
        public virtual DbSet<TelevisionEpisode> TelevisionEpisodes { get; set; }
        public virtual DbSet<TelevisionSeason> TelevisionSeasons { get; set; }
        public virtual DbSet<TelevisionShow> TelevisionShows { get; set; }
        public virtual DbSet<SortFile> SortFiles { get; set; }
        public virtual DbSet<AuthenticatedUserLoginPermission> AspNetUserLoginPermissions { get; set; }            
        public virtual DbSet<TelevisionShowAiringSchedule> TelevisionShowAiringSchedules { get; set; }
        public virtual DbSet<AdministratorLog> AdministrationMessageLog { get; set; }

        public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
            : base(options)
        {
        }

        public List<TelevisionShow> SearchShows(string showName = null)
        {
            var showNameParameter = string.IsNullOrEmpty(showName) ? new ObjectParameter("ShowName", showName) : new ObjectParameter("ShowName", typeof(string));

            var shows = this.TelevisionShows.FromSqlRaw("[dbo].[SearchShowsByName] {0}", showName).ToList();

            return shows;
        }

        public List<AuthenticatedUserLoginPermission> GetLoginPermissions(string userId)
        {
            return AspNetUserLoginPermissions.Where(u => u.UserId == userId).ToList();            
        }

        public void AddMissingEpisodeRecord(int _ShowId, int _SeasonId, int _EpisodeId, string _EpisodeFilePath = null, string _ImdbId = null, string _TvMazeId = null, string _TheMovieDbId = null)
        {
            var showIdParam = new SqlParameter("@ShowId", _ShowId);
            var SeasonIdParam = new SqlParameter("@SeasonId", _SeasonId);
            var EpisodeIdParam = new SqlParameter("@EpisodeId", _EpisodeId);
            var FilePathParam = string.IsNullOrEmpty(_EpisodeFilePath) ? new SqlParameter("@EpisodeFilePath", default(string)) : new SqlParameter("@EpisodeFilePath", _EpisodeFilePath);
            var ImdbIdParam = string.IsNullOrEmpty(_ImdbId) ? new SqlParameter("@ImdbId", default(string)) : new SqlParameter("@ImdbId", _ImdbId);
            var TvMazeIdParam = string.IsNullOrEmpty(_TvMazeId) ? new SqlParameter("@TvMazeId", default(string)) : new SqlParameter("@TvMazeId", _TvMazeId);
            var TheMovieDbIdParam = string.IsNullOrEmpty(_TheMovieDbId) ? new SqlParameter("@TheMovieDbId", default(string)) : new SqlParameter("@TheMovieDbId", _TheMovieDbId);

            TelevisionShows.FromSqlRaw("[dbo].[AddMissingEpisode] @ShowId, @SeasonId, @EpisodeId, @EpisodeFilePath, @ImdbId, @TvMazeId, @TheMovieDbId", showIdParam, SeasonIdParam, EpisodeIdParam, FilePathParam, ImdbIdParam, TvMazeIdParam, TheMovieDbIdParam);
            SaveChanges();
        }

        public void AddPriorityShow(int TelevisionShowId, int PriorityLevel = 3)
        {
            var TelevisionShowIdParam = TelevisionShowId == default ? new SqlParameter("@TelevisionShowId", default(int)) : new SqlParameter("@TelevisionShowId", TelevisionShowId);
            var PriorityLevelIdParam = PriorityLevel == default ? new SqlParameter("@PriorityLevelId", default(int)) : new SqlParameter("@PriorityLevelId", PriorityLevel);

            TelevisionShows.FromSqlRaw("[dbo].[AddPriorityShow] @TelevisionShowId, @PriorityLevelId", TelevisionShowIdParam, PriorityLevelIdParam);
            SaveChanges();
        }

        public void AddPriorityLevel(string PriorityLevelName, int PriorityLevelCode = 3)
        {
            var PriorityLevelNameParam = string.IsNullOrEmpty(PriorityLevelName) ? new SqlParameter("@PriorityLevelName", default(int)) : new SqlParameter("@TelevisionShowId", PriorityLevelName);
            var PriorityLevelCodeParam = PriorityLevelCode == default ? new SqlParameter("@PriorityLevelCode", default(int)) : new SqlParameter("@PriorityLevelCode", PriorityLevelCode);

            TelevisionShows.FromSqlRaw("[dbo].[AddPriorityLevel] @PriorityLevelName, @PriorityLevelCode", PriorityLevelNameParam, PriorityLevelCodeParam);
            SaveChanges();
        }

        public List<AdministratorLog> GetAdministrationLogs()
        {
            var x = Set<AdministratorLog>().FromSqlRaw("dbo.GetAdminLogs").ToList();

            return x;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //model building

            base.OnModelCreating(builder);
        }
    }
}
