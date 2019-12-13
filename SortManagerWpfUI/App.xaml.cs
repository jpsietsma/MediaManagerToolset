using Entities.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SortManagerWpfUI
{    
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
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
            services.Configure<ProgramConfiguration>(Configuration.GetSection(nameof(ProgramConfiguration)));
            services.AddSingleton(typeof(MainWindow));
            services.AddSingleton(typeof(ProgramSettings));
            services.AddSingleton(typeof(LibrarySettings));
            services.AddSingleton(typeof(SortQueue));
            services.AddSingleton(typeof(Entities.Sort.SortQueue));
            services.AddSingleton(typeof(AiringToday));
            services.AddTransient(typeof(TvMazeShowSearch));
            services.AddTransient(typeof(AddNewPriorityShow));

        }

        protected override void OnExit(ExitEventArgs e)
        {

            base.OnExit(e);
        }

        public IServiceProvider GetService()
        {
            return ServiceProvider;
        }
    }
}
