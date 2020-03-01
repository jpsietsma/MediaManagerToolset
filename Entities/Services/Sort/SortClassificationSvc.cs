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

namespace Entities.Services.Sort
{
    public class SortClassificationSvc : ISortClassificationSvc
    {
        ProgramConfiguration AppSettings;
        DatabaseContext DbContext;

        public Regex TelevisionRegex { get; private set; }
        public Regex MovieRegex { get; private set; }
        public List<string> AllowedFileTypes { get; private set; }

        public SortClassificationSvc(DatabaseContext _dbContext, ProgramConfiguration _programSettings)
        {
            AppSettings = _programSettings;
            DbContext = _dbContext;

            TelevisionRegex = new Regex(@"(?<ShowName>.*)[.][S](?<ShowSeason>\d\d)[E](?<ShowEpisode>\d\d)(?<FileJunk>.*)[.](?<FileExtension>mkv|mp4|avi|mpeg)");
            MovieRegex = new Regex(@"123change456me");
            AllowedFileTypes = new List<string> { "mkv", "mp4", "avi", "mpeg" };
        }

        #region Section: Public Media Classification Service Methods
            /// <summary>
        /// Determine media classification type using full file path
        /// </summary>
        /// <param name="filepath">string file path to be used to determine media classification type</param>
        /// <returns>MediaClassificationTypes classification type</returns>
            public MediaClassificationTypes MediaTypeClassification(string filepath)
            {
                return filepath != null ? DetermineMediaClassificationType(filepath) : MediaClassificationTypes.UNKNOWN;
            }

            /// <summary>
            /// Determine media classification type of any IClassifiableMediaFile type
            /// </summary>
            /// <param name="_mediaFile">IClassifiableMediaFile object to be used to determine media classification type</param>
            /// <returns>MediaClassificationTypes representing classification type</returns>
            public MediaClassificationTypes MediaTypeClassification(IClassifiableMediaFile _mediaFile)
            {
                return _mediaFile.FilePath != null ? DetermineMediaClassificationType(_mediaFile.FilePath) : MediaClassificationTypes.UNKNOWN;
            }

            /// <summary>
            /// Determine media classification type of any TelevisionEpisode type
            /// </summary>
            /// <param name="_episode">TelevisionEpisode object to be used to determine media classification type</param>
            /// <returns>MediaClassificationTypes representing classification type</returns>
            public MediaClassificationTypes MediaTypeClassification(TelevisionEpisode _episode)
            {
                return _episode.ClassificationType;
            }

            /// <summary>
            /// Determine media classification type of any Movie type
            /// </summary>
            /// <param name="_movie">Movie object to be used to determine media classification type</param>
            /// <returns>MediaClassificationTypes representing classification type</returns>
            public MediaClassificationTypes MediaTypeClassification(Data.EF_Core.DatabaseEntities.Movie _movie)
            {
                return _movie.ClassificationType;
            }

            /// <summary>
            /// Determine media classification type of any SortFile type
            /// </summary>
            /// <param name="_sortFile">SortFile object to be used to determine media classification type</param>
            /// <returns>MediaClassificationTypes representing classification type</returns>
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

            /// <summary>
            /// Asynchronous call to determine media classification type using full file path
            /// </summary>
            /// <param name="filepath">string file path to be used to determine media classification type</param>
            /// <returns>Task of type MediaClassificationTypes representing classification type</returns>
            public async Task<MediaClassificationTypes> MediaTypeClassificationAsync(string filepath)
            {
                return await Task.Run(() =>
                {
                    return filepath != null ? DetermineMediaClassificationType(filepath) : MediaClassificationTypes.UNKNOWN;
                });
            }

            /// <summary>
            /// Asynchronous call to determine media classification type of any IClassifiableMediaFile type
            /// </summary>
            /// <param name="_mediaFile">IClassifiableMediaFile object to be used to determine media classification type</param>
            /// <returns>Task of MediaClassificationTypes representing classification type</returns>
            public async Task<MediaClassificationTypes> MediaTypeClassificationAsync(IClassifiableMediaFile _mediaFile)
            {
                return await Task.Run(() =>
                {
                    return _mediaFile.FilePath != null ? DetermineMediaClassificationType(_mediaFile.FilePath) : MediaClassificationTypes.UNKNOWN;
                });
            }

            /// <summary>
            /// Asynchronous call to determine media classification type of any SortFile type
            /// </summary>
            /// <param name="_sortFile">SortFile object to be used to determine media classification type</param>
            /// <returns>Task of MediaClassificationTypes representing classification type</returns>
            public async Task<MediaClassificationTypes> MediaTypeClassificationAsync(SortFile _sortFile)
            {
                return await Task.Run(() =>
                {
                    return _sortFile.FilePath != null ? DetermineMediaClassificationType(_sortFile.FilePath) : MediaClassificationTypes.UNKNOWN;
                });
            }

            /// <summary>
            /// Asynchronous call to determine media classification type of any TelevisionEpisode type
            /// </summary>
            /// <param name="_episode">TelevisionEpisode object to be used to determine media classification type</param>
            /// <returns>Task of MediaClassificationTypes representing classification type</returns>
            public async Task<MediaClassificationTypes> MediaTypeClassificationAsync(TelevisionEpisode _episode)
            {
                return await Task.Run(() =>
                {
                    return _episode.FilePath != null ? DetermineMediaClassificationType(_episode.FilePath) : MediaClassificationTypes.UNKNOWN;
                });
            }

