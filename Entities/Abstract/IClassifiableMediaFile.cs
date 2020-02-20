using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public interface IClassifiableMediaFile
    {
        MediaClassificationTypes ClassificationType { get; set; }
        public string FilePath { get; set; }
    }
}
