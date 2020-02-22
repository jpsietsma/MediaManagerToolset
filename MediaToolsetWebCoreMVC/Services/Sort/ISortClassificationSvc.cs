using Entities.Abstract;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Enums;

namespace MediaToolsetWebCoreMVC.Services.Sort
{
    public interface ISortClassificationSvc
    {
        MediaClassificationTypes MediaTypeClassification(IClassifiableMediaFile _mediaFile);
        MediaClassificationTypes MediaTypeClassification(Movie _movie);
        MediaClassificationTypes MediaTypeClassification(SortFile _sortFile);
        MediaClassificationTypes MediaTypeClassification(string filepath);
        MediaClassificationTypes MediaTypeClassification(TelevisionEpisode _episode);

        string SanitizeFilePath(string _filePath, MediaClassificationTypes _classification);
    }
}