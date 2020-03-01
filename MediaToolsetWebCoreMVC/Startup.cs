using System;
using System.Linq;
using System.Net.Http;
using AutoMapper;
using Entities.Configuration;
using Entities.Configuration.AutoMapper;
using Entities.Configuration.Identity.User;
using Entities.Data.EF_Core;
using Entities.Services.Sort;
using MediaToolsetWebCoreMVC.Areas.Identity.Services;
using MediaToolsetWebCoreMVC.Controllers;
using MediaToolsetWebCoreMVC.Services.LocalLibrary;
using MediaToolsetWebCoreMVC.Services.MetaData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRChat.Hubs;

namespace MediaToolsetWebCoreMVC
{
    public class Startup
    {
        private readonly ProgramConfiguration ProgramConfiguration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ProgramConfiguration = Configuration.Get<MvcProgramConfiguration>().ProgramConfiguration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(
                    ProgramConfiguration.DatabaseConfiguration.ConnectionString));
            services
                .AddIdentity<AuthenticatedUser, IdentityRole>(options => {

                    options.SignIn.RequireConfirmedAccount = true;

                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredLength = 10;
                    options.Password.RequireNonAlphanumeric = false;

                    options.User.RequireUniqueEmail = true;
                })
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddSignInManager<SignInManager<AuthenticatedUser>>()
                .AddUserManager<UserManager<AuthenticatedUser>>()
                .AddDefaultTokenProviders();

            services
                .ConfigureApplicationCookie(options => {

                    //Configure cookie options here such as login and logout urls
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                });

            services.AddTransient<IEmailSender, IdentityEmailSender>();

            #region Section: Media Toolset Configuration...

                services.AddAutoMapper(c => c.AddProfile<AutoMapperProfiles>(), typeof(Startup));

                services.AddScoped<DatabaseContext>();

                services.AddSingleton(ProgramConfiguration);
                services.AddSingleton(Configuration.Get<MvcProgramConfiguration>());
                services.AddScoped<ListboxController>();
                services.AddSignalR();
            
            #endregion

            #region Section: HttpClient Services...

                services.AddHttpClient("SDNTelevisionLibraryQuery", c => {
                    c.BaseAddress = new Uri(@"http://api.sietsmadevelopment.com/TV/TelevisionLibrary");
                });

                services.AddHttpClient("TheMovieDBShowQuery", c => {
                    c.BaseAddress = new Uri(@$"https://api.themoviedb.org/3/search/tv?api_key={ ProgramConfiguration.MediaAPIKeyConfiguration.ApiKeyInfo.Where(p => p.Name == "TheMovieDB").First().ApiToken }&language=en-US&query=ShowQueryName&page=1");
                });

                services.AddHttpClient("TheMovieDBExternalIDQuery", c => {
                    c.BaseAddress = new Uri(@$"https://api.themoviedb.org/3/tv/ShowID/external_ids?api_key={ ProgramConfiguration.MediaAPIKeyConfiguration.ApiKeyInfo.Where(p => p.Name == "TheMovieDB").First().ApiToken }&language=en-US&query=ShowQueryName&page=1");
                });

                services.AddHttpClient("TheMovieDBQueryById", c => {
                    c.BaseAddress = new Uri(@$"https://api.themoviedb.org/3/tv/ShowId?api_key={ ProgramConfiguration.MediaAPIKeyConfiguration.ApiKeyInfo.Where(p => p.Name == "TheMovieDB").First().ApiToken }&language=en-US");
                });

                services.AddHttpClient("TvMazeLibraryScan", c => {
                    c.BaseAddress = new Uri(@"http://api.tvmaze.com/singlesearch/shows?q=FBI&embed[]=episodes&embed[]=seasons");
                });

                var request = new HttpRequestMessage() { Method = HttpMethod.Get };
                request.Headers.Add("Accept", "application/json");

                services.AddSingleton(request);

            #endregion

            #region Secton: Email Configuration
                services.AddSingleton<IEmailConfiguration>(Configuration.Get<MvcProgramConfiguration>().ProgramConfiguration.EmailConfiguration);
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();            
            services.AddScoped<AuthenticatedUserInfo>();
            services.AddScoped<UserController>();
            services.AddScoped<AdministratorController>();

            services.AddScoped<IMetaDataApiSvc, MetaDataApiSvc>();
            services.AddScoped<ILocalLibraryService, LocalLibraryService>();
            services.AddScoped<ISortClassificationSvc, SortClassificationSvc>();
            services.AddCors();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var useSyncWidgets = ProgramConfiguration.MediaAPIKeyConfiguration.ApiKeyInfo.Where(a => a.Name == "SyncFusionWidgets");
            if (useSyncWidgets.Count() > 0)
            {
                //Syncfusion widget license Registration           
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(useSyncWidgets.FirstOrDefault().ApiToken);
            }

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

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chatHub");
            });

        }       

    }
}
