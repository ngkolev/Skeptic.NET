using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
