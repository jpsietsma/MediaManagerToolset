using Entities.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SortManagerWpfUI
{    
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ProgramConfiguration ProgramSettings { get { return LoadProgramSettings(); } }

        public ProgramConfiguration LoadProgramSettings()
        {
            using (StreamReader r = new StreamReader("Properties\\appsettings.json"))
            {
                string json = r.ReadToEnd();
                ProgramConfiguration ProgramSettings = JsonConvert.DeserializeObject<AppLoadResult>(json).ProgramConfiguration;

                return ProgramSettings;
            }
        }
    }
}
