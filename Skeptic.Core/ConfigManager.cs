using System.Configuration;

namespace Skeptic.Core
{
    internal class ConfigManager
    {
        private const string RULE_LIBRARY_PATH = "ruleLibraryPath";

        private static ConfigManager current = new ConfigManager();

        private ConfigManager()
        {
        }

        public static ConfigManager Current
        {
            get { return current; }
        }

        public string RuleLibraryPath
        {
            get
            {
                return ConfigurationManager.AppSettings[RULE_LIBRARY_PATH];
            }
        }
    }
}
