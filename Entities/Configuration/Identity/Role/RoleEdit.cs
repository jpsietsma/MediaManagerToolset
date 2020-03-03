﻿using Entities.Configuration.Identity.User;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Entities.Configuration.Identity.Role
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AuthenticatedUser> Members { get; set; }
        public IEnumerable<AuthenticatedUser> NonMembers { get; set; }
    }
}