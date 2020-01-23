using MediaToolsetWebCoreMVC.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaToolsetWebCoreMVC.Services.Logging
{
    public class MediaToolsetAdminLogger : ILogger
    {
        private readonly IdentityDatabaseContext DatabaseContext;

        public MediaToolsetAdminLogger(IdentityDatabaseContext _databaseContext)
        {
            DatabaseContext = _databaseContext;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }
    }
}
