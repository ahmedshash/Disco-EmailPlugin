using Disco.Services.Plugins;
using System.Web.Mvc;

namespace Email.DiscoPlugin
{
    public class EmailPluginWebHandler : PluginWebHandlerController
    {
        public ActionResult TestEmailConfiguration()
        {
            Internal.Email.SendTestEmail();
            return RedirectToPluginConfiguration();
        }
    }
}