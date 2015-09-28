namespace DynamiConf.Helpers
{
    public class InterpreterSources
    {
        internal InterpreterSources(DynamiConfiguration loader)
        {
            Configuration = loader;
        }

        public DynamiConfiguration Configuration { get; private set; }

        public void RegisterConfiguration(Configuration configuration)
        {
            Configuration.RegisterConfiguration(configuration);
        }
    }
}