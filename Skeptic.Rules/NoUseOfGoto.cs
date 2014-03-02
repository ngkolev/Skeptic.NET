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
    public class NoUseOfGoto : IRule
    {
        public string Name
        {
            get { return "No use of goto keyword"; }
        }

        public string Description
        {
            get
            {
                return "The goto keyword should not be used";
            }
        }

        public RuleViolationCollection Violations { get; private set; }

        public Settings Settings { get; set; }

        public void Apply(RuleContext context)
        {
            Violations = new RuleViolationCollection();
            var lines = context.SourceCodeCleaned
                .ToLines()
                .Where(l => l.Contains("goto"));

            lines.ForEachWithIndex((i, line) =>
            {
                var violationText = "Line {0} with use of goto".Formatted(i);
                var violation = new RuleViolation(violationText);
                Violations.Add(violation);
            });
        }
    }
}
