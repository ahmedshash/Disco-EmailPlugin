using Disco.Data.Repository;
using Disco.Data.Repository.Monitor;
using Disco.Models.Repository;
using Disco.Services.Plugins;
using Disco.Services.Plugins.Features.InteroperabilityProvider;
using Disco.Services.Users;
using Email.DiscoPlugin.Configuration;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace Email.DiscoPlugin.Features
{
    [PluginFeature(Id = "Email", Name = "Email", PrimaryFeature = true)]
    public class EmailFeature : InteroperabilityProviderFeature
    {
        public override void Initialize(DiscoDataContext database)
        {
            RepositoryMonitor.StreamAfterCommit
                .Where(e =>
                    e.EntityType == typeof(Job)
                    && e.ModifiedProperties.Contains("DeviceReadyForReturn"))
                .Subscribe(DeviceReadyForReturn);
        }

        public void DeviceReadyForReturn(RepositoryMonitorEvent e)
        {
            var job = (Job)e.Entity;
            var emailConfig = new ConfigurationStore(e.Database).DeserializeConfiguration();
            var user = UserService.GetUser(job.UserId);

            if (!job.DeviceReadyForReturn.HasValue) return;
            if (!emailConfig.DeviceReadyAlert) return;
            if (user.EmailAddress == null) return;

            //Send Collection Email
            try
            {
                Internal.Email.SendCollectionEmail(job, e.Database.DiscoConfiguration.OrganisationName);
                //Insert note into job log if email has been sent successfully
                e.Database.JobLogs.Add(new JobLog
                {
                    Job = job,
                    Comments = $"# Collection Email Sent.\r\n Collection email sent to {user.DisplayName}",
                    TechUser = e.Database.Users.Find(UserService.CurrentUserId),
                    Timestamp = DateTime.Now
                });
                e.Database.SaveChanges();
            }
            catch (Exception message)
            {
                throw new Exception(message.ToString());
            }
        }
    }
}