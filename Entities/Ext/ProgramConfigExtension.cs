using Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Ext
{
    public static class ProgramConfigExtension
    {
        /// <summary>
        /// Get all folders where television shows are stored.
        /// </summary>
        /// <returns>List of full folder paths</returns>
        public static List<string> GetTelevisionLibraries(this ProgramConfiguration _config)
        {
            return _config.TelevisionLibraryConfiguration.TelevisionLibrary.LibraryFolders;
        }

        /// <summary>
        /// Get all folders where movies are stored.
        /// </summary>
        /// <returns>List of full folder paths</returns>
        public static List<string> GetMovieLibraries(this ProgramConfiguration _config)
        {
            return _config.MovieLibraryConfiguration.MovieLibrary.LibraryFolders;
        }

        /// <summary>
        /// Get the path to the local sort folder.
        /// </summary>
        /// <returns>string full path to folder.</returns>
        public static string GetSortLocal(this ProgramConfiguration _config)
        {
            return _config.SortConfiguration.LocalSortDirectory;
        }

        /// <summary>
        /// Get the path to the local sort downloads in progress folder.
        /// </summary>
        /// <returns>string full path to the folder</returns>
        public static string GetSortDownloadLocal(this ProgramConfiguration _config)
        {
            return _config.SortConfiguration.LocalSortDownloadDirectory;
        }

        /// <summary>
        /// Get the path to the remote sort folder.
        /// </summary>
        /// <returns>string of full path to the remote sort sync folder.</returns>
        public static string GetSortRemote(this ProgramConfiguration _config)
        {
            return _config.SortConfiguration.RemoteSortDirectory;
        }

        /// <summary>
        /// Get the path to the remote sort download folder.
        /// </summary>
        /// <returns>string of full path to the remote sort sync download in progress folder.</returns>
        public static string GetSortDownloadRemote(this ProgramConfiguration _config)
        {
            return _config.SortConfiguration.RemoteSortDownloadDirectory;
        }
    }
}
