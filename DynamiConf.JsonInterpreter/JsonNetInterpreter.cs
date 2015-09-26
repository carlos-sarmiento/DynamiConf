using System.Dynamic;
using DynamiConf.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DynamiConf.JsonInterpreter
{
    public static class JsonNetInterpreter
    {
        public static LocationFinder Json(this InterpreterSources sources)
        {
            return new LocationFinder(sources, new JsonNetConfigurationInterpreter());
        }

        public static LocationFinder Json(this InterpreterSources sources, JsonSerializerSettings settings)
        {
            return new LocationFinder(sources, new JsonNetConfigurationInterpreter(settings));
        }
    }

    public class JsonNetConfigurationInterpreter : IConfigurationInterpreter
    {
        private readonly JsonSerializerSettings _settings;

        public JsonNetConfigurationInterpreter()
        { }

        public JsonNetConfigurationInterpreter(JsonSerializerSettings settings)
        {
            _settings = settings;
            _settings.Converters.Insert(0, new ExpandoObjectConverter());
        }

        public Configuration ParseConfiguration(string configuration)
        {
            var expando = _settings != null
                ? JsonConvert.DeserializeObject<ExpandoObject>(configuration, _settings)
                : JsonConvert.DeserializeObject<ExpandoObject>(configuration, new ExpandoObjectConverter());

            return ExpandoObject2Configuration.Transform(expando);
        }
    }
}