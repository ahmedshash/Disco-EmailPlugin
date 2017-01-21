using Disco.Data.Repository;
using Disco.Services.Plugins;
using Disco.Services.Tasks;
using Email.DiscoPlugin.Configuration;
using Email.DiscoPlugin.Models;
using System.Web.Script.Serialization;

namespace Email.DiscoPlugin
{
    [Plugin(Id = "EmailPlugin", Name = "Emails", Author = "Ahmed Shash", Url = "https://github.com/ahmedshash/Disco-EmailPlugin", HostVersionMin = "2.0.0918.1700")]
    public class EmailPlugin : Plugin
    {
        private const int CurrentVersion = 1;

        public override void Install(DiscoDataContext database, ScheduledTaskStatus status)
        {
            var configStore = new ConfigurationStore(database);
            if (configStore.EmailConfiguration == null)
            {
                var defaultConfig = new ConfigurationModel
                {
                    CurrentVersion = 1,
                    SmtpServerAddress = "",
                    SmtpServerPort = null,
                    EnableSsl = false,
                    SmtpSenderAddress = "",
                    AuthenticationRequried = false,
                    SmtpUsername = "",
                    SmtpPassword = "",
                    MessageConfig = ConfigurationExtensions.CreateDefaultMessages()
                };

                configStore.EmailConfiguration = new JavaScriptSerializer().Serialize(defaultConfig);
                database.SaveChanges();
                status.SetFinishedMessage("Installed initial configuration");
            }
        }

        public override void Uninstall(DiscoDataContext context, bool uninstallData, ScheduledTaskStatus status)
        {
            if (uninstallData)
            {
                Internal.Helpers.UninstallData(context, Manifest, status);
            }
        }

        public override void AfterUpdate(DiscoDataContext database, PluginManifest previousManifest)
        {
            var configStore = new ConfigurationStore(database);
            var currentConfig = configStore.DeserializeConfiguration();

            while (currentConfig.CurrentVersion < CurrentVersion)
            {
                switch (currentConfig.CurrentVersion)
                {
                }
            }
        }
    }
}