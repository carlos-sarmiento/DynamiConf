using DynamiConf.Helpers;

namespace DynamiConf
{
    public class DynamiConfiguration
    {
        // Esta clase construye un objeto configuration
        public DynamiConfiguration()
        {
            MergeWith = new InterpreterSources(this);
            FinalConfiguration = new Configuration();
        }

        public InterpreterSources MergeWith { get; private set; }

        private Configuration FinalConfiguration { get; set; }

        public dynamic GetConfiguration()
        {
            return FinalConfiguration;
        }

        internal void RegisterConfiguration(Configuration configuration)
        {
            FinalConfiguration = FinalConfiguration.UpdateWith(configuration);
        }
    }
}