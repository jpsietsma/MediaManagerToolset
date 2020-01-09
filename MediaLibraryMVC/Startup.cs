using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Configuration;
using Entities.Configuration.AutoMapper;
using Entities.Data;
using Entities.Data.EF_Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace MediaLibraryMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.Get<MvcProgramConfiguration>().ProgramConfiguration.DatabaseConfiguration.ConnectionString));
            services.AddAutoMapper(c => c.AddProfile<AutoMapperProfiles>(), typeof(Startup));

            //Configure our httpclient named instances to inject for API 

            // Configures httpClient calls to the local television library
            services.AddHttpClient("SDNTelevisionLibraryQuery", c=> {

                c.BaseAddress = new Uri(@"http://api.sietsmadevelopment.com/TelevisionLibrary/");

            });

            //Configure our httpclient calls to TheMovieDb
            services.AddHttpClient("TheMovieDBShowQuery", c => {

                c.BaseAddress = new Uri(@$"https://api.themoviedb.org/3/search/tv?api_key={ Configuration.Get<MvcProgramConfiguration>().ProgramConfiguration.MediaAPIKeyConfiguration.ApiKeyInfo.Where(p => p.Name == "TheMovieDB").First().ApiToken }&language=en-US&page=1");

            });

            

            ////Get external Ids using TheMovieDb ID for each show
            //using (var client = new WebClient())
            //{
            //    if (result != null)
            //    {
            //        string apiKey = "c0604d69b7df230f03504bdc8475887a";

            //        string RequestUrl = @$"https://api.themoviedb.org/3/tv/{ result.id }/external_ids?api_key={ apiKey }&language=en-US&page=1";
            //        client.BaseAddress = RequestUrl;

            //        client.Headers.Clear();
            //        client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

            //        try
            //        {
            //            //GET Method  
            //            string response = client.DownloadString(RequestUrl);

            //            var x = JsonConvert.DeserializeObject<TheMovieDbExternalIds>(response);
            //            Show.imdbId = x.imdb_id;
            //            Show.theMovieDbId = x.id.ToString();
            //        }
            //        catch (Exception)
            //        {

            //        }

            //    } 

            //}




            //Configure our Request messages to inject for API httpClient calls
            var request = new HttpRequestMessage() { Method = HttpMethod.Get };
            request.Headers.Add("Accept", "application/json");
            services.AddSingleton(request);

            //Add our ProgramConfiguration to the service container for injection
            services.AddSingleton(Configuration.Get<MvcProgramConfiguration>().ProgramConfiguration);

            //Add our DatabaseContext for EF to the service container for injection
            services.AddScoped<DatabaseContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //BEGIN Syncfusion license Registration//
            string syncFusionLicenseKey = @"MTg4OTY4QDMxMzcyZTM0MmUzMFFHU2Q4cWhLb3E0THVwL0RQQVdsK0VieitPbis2OHlyMk4rOXJpNDFPeTQ9;
                                            MTg4OTY5QDMxMzcyZTM0MmUzMGROcCtKRFhQQjQyOEpmY3NxRXE2dTkrSlJZczdveWxtM3hVZzB3MVJueTg9;
                                            MTg4OTcwQDMxMzcyZTM0MmUzMEhsN2M5YW9oOWVwWnkxM3ZsK2NIWnk2MVozR3RLU20vTWw3RGNzOWJoeHM9;
                                            MTg4OTcxQDMxMzcyZTM0MmUzMGlGbHdmZEFOdlNxTnkyT2JWb2p6aEY2d3VJWExmcW8wajBPNTdFcVBhQjA9;
                                            MTg4OTcyQDMxMzcyZTM0MmUzMGc3bTZKbCtiNDlxWnRTTlB5RjdONUpsNmF2S3B5ZUM2QWJsTWVvZllOSmM9;
                                            MTg4OTczQDMxMzcyZTM0MmUzMFVsV3RqclRNaWpkN2h4dUpIREY1N2MxOWh0MkZ6M25ESXcxTVNWUisrUGM9;
                                            MTg4OTc0QDMxMzcyZTM0MmUzMFBaeFBGUUN3Skovd1M5c1AzVXA5R01DK1NNRWI5QzMzUVBBdFk0OXZUZG89;
                                            MTg4OTc1QDMxMzcyZTM0MmUzMGMrU2tTS0hrVFMrS0RFUVNNMytmQlVaYkNGcjB1WE93SnorWHg1M05PejA9;
                                            MTg4OTc2QDMxMzcyZTM0MmUzMGdGY09hemJ3SFRGYnRaN3hFNEFSd0pvUG50Q2o3TXlhQjE2Yjk3Rm5nVUE9;
                                            NT8mJyc2IWhiZH1nfWN9YGpoYmF8YGJ8ampqanNiYmlmamlmanMDHmg5IyA6NicgPjITND4yOj99MDw+;
                                            MTg4OTc3QDMxMzcyZTM0MmUzMFhIN0wvejJBK09BV2EvYjM3NHhDdUJUTTZKaTNObndRSE1uelB5V2NUWVE9";
           
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncFusionLicenseKey);
            //END Syncfusion license Registration//

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
