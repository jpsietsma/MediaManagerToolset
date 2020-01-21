using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class DefaultSuperAdminAccount
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserPass { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}
