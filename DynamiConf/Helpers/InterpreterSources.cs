namespace DynamiConf.Helpers
{
    public class InterpreterSources
    {
        internal InterpreterSources(DynamiConfiguration loader)
        {
            DynamiConfiguration = loader;
            FinalConfiguration = new Configuration();
        }

        public DynamiConfiguration DynamiConfiguration { get; private set; }

        internal Configuration FinalConfiguration { get; set; }

        public void RegisterConfiguration(Configuration configuration)
        {
            FinalConfiguration = FinalConfiguration.UpdateWith(configuration);
        }
    }
}