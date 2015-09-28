using System.Dynamic;
using DynamiConf.Helpers;

namespace DynamiConf.Interpreters
{
    public static class ExpandoObjectInterpreter
    {
        public static DynamiConfiguration ExpandoObject(this InterpreterSources provider, ExpandoObject obj)
        {
            provider.RegisterConfiguration(ExpandoObject2Configuration.Transform(obj));
            return provider.Configuration;
        }
    }
}