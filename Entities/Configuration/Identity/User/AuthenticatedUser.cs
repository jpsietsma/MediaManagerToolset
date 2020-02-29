using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Entities.Configuration.Identity.User
{
    // Add profile data for application users by adding properties to the AuthenticatedUser class
    public class AuthenticatedUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public bool IsAdminApproved { get; set; }
    }
}
