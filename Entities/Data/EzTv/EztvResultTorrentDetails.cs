using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.EzTv
{
    public class EztvResultTorrentDetails
    {
        public int id { get; set; }
        public string hash { get; set; }
        public string filename { get; set; }
        public string episode_url { get; set; }
        public string torrent_url { get; set; }
        public string magnet_url { get; set; }
        public string title { get; set; }
        public string imdb_id { get; set; }
        public string season { get; set; }
        public string episode { get; set; }
        public string small_screenshot { get; set; }
        public string large_screenshot { get; set; }
        public int seeds { get; set; }
        public int peers { get; set; }
        public int date_released_unix { get; set; }
        public string size_bytes { get; set; }
    }
}
