using System.Configuration;

namespace Sterling
{
    internal static class Configuration
    {
        private static class ConfigurationKeys
        {
            public const string QuotesProviderPass = "QuotesProviderPass";
            public const string QuotesProviderUser = "QuotesProviderUser";
            public const string StrategyPollingFrequency = "StrategyPollingFrequency";
        }

        public static int StrategyPollingFrequency
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings[ConfigurationKeys.StrategyPollingFrequency]);
            }
        }

        public static string QuotesProviderPass
        {
            get
            {
                return ConfigurationManager.AppSettings[ConfigurationKeys.QuotesProviderPass];
            }
        }

        public static string QuotesProviderUser
        {
            get
            {
                return ConfigurationManager.AppSettings[ConfigurationKeys.QuotesProviderUser];
            }
        }
    }
}
