using Entities.Configuration;
using Entities.Data.EF_Core;
using Entities.Services.Sort;
using Microsoft.Extensions.DependencyInjection;
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
                .AddSingleton<ProgramConfiguration, ProgramConfiguration>()
                .BuildServiceProvider();

            //do the classification work here
            var classificationSvc = serviceProvider.GetService<ISortClassificationSvc>();
            var classification = classificationSvc.MediaTypeClassification(@"Alaska.The.Last.Frontier.S09E01.New.Frontiers.New.Threats.720p.WEB.x264-CAFFEiNE[eztv].mkv");
            var classifiedName = classificationSvc.SanitizeFilePath(@"Alaska.The.Last.Frontier.S09E01.New.Frontiers.New.Threats.720p.WEB.x264-CAFFEiNE[eztv].mkv", classification);

            string classify = classification.ToString();

            var x = 123;
        }
    }
}
