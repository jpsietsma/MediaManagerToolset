using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace SortManagerWpfUI
{
    /// <summary>
    /// Interaction logic for MediaFileTransfer.xaml
    /// </summary>
    public partial class MediaFileTransfer : Window
    {
        WebClient client;

        public string Source { get; set; }
        public string Destination { get; set; }
        public SortQueue CallingWindow { get; }


        public MediaFileTransfer(string source, string destination, SortQueue _callingWindow, bool needTransferConfirmation = true)
        {
            InitializeComponent();

            client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            Source = source;
            Destination = destination;
            CallingWindow = _callingWindow;

            SourceFile.Text = Source;
            DestinationFile.Text = Destination;

            if (!needTransferConfirmation)
            {                
                Thread thread = new Thread(() =>
                {
                    client = new WebClient();

                    Uri uri = new Uri(Source);

                    Source = Path.GetFileName(uri.AbsolutePath).ToString();

                    client.DownloadFileAsync(uri, Destination);
                });

                thread.Start();
            }
            else
            {
                BeginTransferManuallyBtn.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// File sync successfully completed, make sure local and remote are exact sizes then announce to user and delete file from local.
        /// </summary>
        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {           
            this.Dispatcher.Invoke(() =>
            {
                try
                {
                    File.Delete(SourceFile.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to delete source file!");
                }

                SuccessfulTransferManuallyBtn.IsEnabled = true;
            });
            
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

                BeginTransferManuallyBtn.Visibility = Visibility.Collapsed;
                SuccessfulTransferManuallyBtn.Visibility = Visibility.Visible;
            }));
        }
               
        /// <summary>
        /// User clicked the Sync Remote button, grab the filename, create a new thread to begin async download, and then start the thread
        /// </summary>
        private void BeginTransferManuallyBtn_Click(object sender, RoutedEventArgs e)
        {
            BeginTransferManuallyBtn.IsEnabled = false;

            if (!BeginTransferManuallyBtn.IsVisible)
            {
                BeginTransferManuallyBtn.Visibility = Visibility.Visible;                
            }

            Thread thread = new Thread(() =>
            {
                Uri uri = new Uri(Source);

                Source = Path.GetFileName(uri.AbsolutePath);

                client.DownloadFileAsync(uri, Destination);
            });

            thread.Start();           
        }

        private void SuccessfulTransferButton_Click(object sender, RoutedEventArgs e)
        {
            //Close the calling sort window
            //CallingWindow.Close();

            this.Dispatcher.Invoke(() =>
            {
                var lb = CallingWindow.FindName("CompletedListView") as ListBox;
                    lb.Items.Refresh();
            });

            //Open a new sort window with refreshed content.  Can be fixed when we figure out how to update ListBox UI contents on the fly with this implementation.
            //SortQueue _newWindow = new SortQueue();
            //_newWindow.Activate();
            //_newWindow.Visibility = Visibility.Visible;
            //_newWindow.Topmost = true;

            //Close this file transfer window, as we have successfully completed the transfer.
            this.Close();
        }
    }
}
