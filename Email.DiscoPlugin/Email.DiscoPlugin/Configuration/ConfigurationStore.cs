using Disco.Data.Configuration;
using Disco.Data.Repository;

namespace Email.DiscoPlugin.Configuration
{
    public class ConfigurationStore : ConfigurationBase
    {
        public ConfigurationStore(DiscoDataContext context) : base(context) { }

        public override string Scope => "Plugin_Email";

        public string EmailConfiguration
        {
            get { return Get<string>(null); }
            set { Set(value); }
        }
    }
}