using Disco.Services.Plugins;
using Email.DiscoPlugin.Models;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace Email.DiscoPlugin.Configuration
{
    internal static class ConfigurationExtensions
    {
        public static ConfigurationModel DeserializeConfiguration(this ConfigurationStore configStore)
        {
            if (configStore.EmailConfiguration == null)
            {
                return new ConfigurationModel()
                {
                    AuthenticationRequried = false,
                    DeviceReadyAlert = false,
                    SmtpServerAddress = "",
                    SmtpPassword = "",
                    SmtpSenderAddress = "",
                    SmtpUsername = ""
                };
            }
            var deserializedConfig = JsonConvert.DeserializeObject<ConfigurationModel>(configStore.EmailConfiguration);
            return new ConfigurationModel()
            {
                SmtpServerAddress = deserializedConfig.SmtpServerAddress,
                AuthenticationRequried = deserializedConfig.AuthenticationRequried,
                DeviceReadyAlert = deserializedConfig.DeviceReadyAlert,
                SmtpPassword = deserializedConfig.SmtpPassword,
                SmtpUsername = deserializedConfig.SmtpUsername,
                SmtpSenderAddress = deserializedConfig.SmtpSenderAddress
            };
        }

        public static ConfigurationModel ToConfigurationModel(this Controller controller)
        {
            var model = new ConfigurationModel();
            return controller.TryUpdateModel(model) ? model : null;
        }

        public static void UpdateStore(this ConfigurationModel model, ConfigurationStore configStore)
        {
            var serializedConfig = JsonConvert.SerializeObject(model);
            configStore.EmailConfiguration = serializedConfig;
        }
    }
}