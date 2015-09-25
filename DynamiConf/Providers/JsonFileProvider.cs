using System.Text;

namespace DynamiConf.Providers
{
    public static class JsonFileProvider
    {
        public static DynamiConfiguration JsonFile(this ConfigurationSources provider, string path)
        {
            var srt = System.IO.File.ReadAllText(path);
            return provider.JsonString(srt);
        }

        public static DynamiConfiguration JsonFile(this ConfigurationSources provider, string path, Encoding encoding)
        {
            var srt = System.IO.File.ReadAllText(path, encoding);
            return provider.JsonString(srt);
        }
    }
}