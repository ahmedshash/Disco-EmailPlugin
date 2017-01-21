using Disco.Data.Repository;
using Disco.Services.Plugins;
using Disco.Services.Users;
using System.Web.Mvc;

namespace Email.DiscoPlugin.Configuration
{
    public class ConfigurationHandler : PluginConfigurationHandler
    {
        public override PluginConfigurationHandlerGetResponse Get(DiscoDataContext database, Controller controller)
        {
            var store = new ConfigurationStore(database);
            var model = store.DeserializeConfiguration();

            return Response<Views.Configuration>(model);
        }

        public override bool Post(DiscoDataContext database, FormCollection form, Controller controller)
        {
            var store = new ConfigurationStore(database);
            var model = controller.ToConfigurationModel();

            if (model == null) return false;
            model.UpdateStore(store);
            Internal.Email.SendEmailMessage(UserService.CurrentUser);
            return true;
        }
    }
}