using Entities.Library;
using System.Collections.Generic;

namespace Entities.Abstract
{
    public interface ILibraryStorageSvc
    {
        List<MediaLibraryStorageDrive> GetStorageInfo();
    }
}