using Entities.Data.EF_Core.DatabaseEntities;
using System.Collections.Generic;

namespace MediaToolsetWebCoreMVC.Services.LocalLibrary
{
    public interface ILocalLibraryService
    {
        List<TelevisionEpisode> GetLocalEpisodes(int seasonId);
        List<TelevisionShow> GetLocalLibrary();
        List<TelevisionSeason> GetLocalSeasons(int showId);
        List<TelevisionShow> GetPriorityShows();
        TelevisionShow GetLocalShow(int showId);
        TelevisionShow GetLocalShow(string showName);
    }
}