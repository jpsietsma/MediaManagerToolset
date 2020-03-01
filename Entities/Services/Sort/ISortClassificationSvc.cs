using Entities.Abstract;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Enums;
using System.Threading.Tasks;

namespace Entities.Services.Sort
{
    public interface ISortClassificationSvc
    {
        MediaClassificationTypes MediaTypeClassification(IClassifiableMediaFile _mediaFile);
        MediaClassificationTypes MediaTypeClassification(Data.EF_Core.DatabaseEntities.Movie _movie);
        MediaClassificationTypes MediaTypeClassification(SortFile _sortFile);
        MediaClassificationTypes MediaTypeClassification(string filepath);
        MediaClassificationTypes MediaTypeClassification(TelevisionEpisode _episode);

        Task<MediaClassificationTypes> MediaTypeClassificationAsync(string filepath);
        Task<MediaClassificationTypes> MediaTypeClassificationAsync(IClassifiableMediaFile _mediaFile);
        Task<MediaClassificationTypes> MediaTypeClassificationAsync(SortFile _sortFile);
        Task<MediaClassificationTypes> MediaTypeClassificationAsync(TelevisionEpisode _episode);
        Task<MediaClassificationTypes> MediaTypeClassificationAsync(Data.EF_Core.DatabaseEntities.Movie _movie);

        string SanitizeFilePath(string _filePath, MediaClassificationTypes _classification);
    }
}