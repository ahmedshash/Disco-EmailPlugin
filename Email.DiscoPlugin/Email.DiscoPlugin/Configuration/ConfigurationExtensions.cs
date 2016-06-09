using Disco.Services.Plugins;
using Email.DiscoPlugin.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Email.DiscoPlugin.Configuration
{
    internal static class ConfigurationExtensions
    {
        public static ConfigurationModel DeserializeConfiguration(this ConfigurationStore configStore)
        {
            var serializer = new JavaScriptSerializer();

            if (configStore.EmailConfiguration == null)
            {
                return new ConfigurationModel
                {
                    SmtpServerAddress = "",
                    SmtpServerPort = null,
                    EnableSsl = false,
                    SmtpSenderAddress = "",
                    AuthenticationRequried = false,
                    SmtpUsername = "",
                    SmtpPassword = "",
                    DeviceReadyAlert = false
                };
            }

            var deserializedConfig = serializer.Deserialize<ConfigurationModel>(configStore.EmailConfiguration);

            return new ConfigurationModel
            {
                SmtpServerAddress = deserializedConfig.SmtpServerAddress,
                SmtpServerPort = deserializedConfig.SmtpServerPort,
                EnableSsl = deserializedConfig.EnableSsl,
                SmtpSenderAddress = deserializedConfig.SmtpSenderAddress,
                AuthenticationRequried = deserializedConfig.AuthenticationRequried,
                SmtpUsername = deserializedConfig.SmtpUsername,
                SmtpPassword = deserializedConfig.SmtpPassword,
                DeviceReadyAlert = deserializedConfig.DeviceReadyAlert
            };
        }

        public static ConfigurationModel ToConfigurationModel(this Controller controller)
        {
            var model = new ConfigurationModel();
            return controller.TryUpdateModel(model) ? model : null;
        }

        public static void UpdateStore(this ConfigurationModel model, ConfigurationStore configStore)
        {
            var serializer = new JavaScriptSerializer();
            var serializedConfig = serializer.Serialize(model);
            configStore.EmailConfiguration = serializedConfig;
        }
    }
}