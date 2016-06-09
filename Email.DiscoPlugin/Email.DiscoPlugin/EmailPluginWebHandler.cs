using Disco.Services.Plugins;
using System;
using System.Web.Mvc;

namespace Email.DiscoPlugin
{
    public class EmailPluginWebHandler : PluginWebHandlerController
    {
        public ActionResult TestEmailConfiguration()
        {
            try
            {
                Internal.Email.SendTestEmail();
                return RedirectToPluginConfiguration();
            }
            catch (Exception message)
            {
                throw new Exception(message.ToString());
            }

        }
    }
}