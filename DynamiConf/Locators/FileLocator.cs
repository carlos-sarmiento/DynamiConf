using System;
using System.IO;
using System.Text;
using DynamiConf.Helpers;

namespace DynamiConf.Locators
{
    public static class FileLocator
    {
        public static DynamiConfiguration File(this LocationSources sources, string path, bool optional = false)
        {
            var str = string.Empty;
            try
            {
                str = System.IO.File.ReadAllText(path);
            }
            catch (IOException)
            {
                if (!optional)
                    throw;
            }

            var conf = sources.Interpreter.ParseConfiguration(str);
            sources.RegisterConfiguration(conf);

            return sources.Configuration;
        }

        public static DynamiConfiguration File(this LocationSources sources, string path, Encoding encoding, bool optional = false)
        {
            var str = string.Empty;
            try
            {
                str = System.IO.File.ReadAllText(path, encoding);
            }
            catch (IOException)
            {
                if (!optional)
                    throw;
            }

            var conf = sources.Interpreter.ParseConfiguration(str);
            sources.RegisterConfiguration(conf);

            return sources.Configuration;
        }

        public static DynamiConfiguration File(this LocationSources provider, Func<dynamic, dynamic> pathSelector, bool optional = false)
        {
            try
            {
                return File(provider, pathSelector.Invoke(provider.Configuration.GetConfiguration()) as string, optional);
            }
            catch (Exception)
            {
                if (!optional)
                    throw;
            }

            return provider.Configuration;
        }

        public static DynamiConfiguration File(this LocationSources provider, Func<dynamic, dynamic> pathSelector, Encoding encoding, bool optional = false)
        {
            try
            {
                return File(provider, pathSelector.Invoke(provider.Configuration.GetConfiguration()) as string, encoding, optional);
            }
            catch (Exception)
            {
                if (!optional)
                    throw;
            }

            return provider.Configuration;
        }
    }
}