using Skeptic.Abstraction;
using Skeptic.Model;
using System.Collections.Generic;
using System.Linq;

namespace Skeptic.Core
{
    public class Critic
    {
        public RuleContext Context { get; private set; }

        public Settings Settings { get; private set; }

        public IRuleProvider RuleProvider { get; set; }

        public IEnumerable<IRule> AllRules { get; private set; }

        public IEnumerable<IRule> ViolatedRules
        {
            get
            {
                return AllRules.Where(r => r.Violations.Any());
            }
        }

        public Critic(RuleContext context, Settings settings = null, IRuleProvider ruleProvider = null)
        {
            Context = context;
            Settings = settings ?? new Settings();
            RuleProvider = ruleProvider ?? DefaultRuleProvider.Create(Settings);
        }

        public void Criticize()
        {
            AllRules = RuleProvider.GetRules();
            foreach (var rule in AllRules)
            {
                rule.Apply(Context);
            }
        }
    }
}
