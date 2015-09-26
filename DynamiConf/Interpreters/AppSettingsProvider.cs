using System;
using System.Configuration;
using System.Linq;
using DynamiConf.Helpers;

namespace DynamiConf.Interpreters
{
    public static class AppSettingsProvider
    {
        public static DynamiConfiguration AppSettings(this InterpreterSources provider, string prefix = "")
        {
            var conf = new Configuration();

            foreach (var key in ConfigurationManager.AppSettings.AllKeys.Where(key => key.StartsWith(prefix)))
            {
                conf[key] = ConfigurationManager.AppSettings[key];
            }

            provider.RegisterConfiguration(conf);

            return provider.DynamiConfiguration;
        }
    }
}