using Disco.Data.Repository;
using Disco.Models.Repository;
using Disco.Services.Users;
using Email.DiscoPlugin.Configuration;
using System.Net;
using System.Net.Mail;

namespace Email.DiscoPlugin.Internal
{
    public static class Email
    {
        public static void SendCollectionEmail(this Job job, string organisationName)
        {
            using (var context = new DiscoDataContext())
            {
                var emailConfig = new ConfigurationStore(context).DeserializeConfiguration();
                var user = UserService.GetUser(job.UserId);

                //Send email if alert is enabled
                if (!emailConfig.DeviceReadyAlert) return;

                using (var email = new MailMessage())
                {
                    if (user.EmailAddress == null) return;
                    using (var smtp = new SmtpClient(emailConfig.SmtpServerAddress))
                    {
                        if (emailConfig.AuthenticationRequried)
                            smtp.Credentials = new NetworkCredential(emailConfig.SmtpUsername, emailConfig.SmtpPassword);
                        //Message
                        email.To.Add(new MailAddress(user.EmailAddress));
                        email.From = new MailAddress($"{emailConfig.SmtpSenderAddress}", $"Disco ICT - {organisationName}");
                        email.Subject = "Your device is ready for collection";
                        email.Body = $"Dear {user.GivenName} <br /><br /> Your device is ready for collection from the I.T Office.";
                        email.IsBodyHtml = true;
                        smtp.Send(email);
                    }
                }
            }
        }

        public static void SendTestEmail()
        {
            using (var context = new DiscoDataContext())
            {
                var emailConfig = new ConfigurationStore(context).DeserializeConfiguration();
                var user = UserService.CurrentUser;

                using (var email = new MailMessage())
                {
                    if (user.EmailAddress == null) return;
                    using (var smtp = new SmtpClient(emailConfig.SmtpServerAddress))
                    {
                        if (emailConfig.AuthenticationRequried)
                            smtp.Credentials = new NetworkCredential(emailConfig.SmtpUsername, emailConfig.SmtpPassword);
                        //Message
                        email.To.Add(new MailAddress(user.EmailAddress));
                        email.From = new MailAddress($"{emailConfig.SmtpSenderAddress}", $"Disco ICT - {context.DiscoConfiguration.OrganisationName}");
                        email.Subject = "Test Email";
                        email.Body = $"This is a test email to ensure that the email plugin has been successfully configured";
                        smtp.Send(email);
                    }
                }
            }
        }
    }
}