            /// <summary>
            /// Asynchronous call to determine media classification type of any Movie type
            /// </summary>
            /// <param name="_movie">Movie object to be used to determine media classification type</param>
            /// <returns>Task of MediaClassificationTypes representing classification type</returns>
            public async Task<MediaClassificationTypes> MediaTypeClassificationAsync(Data.EF_Core.DatabaseEntities.Movie _movie)
        {
            return await Task.Run(() =>
            {
                return _movie.ClassificationType;
            });
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

            public string SanitizeShowName(string _filePath, MediaClassificationTypes _classification)
            {
                string filePath = _filePath;
                string sanitizedFileName = string.Empty;

                switch (_classification)
                {
                    case MediaClassificationTypes.TELEVISION:
                        {
                            Regex TelevisionRegex = new Regex(@"(?<ShowName>.*)[.][S](?<ShowSeason>\d\d)[E](?<ShowEpisode>\d\d)(?<FileJunk>.*)[.](?<FileExtension>mkv|mp4|avi|mpeg)");
                            Match NameMatch = TelevisionRegex.Match(filePath);

                            return NameMatch.Groups["ShowName"].ToString().Replace('.', ' ');
                        }
                    case MediaClassificationTypes.MOVIE:
                        {
                            //Determine proper movie filename using regex and return that
                            return null;
                        }
                }

                return null;
            }

            public async Task<string> SanitizeFilePathAsync(string _filePath, MediaClassificationTypes _classification)
            {
                return await Task.Run(() =>
                {
                    string filePath = _filePath;
                    string sanitizedFileName = string.Empty;

                    switch (_classification)
                    {
                        case MediaClassificationTypes.TELEVISION:
                            {
                                Regex TelevisionRegex = new Regex(@"(?<ShowName>.*)[.][S](?<ShowSeason>\d\d)[E](?<ShowEpisode>\d\d)(?<FileJunk>.*)[.](?<FileExtension>mkv|mp4|avi|mpeg)");
                                Match NameMatch = TelevisionRegex.Match(filePath);

                                StringBuilder finalSanitized = new StringBuilder()
                                    .Append(NameMatch.Groups["ShowName"].Value + @".")
                                    .Append('S' + NameMatch.Groups["ShowSeason"].Value)
                                    .Append('E' + NameMatch.Groups["ShowEpisode"].Value)
                                    .Append('.' + NameMatch.Groups["FileExtension"].Value);


                                sanitizedFileName = finalSanitized.ToString();
                                break;
                            }
                        case MediaClassificationTypes.MOVIE:
                            {
                                //Determine proper movie filename using regex and return that
                                break;
                            }
                        default:
                            break;
                    }

                    return filePath;
                });
            }

            public async Task<string> SanitizeShowNameAsync(string _filePath, MediaClassificationTypes _classification)
        {
            return await Task.Run(() =>
            {
                string filePath = _filePath;
                string sanitizedFileName = string.Empty;

                switch (_classification)
                {
                    case MediaClassificationTypes.TELEVISION:
                        {
                            Regex TelevisionRegex = new Regex(@"(?<ShowName>.*)[.][S](?<ShowSeason>\d\d)[E](?<ShowEpisode>\d\d)(?<FileJunk>.*)[.](?<FileExtension>mkv|mp4|avi|mpeg)");
                            Match NameMatch = TelevisionRegex.Match(filePath);

                            return NameMatch.Groups["ShowName"].ToString().Replace('.', ' ');
                        }
                    case MediaClassificationTypes.MOVIE:
                        {
                            //Determine proper movie filename using regex and return that
                            return null;
                        }
                }

                return null;
            });
        }
        #endregion

        #region Section: Sort Processing Service Methods
            public async Task<SortFile> ProcessSortFileAsync(SortFile _sortFile)
        {
            return await Task.Run(async () =>
            {
                //Logic for completing all steps of processing
                // 1 - Get Full File Path
                SortFile finalSortFile = _sortFile;

                // 2 - Run Classification to get type, update database
                finalSortFile.ClassificationType = await MediaTypeClassificationAsync(finalSortFile);

                // 3 - Run sanitization to get sanitized name, sanitized file path, update database
                finalSortFile.SanitizedFilePath = await SanitizeFilePathAsync(finalSortFile.FilePath, finalSortFile.ClassificationType);

                //// 4 - Sanitize Show Name for use in searches going forward
                finalSortFile.SanitizedShowName = await SanitizeShowNameAsync(finalSortFile.FileName, finalSortFile.ClassificationType);

                //// 5 - Sanitize File Name
                //finalSortFile.SanitizedFileName = await SanitizeFileNameAsync(finalSortFile.FileName, finalSortFile.ClassificationType);

                //// 6 - Set Priority Level, update database
                //finalSortFile.PriorityLevel = await DeterminePriorityLevel(finalSortFile);

                return finalSortFile;
            });
        }
        #endregion

        #region Section: Private helper methods
            private MediaClassificationTypes DetermineMediaClassificationType(string fileName)
        {
            if (AllowedFileTypes.Contains(fileName.Split('.').Last()))
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
        #endregion
    }
}
