using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data.EzTv
{
    public class EztvResult : IApiResult
    {
        public string imdb_id { get; set; }
        public int torrents_count { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public List<EztvResultTorrentDetails> torrents { get; set; }
    }
}
