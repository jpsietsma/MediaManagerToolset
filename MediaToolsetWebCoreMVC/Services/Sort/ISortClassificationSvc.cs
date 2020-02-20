using Entities.Abstract;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Enums;

namespace MediaToolsetWebCoreMVC.Services.Sort
{
    public interface ISortClassificationSvc
    {
        MediaClassificationTypes MediaTypeClassification(IClassifiableMediaFile _mediaFile);
        MediaClassificationTypes MediaTypeClassification(IClassifiableMediaFile _mediaFile, out string sanitizedPath);
        MediaClassificationTypes MediaTypeClassification(Movie _movie);
        MediaClassificationTypes MediaTypeClassification(Movie _movie, out string sanitizedName);
        MediaClassificationTypes MediaTypeClassification(SortFile _sortFile);
        MediaClassificationTypes MediaTypeClassification(SortFile _sortFile, out string sanitizedPath);
        MediaClassificationTypes MediaTypeClassification(string filepath);
        MediaClassificationTypes MediaTypeClassification(string filepath, out string sanitizedPath);
        MediaClassificationTypes MediaTypeClassification(TelevisionEpisode _episode);
        MediaClassificationTypes MediaTypeClassification(TelevisionEpisode _episode, out string sanitizedPath);
    }
}