using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Configuration;
using Entities.Configuration.AutoMapper;
using Entities.Data.EF_Core;
using Entities.Sort;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MediaToolsetAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.Get<MvcProgramConfiguration>().ProgramConfiguration.DatabaseConfiguration.ConnectionString));
            services.AddScoped<DatabaseContext>();
            services.AddAutoMapper(c => c.AddProfile<AutoMapperProfiles>(), typeof(Startup));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                    });
            });

            //Add ProgramSettings from appsettings.json to the service container for injection
            services.AddSingleton(Configuration.Get<MvcProgramConfiguration>().ProgramConfiguration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}



//match to exactly one show with embedded external info 
//used for library scans
//
//
//
//http://api.tvmaze.com/singlesearch/shows?q=girls&embed=episodes
//
//
//
//