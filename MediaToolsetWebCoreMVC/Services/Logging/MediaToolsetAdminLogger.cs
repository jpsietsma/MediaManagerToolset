using Entities.Logging;
using MediaToolsetWebCoreMVC.Areas.Identity.Data;
using MediaToolsetWebCoreMVC.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly AuthenticatedUserInfo UserInfo;
        private readonly HttpContext HttpContext;

        public MediaToolsetAdminLogger(IdentityDatabaseContext _databaseContext, AuthenticatedUserInfo _userInfo, IHttpContextAccessor _httpContext)
        {
            DatabaseContext = _databaseContext;
            UserInfo = _userInfo;
            HttpContext = _httpContext.HttpContext;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            //change later on
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return false;
                case LogLevel.Debug:
                    return false;
                case LogLevel.Information:
                    return true;
                case LogLevel.Warning:
                    return true;
                case LogLevel.Error:
                    return true;
                case LogLevel.Critical:
                    return true;
                case LogLevel.None:
                    return false;
                default:
                    return false;
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            AdministratorLog adminLog = new AdministratorLog()
            {
                UserName = UserInfo.UserName,
                MessageDate = DateTime.Now,
                MessageText = exception.Message,
                MessageType = logLevel.ToString(),
                MessagePage = HttpContext.Request.Path
            };

            try
            {
                DatabaseContext.AdministrationMessageLog.Add(adminLog);
                DatabaseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
