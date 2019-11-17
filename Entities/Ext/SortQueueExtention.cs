using Entities.Abstract;
using Entities.Sort;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Entities.Ext
{
    public static class SortQueueExtention
    {
        public static SortQueue RescanAndClassifySortQueue(this SortQueue _queue)
        {
            SortQueue _newQueue = new SortQueue(_queue.SortPath);

            return _newQueue;
        }
        
    }
}
