using Entities.Configuration;
using Microsoft.Extensions.Options;
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
    /// Interaction logic for ProgramSettings.xaml
    /// </summary>
    public partial class ProgramSettings : Window
    {
        private readonly ProgramConfiguration AppSettings;

        public ProgramSettings(ProgramConfiguration settings)
        {
            InitializeComponent();

            AppSettings = settings;

            PopulateUI();

        }

        private void PopulateUI()
        {
            config_SQLConnString.Text = AppSettings.DatabaseConfiguration.ConnectionString;
            config_SQLServer.Text = AppSettings.DatabaseConfiguration.DBServerName;
            config_SQLInstance.Text = AppSettings.DatabaseConfiguration.DBServerInstance;
            config_SQLDBName.Text = AppSettings.DatabaseConfiguration.DBName;
        }
    }
}
