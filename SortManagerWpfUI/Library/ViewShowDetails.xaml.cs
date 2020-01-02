using Entities.Configuration;
using Entities.Data.TvMaze;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SortManagerWpfUI.Library
{
    /// <summary>
    /// Interaction logic for ViewShowDetails.xaml
    /// </summary>
    public partial class ViewShowDetails : Window
    {
        private ProgramConfiguration AppSettings;

        public ViewShowDetails(ProgramConfiguration _settings)
        {
            InitializeComponent();
            AppSettings = _settings;

            closeButton = new ButtonAdv();
            closeButton.Content = "Close Window";
            closeButton.Height = 35;
            closeButton.Width = 150;
            closeButton.Label = "Close";

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
