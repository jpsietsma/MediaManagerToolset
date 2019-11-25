using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using Entities.Sort;
using Entities.Ext;
using TelevisionToolset.Ext;
using System.IO;
using System.Net;
using System.Threading;
using System.ComponentModel;
using Entities.Abstract;
using System.Collections.ObjectModel;

namespace SortManagerWpfUI
{
    /// <summary>
    /// Interaction logic for SortQueue.xaml
    /// </summary>
    public partial class SortQueue : Window
    {
        ObservableCollection<IMediaFile> SortFiles = new ObservableCollection<IMediaFile>();
        FileSystemWatcher _sortWatcher;

        private Entities.Sort.SortQueue FileSortQueue { get; set; }

        WebClient client;
        string currentFileName;
        string filesAwaitingSync;
        string localFolder = "S:\\~completed_remote\\";
        string remoteDir = "C:\\Users\\bobswat\\OneDrive\\~downloads";

        public SortQueue()
        {
            InitializeComponent();

            _sortWatcher = new FileSystemWatcher(localFolder);
            _sortWatcher.Created += _sortWatcher_AddedOrRemoved;
            _sortWatcher.Deleted += _sortWatcher_AddedOrRemoved;
            _sortWatcher.EnableRaisingEvents = true;

            QueueSelection_IsFilenameSanitized.IsEnabled = false;
            QueueSelection_isFileClassified.IsEnabled = false;
            QueueSelection_isExistingShow.IsEnabled = false;

            //Assign values to our new observable collection
            FileSortQueue = new Entities.Sort.SortQueue(localFolder);
            SortFiles = FileSortQueue.CompletedDownloads;

            CompletedListView.ItemsSource = FileSortQueue.CompletedDownloads;
            //DownloadListView.ItemsSource = _SortQueue.DownloadingFiles;
            remoteSyncHyperlinkText.Text = Directory.GetFiles(remoteDir).Count().ToString() + " files ready";

            //DataContext = FileSortQueue;
            QueueSelection_Details.Visibility = Visibility.Collapsed;

            //Web client to be used for downloads
            client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
        }

        /// <summary>
        /// When file watcher detects file changes to the sort folder, refresh the sort queue
        /// </summary>
        private void _sortWatcher_AddedOrRemoved(object sender, FileSystemEventArgs e)
        {
            //MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to refresh the queue?", "Sort Queue files changed!", MessageBoxButton.YesNo);

            //if (messageBoxResult == MessageBoxResult.Yes)
            //{
            //    Dispatcher.BeginInvoke(
            //    new ThreadStart(() => {
            //        SortQueue newQueue = new SortQueue();

            //        newQueue.Activate();
            //        newQueue.Visibility = Visibility.Visible;
            //        newQueue.Topmost = true;

            //        this.Close();
            //    }));
            //}            
        }      

        /// <summary>
        /// Grab the filename of the next file up for sync locally
        /// </summary>
        private void PopulateFilesWaitingForSync()
        {
            filesAwaitingSync = Directory.GetFiles("C:\\Users\\bobswat\\OneDrive\\~downloads").ToList().FirstOrDefault();
        }

