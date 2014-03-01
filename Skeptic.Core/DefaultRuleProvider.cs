using Skeptic.Core.Abstraction;
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

        public static DefaultRuleProvider Create()
        {
            var result = new DefaultRuleProvider();
           
            var config = new ContainerConfiguration();
            var assemblyPath = Assembly.LoadFrom(ConfigManager.Current.RuleLibraryPath);
            config.WithAssembly(assemblyPath);

            using (var container = config.CreateContainer())
            {
                container.SatisfyImports(result);
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
