using System.Dynamic;
using DynamiConf.Providers.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DynamiConf.Providers
{
    public static class JsonStringProvider
    {
        public static DynamiConfiguration JsonString(this ConfigurationSources provider, string json)
        {
            var expando = JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());

            provider.RegisterConfiguration(ExpandoObject2Configuration.Transform(expando));

            return provider.DynamiConfiguration;
        }
    }
}