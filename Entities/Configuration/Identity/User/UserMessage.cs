using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration.Identity.User
{
    public class UserMessage
    {
        public int Id { get; set; }
        public int UserRecipientId { get; set; }
        public int UserSenderId { get; set; }
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }

        public UserMessage()
        {

        }
    }
}
