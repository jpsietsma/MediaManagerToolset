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

namespace SortManagerWpfUI
{
    /// <summary>
    /// Interaction logic for SortQueue.xaml
    /// </summary>
    public partial class SortQueue : Window
    {
        private Entities.Sort.SortQueue _SortQueue { get; set; }

        public SortQueue()
        {
            InitializeComponent();

            QueueSelection_IsFilenameSanitized.IsEnabled = false;
            QueueSelection_isFileClassified.IsEnabled = false;
            QueueSelection_isExistingShow.IsEnabled = false;

            _SortQueue = new Entities.Sort.SortQueue("S:\\~completed_remote\\");

            CompletedListView.ItemsSource = _SortQueue.CompletedDownloads;
            //DownloadListView.ItemsSource = _SortQueue.DownloadingFiles;

            DataContext = new Entities.Sort.SortQueue();
            QueueSelection_Details.Visibility = Visibility.Collapsed;
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
            QueueSelection_FileClassification.Text = _selectedItem.ClassificationType.Name;
            QueueSelection_FileSanitizedName.Text = _selectedItem.SanitizedFileName;

            if (QueueSelection_FileClassification.Text != "SortFile")
            {
                if (QueueSelection_FileClassification.Text == "TelevisionEpisode")
                {
                    QueueSelection_ShowNameDisplay.Text = _selectedItem.ShowName;
                    QueueSelection_isExistingShow.IsChecked = TelevisionLibraryExtensions.DoesShowExist(_selectedItem.ShowName);
                    TelevisionShowNameStackPanel.Visibility = Visibility.Visible;
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

        private void QueueSelection_TrySanitizeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QueueSelection_TryClassifyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QueueSelection_FileInfoBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
