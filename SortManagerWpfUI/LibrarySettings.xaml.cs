using Entities.Configuration;
using Entities.Data.TvMaze;
using Entities.Ext;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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
        string _query = "cops";

        public LibrarySettings(IOptions<ProgramConfiguration> _settings)
        {
            InitializeComponent();
            AppSettings = _settings.Value;

            LibraryListBox.ItemsSource = AppSettings.TelevisionLibraryConfiguration.TelevisionLibrary.LibraryFolders;
        }        
    }
}
