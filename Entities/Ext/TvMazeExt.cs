using Entities.Data;
using Entities.Data.TvMaze;
using Entities.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Ext
{
    public static class TvMazeExt
    {
        public static TvMazeShowResultViewModel GetViewModel(this TvMazeShowDetails _model)
        {
            return new TvMazeShowResultViewModel
            {
                Id = _model.id,
                Imdb = _model.externals.imdb,
                Thetvdb = _model.externals.thetvdb,
                Name = _model.name,
                NetworkName = _model.network != null ? _model.network.name : "",
                Type = _model.type,
                Runtime = _model.runtime.ToString(),
                Status = _model.status,
                AiringDay = _model.schedule.days.FirstOrDefault(),
                AiringTime = _model.schedule.time.ToString(),
                //IsExistingShow = new TelevisionLibrary().DoesShowExist(_selectedItem.ShowName, out string ShowRootDirectory)
                Summary = _model.summary
            };
        }

    }
}
