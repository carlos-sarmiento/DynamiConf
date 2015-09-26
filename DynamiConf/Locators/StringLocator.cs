using DynamiConf.Helpers;

namespace DynamiConf.Locators
{
    public static class StringLocator
    {
        public static DynamiConfiguration String(this LocationSources sources, string str)
        {
            var conf = sources.Interpreter.ParseConfiguration(str);
            sources.RegisterConfiguration(conf);

            return sources.Configuration;
        }
    }
}