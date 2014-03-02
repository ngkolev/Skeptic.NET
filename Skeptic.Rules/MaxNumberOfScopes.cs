using Skeptic.Core.Abstraction;
using Skeptic.Core.Model;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;

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

            // Remove brackets
            var bracketChars = context.SourceCodeCleaned
                .ToArray()
                .Where(s => "{}".Contains(s));

            var nesting = 0;
            foreach (var ch in bracketChars)
            {
                if (ch == '{') nesting++;
                if (ch == '}') nesting--;
                if (MaxNumberOfScopesValue < nesting )
                {
                    var violationText = "Too deep nesting - {0} scopes".Formatted(nesting);
                    var violation = new RuleViolation(violationText);
                    Violations.Add(violation);
                }
                else if (nesting < 0)
                {
                    var violationText = "} with no matching {";
                    var violation = new RuleViolation(violationText);
                    Violations.Add(violation);
                }
            }

            if (nesting > 0)
            {
                var violationText = "{ with no matching }";
                var violation = new RuleViolation(violationText);
                Violations.Add(violation);
            }
        }
    }
}
