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
        WebClient client;
        string currentFileName;
        string filesAwaitingSync;

        string localFolder = "S:\\~completed_remote\\";
        string remoteDir = "C:\\Users\\bobswat\\OneDrive\\~downloads";        

        public MainWindow()
        {            
            InitializeComponent();

            PopulateFilesWaitingForSync();

            remoteWatcher = new FileSystemWatcher
            {
                Path = remoteDir,
                EnableRaisingEvents = true
            };

            //Set our FileSystemWatcher event triggers
            remoteWatcher.Created += RemoteWatcher_RemoteFileAwaitngSync;
            remoteWatcher.Deleted += RemoteWatcher_RemoteFileDeletedAfterCompletion;

            //Web client to be used for downloads
            client = new WebClient();
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
                client.DownloadFileCompleted += Client_DownloadFileCompleted;

            //Get the list of sort files currently waiting processing
            Entities.Sort.SortQueue _queue = new Entities.Sort.SortQueue("S:\\");
            
            //Update our UI fields to display the information
            SortQueueCurrentCount.Text = _queue.CompletedDownloads.Count().ToString();
            SortQueueDownloadingCount.Text = _queue.DownloadingFiles.Count().ToString();
            SortQueueTotalSpace.Text = _queue.StorageSpaceTotal.ToString();
            SortQueueFreeSpace.Text = _queue.StorageSpaceRemaining.ToString();
            remoteSyncHyperlinkText.Text = Directory.GetFiles(remoteDir).Count().ToString() + " files ready";


        }

        #region Section: Miscellaneous Methods...
            /// <summary>
            /// Grab the filename of the next file up for sync locally
            /// </summary>
            private void PopulateFilesWaitingForSync()
            {
                filesAwaitingSync = Directory.GetFiles("C:\\Users\\bobswat\\OneDrive\\~downloads").ToList().FirstOrDefault();
            }
        #endregion

        #region Section: Event Handling Methods...

            /// <summary>
            /// File sync was successful and confirmed local - refresh the next file for sync
            /// </summary>
            private void RemoteWatcher_RemoteFileDeletedAfterCompletion(object sender, FileSystemEventArgs e)
            {
                PopulateFilesWaitingForSync();
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

            /// <summary>
            /// File sync successfully completed, make sure local and remote are exact sizes then announce to user.  delete file from remote, then refresh next sync file.
            /// </summary>
            private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
            {
                if (File.Exists(localFolder + filesAwaitingSync.Split('\\').Last()) && new FileInfo(localFolder + filesAwaitingSync.Split('\\').Last()).Length == new FileInfo(filesAwaitingSync).Length)
                {
                    //filesAwaitingSync.Remove(_thisFileInForeach);
                    MessageBox.Show("File  " + currentFileName + " synchronize complete!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);

                    try
                    {
                        File.Delete(filesAwaitingSync);
                        PopulateFilesWaitingForSync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "An error occurred!");
                    }

                }
            }

            /// <summary>
            /// Fires when syncing a file, updates progress bar and displays current working filename
            /// </summary>
            private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    remoteSyncProgress.Minimum = 0;
                    double received = double.Parse(e.BytesReceived.ToString());
                    double total = double.Parse(e.TotalBytesToReceive.ToString());
                    double percentageComplete = (received / total) * 100;
                    syncPercentComplete.Text = $"{ string.Format("{0:0.##}", percentageComplete)}%";
                    remoteSyncProgress.Value = int.Parse(Math.Truncate(percentageComplete).ToString());

                    if (currentSyncFilenameTxt.Text != currentFileName)
                    {
                        currentSyncFilenameTxt.Text = currentFileName;
                    }                

                }));
            }

            /// <summary>
            /// User clicked the Sync Remote button, grab the filename, create a new thread to begin async download, and then start the thread
            /// </summary>
            private void SyncRemote_Click(object sender, RoutedEventArgs e)
            {
                PopulateFilesWaitingForSync();

                SyncProgressPanel.Visibility = Visibility.Visible;

                string _filePath = filesAwaitingSync;

                Thread thread = new Thread(() =>
                {
                    Uri uri = new Uri(_filePath);

                    currentFileName = System.IO.Path.GetFileName(uri.AbsolutePath);

                    client.DownloadFileAsync(uri, localFolder + currentFileName);
                });

                currentSyncFilenameTxt.Text = currentFileName;

                thread.Start();

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

        private void SyncFilesWaiting_Click(object sender, RoutedEventArgs e)
        {
            RemoteSyncFileQueue _remoteQueue = new RemoteSyncFileQueue();

                _remoteQueue.Activate();
                _remoteQueue.Topmost= true;
                _remoteQueue.Visibility = Visibility.Visible;
        }
    }
}
