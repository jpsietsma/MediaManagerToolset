using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration.Identity.User
{
    public class UserNotification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NotificationType { get; set; }
        public string NotificationText { get; set; }
        public bool NotificationAcknowledged { get; set; }

        public UserNotification()
        {

        }
    }
}
