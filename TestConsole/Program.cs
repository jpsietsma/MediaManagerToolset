using Entities.Configuration;
using Entities.Data.EF_Core;
using Entities.Services.Download;
using Entities.Services.Sort;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ISortClassificationSvc, SortClassificationSvc>()
                .AddSingleton<IDownloadAPISvc, EztvDownloadAPISvc>()
                .AddSingleton<ProgramConfiguration, ProgramConfiguration>()
                .BuildServiceProvider();

            //do the classification work here
            //var classificationSvc = serviceProvider.GetService<ISortClassificationSvc>();
            //var classification = classificationSvc.MediaTypeClassification(@"Alaska.The.Last.Frontier.S09E01.New.Frontiers.New.Threats.720p.WEB.x264-CAFFEiNE[eztv].mkv");
            //var classifiedName = classificationSvc.SanitizeFilePath(@"Alaska.The.Last.Frontier.S09E01.New.Frontiers.New.Threats.720p.WEB.x264-CAFFEiNE[eztv].mkv", classification);

            //string classify = classification.ToString();
            var downloadsvc = serviceProvider.GetService<IDownloadAPISvc>();
            var results = downloadsvc.GetAvailableShowDownloadsAsync("6048596");


            var x = 123;
        }
    }
}
