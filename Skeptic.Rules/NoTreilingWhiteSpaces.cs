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
    public class NoTreilingWhiteSpaces : IRule
    {
        public string Name
        {
            get { return "No trailing white spaces"; }
        }

        public string Description
        {
            get
            {
                return "No trailing white spaces are allowed";
            }
        }

        public RuleViolationCollection Violations { get; private set; }

        public Settings Settings { get; set; }

        public void Apply(RuleContext context)
        {
            Violations = new RuleViolationCollection();
            var lines = context.SourceCodeCleaned
                .ToLines()
                .Where(l => Regex.Match(l, "\\s+$").Captures.Count > 0);

            lines.ForEachWithIndex((i, line) =>
            {
                var violationText = "Line {0} has trailing white spaces".Formatted(i);
                var violation = new RuleViolation(violationText);
                Violations.Add(violation);
            });
        }
    }
}
