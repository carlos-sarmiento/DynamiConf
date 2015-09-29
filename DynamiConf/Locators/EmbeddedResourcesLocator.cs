using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DynamiConf.Helpers;

namespace DynamiConf.Locators
{
    public static class EmbeddedResourcesLocator
    {
        public static DynamiConfiguration EmbeddedResources(this LocationSources provider, string resourcePostfix = "default.conf", params string[] namespacesToLoad)
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var referencedAssemblies = assemblies.SelectMany(c => c.GetReferencedAssemblies());

            var extraList = new Dictionary<string, Assembly>();

            foreach (var referencedAssembly in referencedAssemblies)
            {
                foreach (var @namespace in namespacesToLoad)
                {
                    if (referencedAssembly.FullName.StartsWith(@namespace) && !extraList.ContainsKey(referencedAssembly.FullName))
                    {
                        extraList.Add(referencedAssembly.FullName, Assembly.Load(referencedAssembly));
                    }
                }
            }

            var resources = GetExpandoFromAssemblies(assemblies.Where(assembly => assembly != entryAssembly).Union(extraList.Values), resourcePostfix, provider.Interpreter);

            if (entryAssembly != null)
                resources = resources.UpdateWith(GetExpandoFromAssemblies(new[] { entryAssembly }, resourcePostfix, provider.Interpreter));

            provider.RegisterConfiguration(resources);
            return provider.Configuration;
        }

        public static DynamiConfiguration EmbeddedResources(this LocationSources provider, IEnumerable<Assembly> assemblies, string resourcePostfix = "default.conf")
        {
            var resources = GetExpandoFromAssemblies(assemblies, resourcePostfix, provider.Interpreter);

            provider.RegisterConfiguration(resources);
            return provider.Configuration;
        }

        private static Configuration GetExpandoFromAssemblies(IEnumerable<Assembly> assemblies, string resourcePostfix, IConfigurationInterpreter interpreter)
        {
            return assemblies.SelectMany(assembly => ReadEmbededResources(assembly, resourcePostfix)).Aggregate(new Configuration(), (current, resource) =>
            {
                var obj = interpreter.ParseConfiguration(resource);
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
            {
                // Dynamic assemblies may explode when we try to get Resources from them. 
                yield break;
            }

            var configurationResources = resources.Where(r => r.EndsWith(resourcePostfix, StringComparison.OrdinalIgnoreCase));

            foreach (var stream in configurationResources.Select(assembly.GetManifestResourceStream))
            {
                yield return new StreamReader(stream).ReadToEnd();
            }
        }
    }
}