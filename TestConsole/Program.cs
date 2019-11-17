using Entities.Ext;
using Entities.Sort;
using Entities.Television;
using System;
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
            
        }
    }
}

