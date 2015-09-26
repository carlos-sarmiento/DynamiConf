namespace DynamiConf.Helpers
{
    public class LocationSources
    {
        private readonly InterpreterSources _sources;

        public LocationSources(InterpreterSources sources, IConfigurationInterpreter configurationInterpreter)
        {
            _sources = sources;
            Interpreter = configurationInterpreter;
            Configuration = sources.DynamiConfiguration;
        }

        public IConfigurationInterpreter Interpreter { get; private set; }

        public void RegisterConfiguration(Configuration configuration)
        {
            _sources.FinalConfiguration = _sources.FinalConfiguration.UpdateWith(configuration);
        }

        public DynamiConfiguration Configuration { get; private set; }
    }
}