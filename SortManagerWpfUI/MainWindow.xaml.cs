using Entities.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SortManagerWpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileSystemWatcher remoteWatcher;
        Entities.Sort.SortQueue SortQueue;

        public readonly ProgramConfiguration AppSettings;

        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public MainWindow(IOptions<ProgramConfiguration> settings)
        {            
            InitializeComponent();
            AppSettings = settings.Value;

            ConfigureDI();

            remoteWatcher = new FileSystemWatcher
            {
                Path = AppSettings.SortConfiguration.RemoteSortDownloadDirectory,
                EnableRaisingEvents = true
            };

            //Set our FileSystemWatcher event triggers
            remoteWatcher.Created += RemoteWatcher_RemoteFileSyncReady;
            remoteWatcher.Deleted += RemoteWatcher_RemoteFileSyncDeleted;            

            SortQueue = new Entities.Sort.SortQueue(AppSettings.SortConfiguration.LocalSortDirectory);

            PopulateUI(SortQueue);
        }

        #region Section: Event Handling Methods...            
            private void RemoteWatcher_RemoteFileSyncDeleted(object sender, FileSystemEventArgs e)
            {
                
            }

            /// <summary>
            /// New File has been added to the remote folder - Messagebox to user
            /// </summary>
            private void RemoteWatcher_RemoteFileSyncReady(object sender, FileSystemEventArgs e)
            {
                string existingFilePath = e.FullPath;

                if (existingFilePath.Split(".").Last() == "mkv" || existingFilePath.Split(".").Last() == "avi" || existingFilePath.Split(".").Last() == "mp4")
                {

                    try
                    {
                        File.Copy(existingFilePath, AppSettings.SortConfiguration.RemoteSortDirectory + existingFilePath.Split("\\").Last());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("unable to move remote files to sort directory.", ex);
                    }

                }
            
            }            

        #endregion

        #region Section: File Menu Item Methods...

            /// <summary>
            /// User Clicked on File -> Exit
            /// </summary>
            private void MenuItem_File_Exit(object sender, RoutedEventArgs e)
            {
                Application.Current.Shutdown();
            }

            /// <summary>
            /// User clicked on Sort -> Current Queue
            /// </summary>
            private void MenuItem_Sort_CurrentQueue(object sender, RoutedEventArgs e)
            {
                var newWindow = ServiceProvider.GetRequiredService<SortQueue>();
                    newWindow.Show();                
            }

            private void MenuItem_Admin_UserSettings(object sender, RoutedEventArgs e)
            {

            }

            private void MenuItem_Maintenance_MissingSeasons(object sender, RoutedEventArgs e)
            {

            }

            private void MenuItem_Movies_MovieLibrary(object sender, RoutedEventArgs e)
            {

            }

            private void MenuItem_Television_TvLibrary(object sender, RoutedEventArgs e)
            {

            }

            private void MenuItem_Television_AddShow(object sender, RoutedEventArgs e)
            {

            }

            private void MenuItem_Maintenance_MissingEpisodes(object sender, RoutedEventArgs e)
            {

            }
            
            /// <summary>
            /// User clicked on Admin -> Program Settings
            /// </summary>
            private void MenuItem_Admin_ProgramSettings(object sender, RoutedEventArgs e)
            {
                ProgramSettings _programSettingsWindow = new ProgramSettings(AppSettings);

                _programSettingsWindow.Topmost = true;
                _programSettingsWindow.Activate();
                _programSettingsWindow.Visibility = Visibility.Visible;
            }

            private void MenuItem_Admin_LibrarySettings(object sender, RoutedEventArgs e)
            {
                LibrarySettings _librarySettingsWindow = new LibrarySettings(AppSettings);

                _librarySettingsWindow.Topmost = true;
                _librarySettingsWindow.Activate();
                _librarySettingsWindow.Visibility = Visibility.Visible;
            }

        #endregion

        #region Section: Populate UI
            private void PopulateUI(Entities.Sort.SortQueue _queue)
        {
            SortQueueCurrentCount.Text = Directory.GetFiles(AppSettings.SortConfiguration.LocalSortDirectory).Count().ToString();
            SortQueueDownloadingCount.Text = Directory.GetFiles(AppSettings.SortConfiguration.LocalSortDownloadDirectory).Count().ToString();
            SortQueueRemoteCompletedCount.Text = Directory.GetFiles(AppSettings.SortConfiguration.RemoteSortDirectory).Count().ToString();
            SortQueueRemoteDownloadingCount.Text = Directory.GetFiles(AppSettings.SortConfiguration.RemoteSortDownloadDirectory).Count().ToString();

            SortQueueTotalSpace.Text = _queue.StorageSpaceTotal.ToString();
            SortQueueFreeSpace.Text = _queue.StorageSpaceRemaining.ToString();
        }
        #endregion

        #region Section: Dependency Injection
            private void ConfigureDI()
            {
                var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile(".\\Properties\\AppSettings.json", optional: false, reloadOnChange: true);

                Configuration = builder.Build();

                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                ServiceProvider = serviceCollection.BuildServiceProvider();
            }
            private void ConfigureServices(IServiceCollection services)
            {
                services.Configure<ProgramConfiguration>(Configuration.GetSection(nameof(ProgramConfiguration)));
                services.AddTransient(typeof(MainWindow));
                services.AddTransient(typeof(ProgramSettings));
                services.AddTransient(typeof(SortQueue));
            }
        #endregion
    }
}
