using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Television
{
    public class TelevisionSeason
    {
        public int Id { get; set; }
        public string ShowName { get; set; }
        public string SeasonName { get; set; }
        public string SeasonNumber { get; set; }

        public string DirectoryPath { get; set; }
        public List<TelevisionEpisode> Episodes { get; set; }

        public TelevisionSeason(string _path)
        {
            Episodes = new List<TelevisionEpisode>();

            DirectoryPath = _path;
        }
    }
}
