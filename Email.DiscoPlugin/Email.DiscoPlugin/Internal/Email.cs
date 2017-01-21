using Disco.Data.Repository;
using Disco.Models.Repository;
using Email.DiscoPlugin.Configuration;
using Email.DiscoPlugin.Models;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Email.DiscoPlugin.Internal
{
    public static class Email
    {
        public static void SendEmailMessage(User user, MessageConfig messageConfig = null, Job job = null)
        {
            using (var context = new DiscoDataContext())
            {
                var emailConfig = new ConfigurationStore(context).DeserializeConfiguration();
                if (messageConfig == null)
                {
                    // No message config 
                    messageConfig = emailConfig.MessageConfig.First(z => z.EmailMessageType == MessageType.PluginTestEmail);
                }
                using (var email = new MailMessage())
                {
                    using (var smtp = new SmtpClient(emailConfig.SmtpServerAddress))
                    {
                        if (emailConfig.SmtpServerPort != null) smtp.Port = emailConfig.SmtpServerPort.Value;

                        smtp.EnableSsl = emailConfig.EnableSsl;

                        if (emailConfig.AuthenticationRequried) smtp.Credentials = new NetworkCredential(emailConfig.SmtpUsername, emailConfig.SmtpPassword);

                        //Message
                        email.To.Add(new MailAddress(user.EmailAddress));
                        email.From = new MailAddress($"{emailConfig.SmtpSenderAddress}", $"Disco ICT - {context.DiscoConfiguration.OrganisationName}");
                        email.Subject = messageConfig.EmailSubject;
                        email.Body = messageConfig.EmailBody.Replace("\r\n", "<br />");
                        email.IsBodyHtml = true;
                        smtp.Send(email);
                    }
                }
            }
        }
    }
}