using Entities.Abstract;
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
        FileSystemWatcher SortDirectoryWatcher;
        Entities.Sort.SortQueue SortQueue;

        public readonly ProgramConfiguration AppSettings;

        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public MainWindow(IOptions<ProgramConfiguration> settings)
        {            
            InitializeComponent();
            AppSettings = settings.Value;
            ServiceProvider = (App.Current as App).ServiceProvider;

            SortDirectoryWatcher = ConfigureSortWatcher();            

            SortQueue = ServiceProvider.GetRequiredService<Entities.Sort.SortQueue>();

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
                ServiceProvider.GetRequiredService<SortQueue>().Show();               
            }

            private void MenuItem_Admin_UserSettings(object sender, RoutedEventArgs e)
            {
                ServiceProvider.GetRequiredService<UserSettings>().Show();
            }

            private void MenuItem_Maintenance_MissingSeasons(object sender, RoutedEventArgs e)
            {

            }

            private void MenuItem_Movies_MovieLibrary(object sender, RoutedEventArgs e)
            {

            }

            /// <summary>
            /// User clicked on Television -> Tv Library
            /// </summary>            
            private void MenuItem_Television_AiringToday(object sender, RoutedEventArgs e)
            {
                ServiceProvider.GetRequiredService<AiringToday>().Show();
            }

            private void MenuItem_Television_AddShow(object sender, RoutedEventArgs e)
            {

            }

            private void MenuItem_Maintenance_MissingEpisodes(object sender, RoutedEventArgs e)
            {

            }

            private void MenuItem_Maintenance_SortFileDialogTest(object sender, RoutedEventArgs e)
            {
                new SortFileInfoDialog().Show();
            }
            
            /// <summary>
            /// User clicked on Search -> TvMaze Show search
            /// </summary>
            private void MenuItem_Search_TvMaze(object sender, RoutedEventArgs e)
            {
                ServiceProvider.GetRequiredService<TvMazeShowSearch>().Show();
            }
            
            /// <summary>
            /// User clicked on Admin -> Program Settings
            /// </summary>
            private void MenuItem_Admin_ProgramSettings(object sender, RoutedEventArgs e)
            {
                ServiceProvider.GetRequiredService<ProgramSettings>().Show();
            }

            /// <summary>
            /// User clicked on Admin -> Library Settings
            /// </summary>
            private void MenuItem_Admin_LibrarySettings(object sender, RoutedEventArgs e)
            {
                ServiceProvider.GetRequiredService<LibrarySettings>().Show();
            }

        #endregion

        private FileSystemWatcher ConfigureSortWatcher()
        {
            var watcher = new FileSystemWatcher
            {
                Path = AppSettings.SortConfiguration.LocalSortDirectory,
                EnableRaisingEvents = true
            };

            watcher.Created += RemoteWatcher_RemoteFileSyncReady;
            watcher.Deleted += RemoteWatcher_RemoteFileSyncDeleted;

            return watcher;
        }

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
        
    }
}
