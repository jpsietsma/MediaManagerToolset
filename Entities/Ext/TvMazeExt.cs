using Entities.Data.TvMaze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Ext
{
    public static class TvMazeExt
    {
        public static TvMazeShowResultViewModel GetViewModel(this TvMazeShowDetails _model)
        {
            return new TvMazeShowResultViewModel
            {
                Name = _model.name,
                NetworkName = _model.network.name,
                Type = _model.type,
                Runtime = _model.runtime.ToString(),
                Status = _model.status,
                AiringDay = _model.schedule.days.First(),
                //Summary = _model.summary
            };
        }
    }
}
