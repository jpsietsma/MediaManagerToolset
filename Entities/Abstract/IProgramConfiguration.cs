using Entities.Configuration;

namespace Entities.Abstract
{
    public interface IProgramConfiguration
    {
        DatabaseConfiguration DatabaseConfiguration { get; set; }
        MediaAPIKeyConfiguration MediaAPIKeyConfiguration { get; set; }
        MovieLibraryConfiguration MovieLibraryConfiguration { get; set; }
        SortConfiguration SortConfiguration { get; set; }
        TelevisionLibraryConfiguration TelevisionLibraryConfiguration { get; set; }
    }
}