using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class SqlConfiguration
    {
        public string SqlServer_Name { get; set; }
        public string SqlServer_Instance { get; set; }
        public string SqlServer_Username { get; set; }
        public string SqlServer_Password { get; set; }
    }

    public class SortDrive
    {
        public string SortDriveLetter { get; set; }
        public string LocalSortDirectory { get; set; }
        public string RemoteSortDirectory { get; set; }
    }

    public class MediaLibrarySetting
    {
        public List<string> TelevisionShowDrives { get; set; }
        public List<string> MovieDrives { get; set; }
        public List<string> KidsTelevisionDrives { get; set; }
        public List<SortDrive> SortDrives { get; set; }
    }

    public class ProgramConfiguration
    {
        public List<SqlConfiguration> SqlConfiguration { get; set; }
        public List<MediaLibrarySetting> MediaLibrarySettings { get; set; }
    }

    public class AppearancePreference
    {
        public string UserInterfaceTheme { get; set; }
        public string UserDisplayName { get; set; }
    }

    public class UserPermission
    {
        public string UserPermissions { get; set; }
        public string AllowedActions { get; set; }
    }

    public class UserConfiguration
    {
        public List<AppearancePreference> AppearancePreferences { get; set; }
        public List<UserPermission> UserPermissions { get; set; }
    }

    public class WpfConfigurationSettings
    {
        public List<ProgramConfiguration> ProgramConfiguration { get; set; }
        public List<UserConfiguration> UserConfiguration { get; set; }
    }
}
