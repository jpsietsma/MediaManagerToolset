using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Configuration.Identity.User
{
    public class AuthenticatedUserLoginPermission
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public bool HasPermission { get; set; }

    }
}
