using Entities.Abstract;
using Entities.Configuration;
using Entities.Data.EF_Core;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MediaToolsetWebCoreMVC.Services.Sort
{
    public class SortClassificationSvc : ISortClassificationSvc
    {
        ProgramConfiguration AppSettings;

        public SortClassificationSvc(ProgramConfiguration _programSettings)
        {
            AppSettings = _programSettings;
        }

        #region Section: Media Classification Service Methods
            public MediaClassificationTypes MediaTypeClassification(string filepath)
            {
                return filepath != null ? DetermineMediaClassificationType(filepath) : MediaClassificationTypes.UNKNOWN;
            }

            public MediaClassificationTypes MediaTypeClassification(IClassifiableMediaFile _mediaFile)
            {
                return _mediaFile.FilePath != null ? DetermineMediaClassificationType(_mediaFile.FilePath) : MediaClassificationTypes.UNKNOWN;
            }

            public MediaClassificationTypes MediaTypeClassification(TelevisionEpisode _episode)
            {
                return _episode.ClassificationType;
            }

            public MediaClassificationTypes MediaTypeClassification(Movie _movie)
            {
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
        #endregion

        #region Section: Media Sanitization Service Methods
                public string SanitizeFilePath(string _filePath, MediaClassificationTypes _classification)
                {                    
                    string sanitizedFileName = _filePath;

                    switch (_classification)
                    {
                        case MediaClassificationTypes.TELEVISION:
                        {
                            Regex TelevisionRegex = new Regex(@"(?<ShowName>.*)[.][S](?<ShowSeason>\d\d)[E](?<ShowEpisode>\d\d)(?<FileJunk>.*)[.](?<FileExtension>mkv|mp4|avi|mpeg)");
                            Match NameMatch = TelevisionRegex.Match(sanitizedFileName);
                        
                            StringBuilder finalSanitized = new StringBuilder()
                                .Append(NameMatch.Groups["ShowName"].Value + @".")
                                .Append('S' + NameMatch.Groups["ShowSeason"].Value)
                                .Append('E' + NameMatch.Groups["ShowEpisode"].Value)
                                .Append('.' + NameMatch.Groups["FileExtension"].Value);
                                

                            return finalSanitized.ToString();
                        }
                        case MediaClassificationTypes.MOVIE:
                        {
                            //Determine proper movie filename using regex and return that
                            return sanitizedFileName;
                        }
                        default:
                            return sanitizedFileName;
                    }
                }
            #endregion

            private MediaClassificationTypes DetermineMediaClassificationType(string fileName)
            {
                Regex TelevisionRegex = new Regex(@"(?<ShowName>.*)[.][S](?<ShowSeason>\d\d)[E](?<ShowEpisode>\d\d)(?<FileJunk>.*)[.](?<FileExtension>mkv|mp4|avi|mpeg)");
                Regex MovieRegex = new Regex(@"123change456me");
                List<string> allowedFiletypes = new List<string> { "mkv", "mp4", "avi", "mpeg" };

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

    }
}
