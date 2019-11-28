using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Entities.Sort;
using Entities.Ext;
using TelevisionToolset.Ext;
using System.IO;
using Entities.Abstract;
using System.Collections.ObjectModel;

namespace SortManagerWpfUI
{
    /// <summary>
    /// Interaction logic for SortQueue.xaml
    /// </summary>
    public partial class SortQueue : Window
    {
        FileSystemWatcher _sortWatcher;

        private Entities.Sort.SortQueue FileSortQueue { get; set; }
        private ObservableCollection<IMediaFile> _sortFiles;
        string filesAwaitingSync;
        string localFolder = "S:\\~completed_remote\\";

        public ObservableCollection<IMediaFile> SortFiles { get; set; }

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
            QueueSelection_Details.Visibility = Visibility.Collapsed;

            //Assign values to our new observable collection
            FileSortQueue = new Entities.Sort.SortQueue(localFolder);
            SortFiles = FileSortQueue.CompletedDownloads;

            CompletedListView.ItemsSource = FileSortQueue.CompletedDownloads;

            
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

        private void MoveSortToLibraryBtn_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button;
            StackPanel ParentPanel = clicked.Parent as StackPanel;
            string source;
            string finalDestination = (ParentPanel.Children[2] as TextBlock).Text;

            var ParentWindow = ParentPanel.Parent as StackPanel;
            var ParentWindow2 = ParentWindow.Parent as StackPanel;
            Border border = ParentWindow2.Parent as Border;
            var ParentWindow3 = border.Parent as StackPanel;
            var ParentWindow4 = ParentWindow3.Parent as StackPanel;
            Grid grid = ParentWindow4.Parent as Grid;
            ListBox SortQueueList = ParentWindow4.Children[2] as ListBox;

            source = (SortQueueList.SelectedItem as IMediaFile).FilePath;

            SortQueue thisWindow = this;

            MediaFileTransfer _transferWindow = new MediaFileTransfer(source, finalDestination, thisWindow);
            _transferWindow.Activate();
            _transferWindow.Visibility = Visibility.Visible;
            _transferWindow.Topmost = true;

        }

        #region NEED TO WORK ON - LOW PRIORITY
        private void QueueSelection_TrySanitizeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QueueSelection_TryClassifyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QueueSelection_FileInfoBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

    }
}
