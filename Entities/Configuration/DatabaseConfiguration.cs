using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Configuration
{
    public class DatabaseConfiguration
    {
        public string ConnectionString { get { return GetConnectionString(); } }
        public string DBServerType { get; set; }
        public string DBServerName { get; set; }
        public string DBServerInstance { get; set; }
        public string DBName { get; set; }
        public bool Trusted_Connection { get; set; }
        public bool Allow_Default_Login { get; set; }
        public List<DefaultLoginCredential> DefaultLoginCredentials { get; set; }

        public DatabaseConfiguration()
        {

        }

        private string GetConnectionString()
        {
            var _connStringBuilder = new StringBuilder("Data Source=")
                .Append(DBServerName)
                .Append(DBServerInstance + @";")
                .Append("Initial Catalog=")
                .Append(DBName + @";");

            if (Trusted_Connection)
            {
                _connStringBuilder.Append("Trusted_Connection=True;");
            }
            else if(!Trusted_Connection && Allow_Default_Login)
            {
                _connStringBuilder.Append("User Id=");
                _connStringBuilder.Append(DefaultLoginCredentials.First().username + ";");

                _connStringBuilder.Append("Password=");
                _connStringBuilder.Append(DefaultLoginCredentials.First().password + ";");                
            }

            _connStringBuilder.Append("MultipleActiveResultSets=true;");

            return _connStringBuilder.ToString();
        }


    }
}
