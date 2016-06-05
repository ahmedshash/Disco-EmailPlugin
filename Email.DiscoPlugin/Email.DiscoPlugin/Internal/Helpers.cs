using Disco.Data.Repository;
using Disco.Services.Plugins;
using Disco.Services.Tasks;
using Email.DiscoPlugin.Configuration;

namespace Email.DiscoPlugin.Internal
{
    public class Helpers
    {
        public static void UninstallData(DiscoDataContext database, PluginManifest manifest, ScheduledTaskStatus status)
        {
            status.UpdateStatus("Removing Plugin Configuration");
            var config = new ConfigurationStore(database);
            config.EmailConfiguration = null;
            database.SaveChanges();
        }
    }
}