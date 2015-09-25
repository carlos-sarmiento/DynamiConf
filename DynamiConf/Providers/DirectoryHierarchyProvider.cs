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
    public static class DirectoryHierarchyProvider
    {
        public static DynamiConfiguration ChainedJson(this ConfigurationSources provider, string resourcePostfix = "settings.conf.json")
        {
            var executionPath = AppDomain.CurrentDomain.BaseDirectory;

            provider.RegisterConfiguration(ExpandoObject2Configuration.Transform(LoadAllJsonFromChain(executionPath, resourcePostfix)));

            return provider.DynamiConfiguration;
        }


        private static ExpandoObject LoadAllJsonFromChain(string path, string resourcePostfix)
        {
            var files = new List<FileInfo>();

            var d = new DirectoryInfo(path);
            do
            {
                var userConfig =
                    d.GetFiles()
                        .Where(r => r.Name.EndsWith(resourcePostfix, StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(c => c.Name);

                files.AddRange(userConfig);
                d = d.Parent;
            } while (d != null);

            files.Reverse();

            return files.Select(c => File.ReadAllText(c.FullName))
                        .Aggregate(new ExpandoObject(), (current, resource) =>
                        {
                            var obj = JsonConvert.DeserializeObject<ExpandoObject>(resource, new ExpandoObjectConverter());
                            return current.UpdateWith(obj);
                        });
        }
    }
}