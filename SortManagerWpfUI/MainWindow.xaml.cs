using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public MainWindow()
        {
            InitializeComponent();

            Entities.Sort.SortQueue _queue = new Entities.Sort.SortQueue("S:\\");
            SortQueueCurrentCount.Text = _queue.CompletedDownloads.Count().ToString();
            SortQueueDownloadingCount.Text = _queue.DownloadingFiles.Count().ToString();
            SortQueueTotalSpace.Text = _queue.StorageSpaceTotal.ToString();
            SortQueueFreeSpace.Text = _queue.StorageSpaceRemaining.ToString();


        }

        private void MenuItem_File_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

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

        private void MenuItem_Admin_ProgramSettings(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Admin_LibrarySettings(object sender, RoutedEventArgs e)
        {

        }
    }
}
