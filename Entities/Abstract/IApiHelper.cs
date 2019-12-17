using Entities.Abstract;
using Entities.Data.EF_Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public interface IApiHelper
    {
        DatabaseContext DatabaseContext { get; }
        string Response { get; }
        string RequestUrl { get; set; }

        dynamic MakeAPICall(string ImdbQueryId = "6048596", string TheMovieDbQueryId = "44", string language = "en-US");
    }
}
