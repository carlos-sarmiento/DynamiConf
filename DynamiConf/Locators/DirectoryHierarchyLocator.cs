using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DynamiConf.Helpers;

namespace DynamiConf.Locators
{
    public static class DirectoryHierarchyLocator
    {
        public static DynamiConfiguration ChainedHierarchy(this LocationSources provider, string resourcePostfix = "settings.conf")
        {
            var executionPath = AppDomain.CurrentDomain.BaseDirectory;

            provider.RegisterConfiguration(LoadAllFilesFromChain(executionPath, resourcePostfix, provider.Interpreter));

            return provider.Configuration;
        }

        private static Configuration LoadAllFilesFromChain(string path, string resourcePostfix, IConfigurationInterpreter interpreter)
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
                        .Aggregate(new Configuration(), (current, resource) =>
                        {
                            var obj = interpreter.ParseConfiguration(resource);
                            return current.UpdateWith(obj);
                        });
        }
    }
}