using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using MediaToolsetCoreMVC.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Entities.Configuration;
using System.Net.Http;
using AutoMapper;
using Entities.Configuration.AutoMapper;

namespace MediaToolsetCoreMVC
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services
                .AddIdentity<IdentityUser, IdentityRole>(options => {

                    options.SignIn.RequireConfirmedAccount = true;

                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredLength = 10;
                    options.Password.RequireNonAlphanumeric = false;

                    options.User.RequireUniqueEmail = true;

                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services
                .ConfigureApplicationCookie(options => {

                    //Configure cookie options here such as login and logout urls
                
                });

            #region Section: Media Toolset Configuration...

                services.AddAutoMapper(c => c.AddProfile<AutoMapperProfiles>(), typeof(Startup));

                services.AddScoped<ApplicationDbContext>();

                services.AddSingleton(Configuration.Get<MvcProgramConfiguration>().ProgramConfiguration);
            #endregion

            #region Section: HttpClient Services...

                services.AddHttpClient("SDNTelevisionLibraryQuery", c => {
                    c.BaseAddress = new Uri(@"http://api.sietsmadevelopment.com/TV/TelevisionLibrary");
                });

                services.AddHttpClient("TheMovieDBShowQuery", c => {
                    c.BaseAddress = new Uri(@$"https://api.themoviedb.org/3/search/tv?api_key={ Configuration.Get<MvcProgramConfiguration>().ProgramConfiguration.MediaAPIKeyConfiguration.ApiKeyInfo.Where(p => p.Name == "TheMovieDB").First().ApiToken }&language=en-US&page=1");
                });

                services.AddHttpClient("TvMazeLibraryScan", c => {
                    c.BaseAddress = new Uri(@"http://api.tvmaze.com/singlesearch/shows?q=FBI&embed[]=episodes&embed[]=seasons");
                });

                var request = new HttpRequestMessage() { Method = HttpMethod.Get };
                request.Headers.Add("Accept", "application/json");

                services.AddSingleton(request);
            #endregion

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var useSyncWidgets = Configuration.Get<MvcProgramConfiguration>().ProgramConfiguration.MediaAPIKeyConfiguration.ApiKeyInfo.Where(a => a.Name == "SyncFusionWidgets");
            if (useSyncWidgets.Count() > 0)
            {
                //Syncfusion widget license Registration           
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(useSyncWidgets.FirstOrDefault().ApiToken);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }

}