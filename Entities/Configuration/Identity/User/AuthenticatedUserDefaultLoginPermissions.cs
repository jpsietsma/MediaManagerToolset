using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Configuration.Identity.User
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
                Name = "AllowWebLogin",
                HasPermission = true
            });

            //All users can log in by default through the desktop application
            Permissions.Add(new AuthenticatedUserLoginPermission
            {
                Name = "AllowDesktopLogin",
                HasPermission = true
            });

            //All users are denied login by default to the WebAPI interface as this is for developers
            Permissions.Add(new AuthenticatedUserLoginPermission
            {
                Name = "AllowWebApiLogin",
                HasPermission = false
            });
        }
    }
}
