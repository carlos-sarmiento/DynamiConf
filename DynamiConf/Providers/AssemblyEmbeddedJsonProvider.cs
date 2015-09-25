using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using DynamiConf.Providers.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DynamiConf.Providers
{
    public static class AssemblyEmbeddedJsonProvider
    {
        public static DynamiConfiguration AssemblyEmbeddedJson(this ConfigurationSources provider, string resourcePostfix = "default.conf.json")
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var resources = GetExpandoFromAssemblies(assemblies.Where(assembly => assembly != entryAssembly), resourcePostfix);

            if (entryAssembly != null)
                resources.UpdateWith(GetExpandoFromAssemblies(new[] { entryAssembly }, resourcePostfix));

            provider.RegisterConfiguration(ExpandoObject2Configuration.Transform(resources));
            return provider.DynamiConfiguration;
        }

        public static DynamiConfiguration AssemblyEmbeddedJson(this ConfigurationSources provider, IEnumerable<Assembly> assemblies, string resourcePostfix = "default.conf.json")
        {
            var resources = GetExpandoFromAssemblies(assemblies, resourcePostfix);

            provider.RegisterConfiguration(ExpandoObject2Configuration.Transform(resources));
            return provider.DynamiConfiguration;
        }

        private static ExpandoObject GetExpandoFromAssemblies(IEnumerable<Assembly> assemblies, string resourcePostfix)
        {
            return assemblies.SelectMany(assembly => ReadEmbededResources(assembly, resourcePostfix)).Aggregate(new ExpandoObject(), (current, resource) =>
            {
                var obj = JsonConvert.DeserializeObject<ExpandoObject>(resource, new ExpandoObjectConverter());
                return current.UpdateWith(obj);
            });
        }

        private static IEnumerable<string> ReadEmbededResources(Assembly assembly, string resourcePostfix)
        {
            if (assembly == null)
                yield break;

            string[] resources;
            try
            {
                resources = assembly.GetManifestResourceNames();
            }
            catch
            { yield break; }

            var configurationResources = resources.Where(r => r.EndsWith(resourcePostfix, StringComparison.OrdinalIgnoreCase));

            foreach (var stream in configurationResources.Select(assembly.GetManifestResourceStream))
            {
                yield return new StreamReader(stream).ReadToEnd();
            }
        }
    }
}