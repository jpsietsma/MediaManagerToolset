using Entities.Data.TvMaze;
using Syncfusion.Windows.Tools.Controls;
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

namespace SortManagerWpfUI.Library
{
    /// <summary>
    /// Interaction logic for ViewShowDetails.xaml
    /// </summary>
    public partial class ViewShowDetails : Window
    {
        public ViewShowDetails()
        {
            InitializeComponent();

            closeButton = new ButtonAdv();
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
