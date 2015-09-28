using System;
using DynamiConf.Helpers;

namespace DynamiConf.Interpreters
{
    public static class EnvironmentVarsInterpreter
    {
        public static DynamiConfiguration EnvironmentVars(this InterpreterSources provider, params string[] vars)
        {
            var conf = new Configuration();

            foreach (var @var in vars)
            {
                conf[@var] = Environment.GetEnvironmentVariable(@var);
            }

            provider.RegisterConfiguration(conf);

            return provider.Configuration;
        }
    }
}