using Common;
using Skeptic.Abstraction;
using Skeptic.Model;
using System.Composition;
using System.Linq;

namespace Skeptic.Rules
{
    [Export(typeof(IRule))]
    public class MaxNumberOfScopes : IRule
    {
        private const int DEFAULT_MAX_NUMBER_OF_SCOPES = 5;
        private const string MAX_NUMBER_OF_SCOPES_KEY = "max-scopes";

        public string Name
        {
            get { return "Max number of scopes"; }
        }

        public string Description
        {
            get
            {
                return "The number of scopes should not be greater than {0}".Formatted(MaxNumberOfScopesValue);
            }
        }

        public RuleViolationCollection Violations { get; private set; }

        public Settings Settings { get; set; }

        public int MaxNumberOfScopesValue
        {
            get
            {
                return Settings[MAX_NUMBER_OF_SCOPES_KEY].TryParseAsInt() ?? DEFAULT_MAX_NUMBER_OF_SCOPES;
            }
        }

        public void Apply(RuleContext context)
        {
            Violations = new RuleViolationCollection();

            var lines = context.SourceCodeCleaned.ToLines();
            var nesting = 0;

            // Remove brackets but preserve lines
            lines.ForEachWithIndex((i, line) =>
            {
                foreach (var ch in line)
                {
                    if (ch == '{') nesting++;
                    if (ch == '}') nesting--;
                    if (MaxNumberOfScopesValue < nesting)
                    {
                        var violationText = "Too deep nesting in line {0} - {1} scopes".Formatted(i+1,nesting);
                        var violation = new RuleViolation(violationText);
                        Violations.Add(violation);
                    }
                    else if (nesting < 0)
                    {
                        var violationText = "} with no matching { on line {0}".Formatted(i + 1);
                        var violation = new RuleViolation(violationText);
                        Violations.Add(violation);
                    }
                }
            });
            

            if (nesting > 0)
            {
                var violationText = "{ with no matching } on line {0}".Formatted(lines.Count);
                var violation = new RuleViolation(violationText);
                Violations.Add(violation);
            }
        }
    }
}
