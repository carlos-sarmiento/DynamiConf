namespace DynamiConf.Helpers
{
    public interface IConfigurationInterpreter
    {
        Configuration ParseConfiguration(string configuration);
    }
}