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
    /// Interaction logic for AddNewPriorityShow.xaml
    /// </summary>
    public partial class AddNewPriorityShow : Window
    {
        ProgramConfiguration AppSettings;

        public AddNewPriorityShow(IOptions<ProgramConfiguration> _settings)
        {
            InitializeComponent();
            AppSettings = _settings.Value;


        }
    }
}
