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
        public static DynamiConfiguration EmbeddedResources(this LocationSources provider, string resourcePostfix = "default.conf")
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var referencedAssemblies = assemblies.SelectMany(c => c.GetReferencedAssemblies());

            var assemblyQueue = new Queue<AssemblyName>(referencedAssemblies);
            var assemblyList = new List<Assembly>();
            var assemblyHash = new HashSet<string>();

            while (assemblyQueue.Count > 0)
            {
                var referencedAssembly = assemblyQueue.Dequeue();

                if (assemblyHash.Contains(referencedAssembly.FullName))
                    continue;

                var assembly = Assembly.Load(referencedAssembly);

                foreach (var aa in assembly.GetReferencedAssemblies())
                {
                    assemblyQueue.Enqueue(aa);
                }

                assemblyList.Add(assembly);
                assemblyHash.Add(assembly.FullName);
            }

            var resources = GetExpandoFromAssemblies(assemblies.Union(assemblyList), resourcePostfix, provider.Interpreter);

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