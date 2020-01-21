using System;
using MediaToolsetWebCoreMVC.Data;
using MediaToolsetWebCoreMVC.Models.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(MediaToolsetWebCoreMVC.Areas.Identity.IdentityHostingStartup))]
namespace MediaToolsetWebCoreMVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<IdentityDatabaseContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("IdentityDatabaseContextConnection")));

            //    services.AddDefaultIdentity<AuthenticatedUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<IdentityDatabaseContext>();
            //});
        }
    }
}