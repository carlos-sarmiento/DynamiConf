using DynamiConf.Providers.Helpers;

namespace DynamiConf
{
    public class DynamiConfiguration
    {
        // Esta clase construye un objeto configuration
        public DynamiConfiguration()
        {
            MergeWith = new ConfigurationSources(this);
        }

        public ConfigurationSources MergeWith { get; private set; }

        public Configuration GetConfiguration()
        {
            return MergeWith.FinalConfiguration;
        }
    }

    public class ConfigurationSources
    {
        internal ConfigurationSources(DynamiConfiguration loader)
        {
            DynamiConfiguration = loader;
        }

        public DynamiConfiguration DynamiConfiguration { get; private set; }

        internal Configuration FinalConfiguration { get; set; }

        public void RegisterConfiguration(Configuration configuration)
        {
            FinalConfiguration.UpdateWith(configuration);
        }
    }
}