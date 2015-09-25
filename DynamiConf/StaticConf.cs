using System;

namespace DynamiConf
{
    public static class StaticConf
    {
        private static dynamic _configuration;
        /// <summary>
        /// Gets or sets a Configuration file that is shared by the full process. It is only provided as a convenience; nothing in DynamiConf has shared state.
        /// </summary>
        public static dynamic Configuration
        {
            get { return _configuration; }
            set
            {
                if (!(Configuration is Configuration))
                    throw new ArgumentException("The static property Configuration can only be set on an instance of a DynamiConf.Configuration object", "Configuration");

                _configuration = value;
            }
        }
    }
}