using System.Dynamic;
using DynamiConf.Providers.Helpers;

namespace DynamiConf.Providers
{
    public static class ExpandoObjectProvider
    {
        public static DynamiConfiguration ExpandoObject(this ConfigurationSources provider, ExpandoObject obj)
        {
            provider.RegisterConfiguration(ExpandoObject2Configuration.Transform(obj));
            return provider.DynamiConfiguration;
        }
    }
}