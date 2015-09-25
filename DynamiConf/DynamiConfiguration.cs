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

        public dynamic GetConfiguration()
        {
            return MergeWith.FinalConfiguration;
        }
    }
}