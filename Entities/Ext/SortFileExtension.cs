﻿using Entities.Sort;
using Entities.Television;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Entities.Ext
{
    public static class SortFileExtension
    {
        public static void PopulateFileInfo(this SortFile _sortFile)
        {
            if (!string.IsNullOrEmpty(_sortFile.FilePath))
            {
                var info = new FileInfo(_sortFile.FilePath);

                SortFileInfo _SortInfo = new SortFileInfo
                {
                    CreationTime = info.CreationTime,
                    Directory = info.Directory,
                    DirectoryName = info.DirectoryName,
                    Extension = info.Extension,
                    FileAttributes = info.Attributes,
                    FullName = info.FullName,
                    Name = info.Name,
                    LastWriteTime = info.LastWriteTime,
                    Length = info.Length

                };

                _sortFile.SortFileInfo = _SortInfo;
            }
        }

        public static bool SanitizeTelevisionEpisode(this SortFile _sortFile, out string SanitizedTitle, out string DisplayShowName, out string SeasonNumber, out string EpisodeNumber)
        {
            if (_sortFile != null)
            {
                string unsanitizedTitle = _sortFile.FileName;
                string unsanitizedShowName = string.Empty;

                string TelevisionShowPattern = @"(?<ShowName>.+)[.|\s]+[Ss](?<ShowSeasonNumber>\d?\d)[Ee]?(?<ShowEpisodeNumber>\d?\d)[.|\s]+.*(?<ShowFileExtension>mkv|avi|mp4)";

                var RegResults = new Regex(TelevisionShowPattern).Match(unsanitizedTitle);

                unsanitizedShowName = RegResults.Groups["ShowName"].Value;


                if (RegResults.Groups.Count == 5)
                {
                    StringBuilder _finalTitle = new StringBuilder();
                    _finalTitle.Append(RegResults.Groups["ShowName"]);
                    _finalTitle.Append(".S");
                    _finalTitle.Append(RegResults.Groups["ShowSeasonNumber"]);
                    _finalTitle.Append("E");
                    _finalTitle.Append(RegResults.Groups["ShowEpisodeNumber"]);
                    _finalTitle.Append("." + RegResults.Groups["ShowFileExtension"]);

                    SanitizedTitle = _finalTitle.ToString();
                    DisplayShowName = RegResults.Groups["ShowName"].Value.Replace(".", " ");

                    //If season is only one digit in length, assume we need to prepend a 0
                    if (RegResults.Groups["ShowSeasonNumber"].Value.Length == 1)
                    {
                        SeasonNumber = "0" + RegResults.Groups["ShowSeasonNumber"].Value;
                    }
                    else
                    {
                        SeasonNumber = RegResults.Groups["ShowSeasonNumber"].Value;
                    }

                    //Same with episode number, if length == 1 then prepend 0
                    if (RegResults.Groups["ShowEpisodeNumber"].Value.Length == 1)
                    {
                        EpisodeNumber = ")" + RegResults.Groups["ShowEpisodeNumber"].Value;
                    }
                    else
                    {
                        EpisodeNumber = RegResults.Groups["ShowEpisodeNumber"].Value;
                    }                    

                    _sortFile.SanitizedFileName = SanitizedTitle;
                    _sortFile.SanitizedFilePath = _sortFile.FilePath.Replace(_sortFile.FileName, SanitizedTitle);
                    _sortFile.PriorityLevel = "1";

                    return true;
                }
                else
                {
                    SanitizedTitle = null;
                    DisplayShowName = null;
                    SeasonNumber = null;
                    EpisodeNumber = null;

                    return false;
                    //throw new Exception("Filename does not match Television Episode format.");
                }
            }
            else
            {
                SanitizedTitle = null;
                DisplayShowName = null;
                SeasonNumber = null;
                EpisodeNumber = null;
                return false;
            }
            
        }

        public static bool IsTelevisionFileNameSanitized(this SortFile _sortFile)
        {
            string TelevisionShowPattern = @"(?<ShowName>.+)[.][Ss](?<ShowSeasonNumber>\d\d)[Ee](?<ShowEpisodeNumber>\d\d)(?<ShowFileExtension>.mkv|.avi|.mp4)";

            return Regex.IsMatch(_sortFile.FileName, TelevisionShowPattern);
        }

        public static bool IsTelevisionFileNameSanitized(string _fileName)
        {
            string TelevisionShowPattern = @"(?<ShowName>.+)[.][Ss](?<ShowSeasonNumber>\d\d)[Ee](?<ShowEpisodeNumber>\d\d)(?<ShowFileExtension>.mkv|.avi|.mp4)";

            return Regex.IsMatch(_fileName, TelevisionShowPattern);
        }

        public static TelevisionEpisode GetEpisode(this SortFile _sortFile)
        {
            if (_sortFile != null)
            {
                string patternTV = @"(?<ShowName>.+)[.|\s]+[Ss](?<ShowSeasonNumber>\d?\d)[Ee]?(?<ShowEpisodeNumber>\d?\d)[.|\s]+.*(?<ShowFileExtension>mkv|avi|mp4)";
                Regex _reg = new Regex(patternTV);

                var match = Regex.Match(_sortFile.FileName, patternTV);

                TelevisionEpisode _episode = new TelevisionEpisode
                {
                    ShowName = match.Groups[1].Value.Replace(".", " "),
                    SeasonNumber = match.Groups[2].Value,
                    EpisodeNumber = match.Groups[3].Value,
                    FileName = _sortFile.FileName,
                    FilePath = _sortFile.FilePath,
                    FileSize = _sortFile.FileSize,
                    PriorityLevel = _sortFile.PriorityLevel

                };

                return _episode;
            }
            else
            {
                return null;
            }
        }
            

    }

    public static class FormattingExtensions
    {
        public static void FormatShowTitleString(this string _showTitle, out string SanitizedShowName)
        {
            string _finalShowTitle = string.Empty;
            string[] _titleWords = _showTitle.Replace(".", " ").Split(" ");

            foreach (string word in _titleWords)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    _finalShowTitle += word.Replace(word.ToCharArray()[0].ToString(), word.ToCharArray()[0].ToString().ToUpper()) + " ";
                }                
            }

            _finalShowTitle = _finalShowTitle.Trim();

            SanitizedShowName = _finalShowTitle;

        }

    }
}