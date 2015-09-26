using System.Text.RegularExpressions;
using DynamiConf.Helpers;
using IniParser.Parser;

namespace DynamiConf.IniInterpreter
{
    public static class IniFormatInterpreter
    {
        public static LocationFinder IniFormat(this InterpreterSources sources)
        {
            return new LocationFinder(sources, new IniFormatParserConfigurationInterpreter());
        }
    }

    public class IniFormatParserConfigurationInterpreter : IConfigurationInterpreter
    {
        public Configuration ParseConfiguration(string configuration)
        {
            var parser = new IniDataParser();
            var data = parser.Parse(configuration);

            var config = new Configuration();

            foreach (var section in data.Sections)
            {
                var innerConfig = new Configuration();

                foreach (var key in section.Keys)
                {
                    innerConfig[RemoveWhiteSpace(key.KeyName)] = key.Value;
                }

                config[RemoveWhiteSpace(section.SectionName)] = innerConfig;
            }

            foreach (var key in data.Global)
            {
                config[RemoveWhiteSpace(key.KeyName)] = key.Value;
            }

            return config;
        }

        private static readonly Regex WhitespaceCleaner = new Regex(@"\s", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        private static string RemoveWhiteSpace(string str)
        {
            return WhitespaceCleaner.Replace(str, "");
        }
    }
}