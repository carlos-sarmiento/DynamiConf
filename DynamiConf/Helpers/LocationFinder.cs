namespace DynamiConf.Helpers
{
    public class LocationFinder
    {
        public LocationFinder(InterpreterSources sources, IConfigurationInterpreter configurationInterpreter)
        {
            From = new LocationSources(sources, configurationInterpreter);
        }

        public LocationSources From { get; set; }
    }
}