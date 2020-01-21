using Entities.Configuration;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaToolsetWebCoreMVC.Areas.Identity.Services
{
    public class IdentityEmailSender : IEmailSender
    {
        IEmailConfiguration EmailConfigurationSettings;

        public IdentityEmailSender(IEmailConfiguration _emailConfig)
        {
            EmailConfigurationSettings = _emailConfig;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
                message.To.Add(new MailboxAddress(email));
                message.From.Add(new MailboxAddress(EmailConfigurationSettings.SmtpUsername));
                message.Subject = subject;
                message.Body = new TextPart(TextFormat.Html) {
                
                    Text = htmlMessage
                
                };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(EmailConfigurationSettings.SmtpServer, EmailConfigurationSettings.SmptPort, EmailConfigurationSettings.UseSSL);

                smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");

                await smtpClient.AuthenticateAsync(EmailConfigurationSettings.SmtpUsername, EmailConfigurationSettings.SmtpPassword);

                smtpClient.Send(message);

                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}
