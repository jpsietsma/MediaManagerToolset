using Entities.Configuration;
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

namespace SortManagerWpfUI
{
    /// <summary>
    /// Interaction logic for LibrarySettings.xaml
    /// </summary>
    public partial class LibrarySettings : Window
    {
        private readonly ProgramConfiguration AppSettings;

        public LibrarySettings(ProgramConfiguration settings)
        {
            InitializeComponent();
            AppSettings = settings;

            List<string> LibraryList = AppSettings.TelevisionLibraryConfiguration.TelevisionLibrary.LibraryFolders;
            LibraryListBox.ItemsSource = LibraryList;
        }
    }
}
