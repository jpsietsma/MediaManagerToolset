using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Television
{
    public class TelevisionLibraryScannedDirectory
    {
        private string _data;

        public string Data { get { return _data; } private set { _data = value; UpdateShowDetails(value); } }               

        public string ShowName { get; private set; }
        public string ShowPath { get; private set; }

        public TelevisionLibraryScannedDirectory(string _data)
        {
            Data = _data;
        }

        private void UpdateShowDetails(string value)
        {
            if (value.Contains(@"\TV Shows\"))
            {
                string showPath = value;

                ShowName = showPath.Split(@"\").Last();
                ShowPath = showPath;
            }
        }
    }
}
