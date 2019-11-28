using Entities.Configuration;
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
        private readonly ProgramConfiguration AppSettings;

        public MainWindow(IOptions<ProgramConfiguration> settings)
        {            
            InitializeComponent();
            AppSettings = settings.Value;

            var y = AppSettings.DatabaseConfiguration.ConnectionString;

            remoteWatcher = new FileSystemWatcher
            {
                Path = AppSettings.SortConfiguration.RemoteSortDownloadDirectory,
                EnableRaisingEvents = true
            };

            //Set our FileSystemWatcher event triggers
            remoteWatcher.Created += RemoteWatcher_RemoteFileAwaitngSync;
            remoteWatcher.Deleted += RemoteWatcher_RemoteFileDeletedAfterCompletion;            

            //Get the list of sort files currently waiting processing
            Entities.Sort.SortQueue _queue = new Entities.Sort.SortQueue("S:\\");

            //Update our UI fields to display the information
            SortQueueCurrentCount.Text = Directory.GetFiles(AppSettings.SortConfiguration.LocalSortDirectory).Count().ToString();
            SortQueueDownloadingCount.Text = Directory.GetFiles(AppSettings.SortConfiguration.LocalSortDownloadDirectory).Count().ToString();
            SortQueueRemoteCompletedCount.Text = Directory.GetFiles(AppSettings.SortConfiguration.RemoteSortDirectory).Count().ToString();
            SortQueueRemoteDownloadingCount.Text = Directory.GetFiles(AppSettings.SortConfiguration.RemoteSortDownloadDirectory).Count().ToString();

            SortQueueTotalSpace.Text = _queue.StorageSpaceTotal.ToString();
            SortQueueFreeSpace.Text = _queue.StorageSpaceRemaining.ToString();           

        }

        #region Section: Event Handling Methods...

            
            private void RemoteWatcher_RemoteFileDeletedAfterCompletion(object sender, FileSystemEventArgs e)
            {
                
            }

            /// <summary>
            /// New File has been added to the remote folder - Messagebox to user
            /// </summary>
            private void RemoteWatcher_RemoteFileAwaitngSync(object sender, FileSystemEventArgs e)
            {
                string newFilePath = e.FullPath;

                if (newFilePath.Split(".").Last() == "mkv" || newFilePath.Split(".").Last() == "avi" || newFilePath.Split(".").Last() == "mp4")
                {

                    MessageBox.Show($"{e.Name} is ready to sync!\nLocated: {newFilePath}");

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
                SortQueue _queueWindow = new SortQueue();

                _queueWindow.Topmost = true;
                _queueWindow.Activate();
                _queueWindow.Visibility = Visibility.Visible;
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
                ProgramSettings _programSettingsWindow = new ProgramSettings();

                _programSettingsWindow.Topmost = true;
                _programSettingsWindow.Activate();
                _programSettingsWindow.Visibility = Visibility.Visible;
            }

            private void MenuItem_Admin_LibrarySettings(object sender, RoutedEventArgs e)
            {

            }

        #endregion
    }
}
