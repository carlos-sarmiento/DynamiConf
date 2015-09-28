namespace DynamiConf.Helpers
{
    public class LocationSources
    {
        private readonly InterpreterSources _sources;

        public LocationSources(InterpreterSources sources, IConfigurationInterpreter configurationInterpreter)
        {
            _sources = sources;
            Interpreter = configurationInterpreter;
            Configuration = sources.Configuration;
        }

        public IConfigurationInterpreter Interpreter { get; private set; }

        public void RegisterConfiguration(Configuration configuration)
        {
            Configuration.RegisterConfiguration(configuration);
        }

        public DynamiConfiguration Configuration { get; private set; }
    }
}