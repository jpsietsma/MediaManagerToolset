using Entities.Abstract;
using Entities.Configuration;
using Entities.Data.EF_Core;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MediaToolsetWebCoreMVC.Services.Sort
{
    public class SortClassificationSvc : ISortClassificationSvc
    {
        DatabaseContext DbContext;
        ProgramConfiguration AppSettings;

        public SortClassificationSvc(DatabaseContext _dbContext, ProgramConfiguration _programSettings)
        {
            DbContext = _dbContext;
            AppSettings = _programSettings;
        }

        public MediaClassificationTypes MediaTypeClassification(string filepath)
        {
            return filepath != null ? DetermineMediaClassificationType(filepath) : MediaClassificationTypes.UNKNOWN;
        }

        public MediaClassificationTypes MediaTypeClassification(string filepath, out string sanitizedPath)
        {
            MediaClassificationTypes _classification = filepath != null ? DetermineMediaClassificationType(filepath) : MediaClassificationTypes.UNKNOWN;
            sanitizedPath = SanitizeFilePath(filepath, _classification);

            return _classification;
        }

        public MediaClassificationTypes MediaTypeClassification(IClassifiableMediaFile _mediaFile)
        {
            return _mediaFile.FilePath != null ? DetermineMediaClassificationType(_mediaFile.FilePath) : MediaClassificationTypes.UNKNOWN;
        }

        public MediaClassificationTypes MediaTypeClassification(IClassifiableMediaFile _mediaFile, out string sanitizedPath)
        {
            MediaClassificationTypes _classification = _mediaFile.FilePath != null ? DetermineMediaClassificationType(_mediaFile.FilePath) : MediaClassificationTypes.UNKNOWN;
            sanitizedPath = SanitizeFilePath(_mediaFile.FilePath, _classification);

            return _classification;
        }

        public MediaClassificationTypes MediaTypeClassification(TelevisionEpisode _episode)
        {
            return _episode.ClassificationType;
        }

        public MediaClassificationTypes MediaTypeClassification(TelevisionEpisode _episode, out string sanitizedPath)
        {
            sanitizedPath = _episode.EpisodePath;
            return _episode.ClassificationType;
        }

        public MediaClassificationTypes MediaTypeClassification(Movie _movie)
        {
            return _movie.ClassificationType;
        }

        public MediaClassificationTypes MediaTypeClassification(Movie _movie, out string sanitizedName)
        {
            sanitizedName = _movie.FilePath;
            return _movie.ClassificationType;
        }

        public MediaClassificationTypes MediaTypeClassification(SortFile _sortFile)
        {
            if (_sortFile.ClassificationType == MediaClassificationTypes.UNDETERMINED)
            {
                return DetermineMediaClassificationType(_sortFile.FilePath);
            }
            else
            {
                return _sortFile.ClassificationType;
            }
        }

        public MediaClassificationTypes MediaTypeClassification(SortFile _sortFile, out string sanitizedPath)
        {
            MediaClassificationTypes _classification = _sortFile.FilePath != null ? DetermineMediaClassificationType(_sortFile.FilePath) : MediaClassificationTypes.UNKNOWN;
            sanitizedPath = SanitizeFilePath(_sortFile.FilePath, _classification);

            return _classification;
        }



        private MediaClassificationTypes DetermineMediaClassificationType(string fileName)
        {
            Regex TelevisionRegex = new Regex("");
            Regex MovieRegex = new Regex("");
            List<string> allowedFiletypes = new List<string> { ".mkv", ".mp4", ".avi", ".mpeg" };

            if (allowedFiletypes.Contains(fileName.Split('.').Last()))
            {
                if (TelevisionRegex.IsMatch(fileName))
                {
                    return MediaClassificationTypes.TELEVISION;
                }
                else if (MovieRegex.IsMatch(fileName))
                {
                    return MediaClassificationTypes.MOVIE;
                }
                else
                {
                    return MediaClassificationTypes.UNKNOWN;
                }
            }
            else
            {
                return MediaClassificationTypes.UNKNOWN;
            }
        }

        private string SanitizeFilePath(string FilePath, MediaClassificationTypes _classification)
        {
            switch (_classification)
            {
                case MediaClassificationTypes.TELEVISION:
                    {
                        //Determine proper television episode filename using regex and return that
                        return FilePath;
                    }
                case MediaClassificationTypes.MOVIE:
                    {
                        //Determine proper movie filename using regex and return that
                        return FilePath;
                    }
                default:
                    throw new Exception("Unable to sanitize files classified as undetermined or unknown");
            }
        }

    }
}
