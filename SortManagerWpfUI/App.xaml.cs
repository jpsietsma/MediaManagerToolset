using Entities.Configuration;
using Entities.Data;
using Entities.Data.EF_Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SortManagerWpfUI.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SortManagerWpfUI
{    
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ProgramConfiguration AppSettings;

        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public App()
        {
            
        }        

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile(".\\Properties\\AppSettings.json", optional: false, reloadOnChange: true);             

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(GetConnectionString()));

            services.AddSingleton(Configuration.Get<ProgramConfiguration>());
            services.AddSingleton(typeof(MainWindow));
            services.AddSingleton(typeof(LibrarySettings));
            services.AddSingleton(typeof(SortQueue));
            services.AddSingleton(typeof(Entities.Sort.SortQueue));
            services.AddTransient(typeof(AiringToday));
            services.AddTransient(typeof(TvMazeShowSearch));
            services.AddTransient(typeof(AddNewPriorityShow));
            services.AddTransient(typeof(DatabaseContext));
            services.AddTransient(typeof(SortFileInfoDialog));
            services.AddTransient(typeof(ViewShowDetails));

        }

        protected override void OnExit(ExitEventArgs e)
        {

            base.OnExit(e);
        }

        /// <summary>
        /// Return connection string from appsettings.json file by building from properties
        /// </summary>
        private string GetConnectionString()
        {
            StringBuilder connStr = new StringBuilder("Server=");
            connStr.Append(Configuration.GetSection(nameof(ProgramConfiguration)).GetSection(nameof(DatabaseConfiguration)).GetValue<string>("DBServerName"));
            connStr.Append(Configuration.GetSection(nameof(ProgramConfiguration)).GetSection(nameof(DatabaseConfiguration)).GetValue<string>("DBServerInstance") + ";");
            connStr.Append("Database=");
            connStr.Append(Configuration.GetSection(nameof(ProgramConfiguration)).GetSection(nameof(DatabaseConfiguration)).GetValue<string>("DBName") + ";");

            bool.TryParse(Configuration.GetSection(nameof(ProgramConfiguration)).GetSection(nameof(DatabaseConfiguration)).GetValue<string>("Trusted_Connection"), out bool isTrustedConnection);

            if (isTrustedConnection)
            {
                connStr.Append("Trusted_Connection=true;");
            }

            return connStr.ToString();
        }
    }
}
