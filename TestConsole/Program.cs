using Entities.Configuration;
using Entities.Ext;
using Entities.Library;
using Entities.Sort;
using Entities.Television;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SortQueue _queue = new SortQueue("S:\\");
                _queue.RescanAndClassifySortQueue();

            dynamic _file = _queue.CompletedDownloads.First() as dynamic;
            //_file.SanitizeTelevisionEpisode(out string NewName, out string ShowName, out string SeasonNumber, out string EpisodeNumber);

            //FileSystemWatcher onedriveWatcher = new FileSystemWatcher(@"");

            //MediaLibrary Library = new MediaLibrary() { LibraryRootPath = "E:\\" };
            

            //Console.WriteLine(Library.GetFreeDriveSpace());
            //Console.WriteLine(Library.GetTotalDriveSpace());

            //var config = LoadProgramSettings();

        }

        public static WpfConfigurationSettings LoadProgramSettings()
        {
            using (StreamReader r = new StreamReader("ProgramSettings.json"))
            {
                string json = r.ReadToEnd();
                WpfConfigurationSettings ProgramSettings = JsonConvert.DeserializeObject<WpfConfigurationSettings>(json);

                return ProgramSettings;
            }
        }

    }
}

