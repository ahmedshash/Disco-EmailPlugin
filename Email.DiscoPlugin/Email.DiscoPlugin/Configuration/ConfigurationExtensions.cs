using Disco.Services.Plugins;
using Email.DiscoPlugin.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace Email.DiscoPlugin.Configuration
{
    internal static class ConfigurationExtensions
    {
        public static ConfigurationModel DeserializeConfiguration(this ConfigurationStore configStore)
        {
            var deserializedConfig = new JavaScriptSerializer().Deserialize<ConfigurationModel>(configStore.EmailConfiguration);

            return new ConfigurationModel
            {
                CurrentVersion = deserializedConfig.CurrentVersion,
                SmtpServerAddress = deserializedConfig.SmtpServerAddress,
                SmtpServerPort = deserializedConfig.SmtpServerPort,
                EnableSsl = deserializedConfig.EnableSsl,
                SmtpSenderAddress = deserializedConfig.SmtpSenderAddress,
                AuthenticationRequried = deserializedConfig.AuthenticationRequried,
                SmtpUsername = deserializedConfig.SmtpUsername,
                SmtpPassword = deserializedConfig.SmtpPassword,
                MessageConfig = deserializedConfig.MessageConfig
            };
        }

        public static ConfigurationModel ToConfigurationModel(this Controller controller)
        {
            var model = new ConfigurationModel();
            return controller.TryUpdateModel(model) ? model : null;
        }

        public static void UpdateStore(this ConfigurationModel model, ConfigurationStore configStore)
        {
            configStore.EmailConfiguration = new JavaScriptSerializer().Serialize(model);
        }

        public static List<MessageConfig> CreateDefaultMessages()
        {
            return new List<MessageConfig>
            {
                new MessageConfig
                {
                    EmailBody = "",
                    EmailSubject = "",
                    EmailAlertEnabled = false,
                    EmailMessageType = MessageType.DeviceReadyForCollection
                },
                new MessageConfig
                {
                    EmailBody = "This is a test email to ensure that the email plugin has been successfully configured",
                    EmailSubject = "Test Email",
                    EmailAlertEnabled = true,
                    EmailMessageType = MessageType.PluginTestEmail
                }
            };
        }
    }
}