        private void QueueSelection_Changed(object sender, SelectionChangedEventArgs e)
        {
            QueueSelection_TrySanitizeBtn.Visibility = Visibility.Collapsed;
            QueueSelection_TrySanitizeBtn.Visibility = Visibility.Collapsed;

            ListBox _lb = CompletedListView;

            dynamic _selectedItem = _lb.SelectedValue as dynamic;                

            QueueSelection_Filename.Text = _selectedItem.FileName;
            QueueSelection_Filesize.Text = (_selectedItem.FileSize / 1024) / 1024 + " MB";                               
            QueueSelection_Details.Visibility = Visibility.Visible;
            QueueSelection_FileClassification.Text = _selectedItem.FileClassification.Name;
            QueueSelection_FileSanitizedName.Text = _selectedItem.SanitizedFileName;

            if (QueueSelection_FileClassification.Text != "SortFile")
            {
                if (QueueSelection_FileClassification.Text == "TelevisionEpisode")
                {
                    QueueSelection_ShowNameDisplay.Text = _selectedItem.ShowName;

                    TelevisionShowNameStackPanel.Visibility = Visibility.Visible;
                    MoveFinalDestinationTxt.Text = "Show does not exist!";

                    if (TelevisionLibraryExtensions.DoesShowExist(_selectedItem.ShowName, out string ShowRootDirectory))
                    {
                        QueueSelection_isExistingShow.IsChecked = true;
                        IsClassifiedCbxPanel.Visibility = Visibility.Collapsed;
                        IsSanitizedCbxPanel.Visibility = Visibility.Collapsed;

                        FormattingExtensions.GetLibraryHomePath(_selectedItem.FileName, ShowRootDirectory, out string FinalMovePath);

                        if (_selectedItem.SeasonNumber.StartsWith('0'))
                        {
                            _selectedItem.SeasonNumber = _selectedItem.SeasonNumber[1].ToString();
                        }

                        MoveFinalDestinationTxt.Text = FinalMovePath + @"\Season " +  _selectedItem.SeasonNumber + @"\" + _selectedItem.SanitizedFileName;
                        ClassifiedMovePanel.Visibility = Visibility.Visible;
                    }                    
                }

                QueueSelection_isFileClassified.IsChecked = true;
            }
            else
            {
                QueueSelection_isFileClassified.IsChecked = false;
                QueueSelection_TrySanitizeBtn.Visibility = Visibility.Visible;
            }

            //If the filename passes Regex sanitization
            if (_selectedItem.IsFileNameSanitized())
            {
                //Mark checkbox as checked
                QueueSelection_IsFilenameSanitized.IsChecked = true;
            }
            else
            {
                //If filename fails Regex sanitization then uncheck 'IsSanitized'
                QueueSelection_IsFilenameSanitized.IsChecked = false;

                //Show the sanitize Button
                QueueSelection_TrySanitizeBtn.Visibility = Visibility.Visible;

                //Show the classify button
                QueueSelection_TryClassifyBtn.Visibility = Visibility.Collapsed;

                //but disable until filename is sanitized manually.
                if (QueueSelection_IsFilenameSanitized.IsChecked == false)
                {
                    QueueSelection_TryClassifyBtn.IsEnabled = false;
                }

            }

            QueueSelection_Filepriority.Text = _selectedItem.PriorityLevel;
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
                    remoteSyncHyperlinkText.Text = FileSortQueue.DownloadingFiles.Count().ToString() + " files ready";
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

        private void SyncFilesWaiting_Click(object sender, RoutedEventArgs e)
        {
            RemoteSyncFileQueue _remoteQueue = new RemoteSyncFileQueue();

            _remoteQueue.Activate();
            _remoteQueue.Topmost = true;
            _remoteQueue.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// User clicked the Sync Remote button, grab the filename, create a new thread to begin async download, and then start the thread
        /// </summary>
        private void SyncRemote_Click(object sender, RoutedEventArgs e)
        {
            PopulateFilesWaitingForSync();

            if (!string.IsNullOrEmpty(filesAwaitingSync))
            {
                currentSyncFilenameTxt.Text = currentFileName;

                SyncProgressPanel.Visibility = Visibility.Visible;

                string _filePath = filesAwaitingSync;

                Thread thread = new Thread(() =>
                {
                    Uri uri = new Uri(_filePath);

                    currentFileName = System.IO.Path.GetFileName(uri.AbsolutePath);

                    client.DownloadFileAsync(uri, localFolder + currentFileName);
                });

                thread.Start();
            }
            else
            {
                MessageBox.Show("There are no remote files waiting for Synchronization.");
            }

        }

        private void QueueSelection_TrySanitizeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QueueSelection_TryClassifyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QueueSelection_FileInfoBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QueueSelection_TryMoveToLibrary(object sender, RoutedEventArgs e)
        {
            try
            {
                Dispatcher.BeginInvoke(
                new ThreadStart(() => {
                    SortQueue newQueue = new SortQueue();

                    newQueue.Activate();
                    newQueue.Visibility = Visibility.Visible;
                    newQueue.Topmost = true;

                    this.Close();
                }));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MoveSortToLibraryBtn_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button;
            StackPanel ParentPanel = clicked.Parent as StackPanel;
            string finalDestination = (ParentPanel.Children[2] as TextBlock).Text;

            var ParentWindow = ParentPanel.Parent as StackPanel;
            var ParentWindow2 = ParentWindow.Parent as StackPanel;
            Border border = ParentWindow2.Parent as Border;
            var ParentWindow3 = border.Parent as StackPanel;
            var ParentWindow4 = ParentWindow3.Parent as StackPanel;
            Grid grid = ParentWindow4.Parent as Grid;
            ListBox SortQueueList = ParentWindow4.Children[2] as ListBox;


            MessageBox Confirmation = new MessageBox()

            //var y = e.Source
        }
    }
}
