using Entities.Abstract;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Data.EF_Core.DatabaseEntities
{
    public class TelevisionEpisode : IClassifiableMediaFile
    {
        public int Id { get; set; }
        public string EpisodeNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string EpisodeName { get; set; }
        public string EpisodePath { get; set; }

        public string ImdbId { get; set; }
        public string TvMazeId { get; set; }
        public string TheMovieDbId { get; set; }
        public int TelevisionSeasonId { get; set; }

        [NotMapped]
        public MediaClassificationTypes ClassificationType { get; set; } = MediaClassificationTypes.TELEVISION;
        [NotMapped]
        public string FilePath { get { return EpisodePath; } set { EpisodePath = value; } }
             
        public TelevisionEpisode()
        {

        }

    }
}
