using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public interface IMediaFile
    {
        string FileName { get; }
        Type FileClassification { get; }
        string FilePath { get; }
        long FileSize { get; }
        string PriorityLevel { get; set; }

    }
}
