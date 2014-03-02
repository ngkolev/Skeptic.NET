using Skeptic.Core.Abstraction;
using Skeptic.Core.Model;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Skeptic.Core
{
    internal class DefaultRuleProvider : IRuleProvider
    {
        private DefaultRuleProvider()
        {
        }

        public static DefaultRuleProvider Create(Settings settings)
        {
            var result = new DefaultRuleProvider();
           
            // Inject rules using MEF
            var config = new ContainerConfiguration();
            var assemblyPath = Assembly.LoadFrom(ConfigManager.Current.RuleLibraryPath);
            config.WithAssembly(assemblyPath);

            using (var container = config.CreateContainer())
            {
                container.SatisfyImports(result);
            }

            // Set rules settings
            foreach (var rule in result.Rules)
            {
                rule.Settings = settings;
            }

            return result;
        }

        [ImportMany]
        public IEnumerable<IRule> Rules { get; set; }

        public IEnumerable<IRule> GetRules()
        {
            return Rules;
        }
    }
}
