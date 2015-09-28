using System;
using System.Net.Http;
using DynamiConf.Helpers;

namespace DynamiConf.HttpLocator
{
    public static class HttpUrlLocator
    {
        public static DynamiConfiguration HttpUrl(this LocationSources sources, string url, bool optional = false)
        {

            var str = string.Empty;

            try
            {
                using (var client = new HttpClient())
                using (var response = client.GetAsync(url).Result)
                using (var content = response.Content)
                {
                    str = content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception)
            {
                if (!optional)
                    throw;
            }

            var conf = sources.Interpreter.ParseConfiguration(str);
            sources.RegisterConfiguration(conf);

            return sources.Configuration;
        }

        public static DynamiConfiguration HttpUrl(this LocationSources provider, Func<dynamic, dynamic> urlSelector, bool optional = false)
        {
            try
            {
                return HttpUrl(provider, urlSelector.Invoke(provider.Configuration.GetConfiguration()) as string, optional);
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