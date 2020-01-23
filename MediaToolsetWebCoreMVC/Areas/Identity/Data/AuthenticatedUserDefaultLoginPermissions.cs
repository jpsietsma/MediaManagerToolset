using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaToolsetWebCoreMVC.Areas.Identity.Data
{
    public class AuthenticatedUserDefaultLoginPermissions
    {
        public List<AuthenticatedUserLoginPermission> Permissions { get; private set; }

        public AuthenticatedUserDefaultLoginPermissions()
        {
            Permissions = new List<AuthenticatedUserLoginPermission>();

            //All users can log in by default through the web interface
            Permissions.Add(new AuthenticatedUserLoginPermission
            {
                Id = 0,
                Name = "AllowWebLogin",
                HasPermission = true
            });

            //All users can log in by default through the desktop application
            Permissions.Add(new AuthenticatedUserLoginPermission
            {
                Id = 1,
                Name = "AllowDesktopLogin",
                HasPermission = true
            });

            //All users are denied login by default to the WebAPI interface as this is for developers
            Permissions.Add(new AuthenticatedUserLoginPermission
            {
                Id = 2,
                Name = "AllowWebApiLogin",
                HasPermission = false
            });
        }
    }
}
