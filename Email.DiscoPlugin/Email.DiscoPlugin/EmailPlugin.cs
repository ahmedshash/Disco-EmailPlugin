using Disco.Data.Repository;
using Disco.Services.Plugins;
using Disco.Services.Tasks;

namespace Email.DiscoPlugin
{
    [Plugin(Id = "EmailPlugin", Name = "Email Plugin", Author = "Ahmed Shash", Url = "https://github.com/ahmedshash/Disco-EmailPlugin", HostVersionMin = "2.0.0918.1700")]
    public class EmailPlugin : Plugin
    {
        public override void Uninstall(DiscoDataContext context, bool uninstallData, ScheduledTaskStatus status)
        {
            if (uninstallData)
                Internal.Helpers.UninstallData(context, this.Manifest, status);
        }
    }
}