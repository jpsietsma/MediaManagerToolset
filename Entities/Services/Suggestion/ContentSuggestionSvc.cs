using Entities.Configuration.Identity.User;
using Entities.Data.EF_Core;
using Entities.Data.EF_Core.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Services.Suggestion
{
    public class ContentSuggestionSvc
    {
        AuthenticatedUserInfo UserInfo;
        DatabaseContext DbContext;

        public ContentSuggestionSvc(AuthenticatedUserInfo _userInfo, DatabaseContext _dbContext)
        {
            UserInfo = _userInfo;
            DbContext = _dbContext;
        }

        public List<TelevisionShow> SuggestShows()
        {
            List<TelevisionShow> _suggestedShows = new List<TelevisionShow>();




            return _suggestedShows;
        }

    }
}
