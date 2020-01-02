using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Entities.Configuration.Ext
{
    public static class ProgramConfigurationExt
    {
        /// <summary>
        /// Return a list of string representing television show directories within the television show library folders
        /// </summary>
        public static List<string> GetTelevisionLibraryContents(this TelevisionLibraryConfiguration _LibConfig)
        {
            List<string> _finalFiles = new List<string>();

            foreach (string _drive in _LibConfig.TelevisionLibrary.LibraryFolders)
            {
                _finalFiles.AddRange(Directory.GetDirectories(_drive));               
            }

            _finalFiles = _finalFiles.OrderBy(p => p.Substring(0)).ToList();

            return _finalFiles;
        }

        /// <summary>
        /// Return a list of string representing media files in the local sort directory
        /// </summary>
        public static List<string> GetLocalSortDirectoryContents(this SortConfiguration _SortConfig)
        {
            List<string> _finalSortFiles = new List<string>();

            foreach (string _file in Directory.GetFiles(_SortConfig.LocalSortDirectory))            {

                _finalSortFiles.Add(_file);
            }

            return _finalSortFiles;
        }
    }
}
