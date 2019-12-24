using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class ApiKeyInfo
    {
        public string Name { get; set; }
        public string Base_URL { get; set; }
        public string Image_Base_Url { get; set; }
        public object Authentication_URL { get; set; }
        public string ApiToken { get; set; }
        public object auth_username { get; set; }
        public object auth_password { get; set; }
    }
}
