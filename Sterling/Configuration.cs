using System.Configuration;

namespace Sterling
{
    internal static class Configuration
    {
        private static class ConfigurationKeys
        {
            public const string StrategyPollingFrequency = "StrategyPollingFrequency";
        }

        public static int StrategyPollingFrequency
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings[ConfigurationKeys.StrategyPollingFrequency]);
            }
        }
    }
}
