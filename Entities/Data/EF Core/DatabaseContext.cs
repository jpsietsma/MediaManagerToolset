using Entities.Data.EF_Core.DatabaseEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Entities.Data.EF_Core
{
    public class DatabaseContext : DbContext
    {
        //DbSet property declarations
        public virtual DbSet<TelevisionShow> Courses { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public virtual ObjectResult<List<TelevisionShow>> SearchShows(string showName = null)
        {
            var showNameParameter = string.IsNullOrEmpty(showName) ?
                new ObjectParameter("ShowName", showName) :
                new ObjectParameter("ShowName", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<List<TelevisionShow>>("SearchShowsByName", showNameParameter);
        }
    }
}
