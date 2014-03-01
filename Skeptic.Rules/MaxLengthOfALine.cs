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
    public class MaxLengthOfALine : IRule
    {
        private const int MAX_LINE_LENGTH = 79;

        public string Name
        {
            get { return "Max length of a line"; }
        }

        public string Description
        {
            get
            {
                return "The length of a line should be no more than {0} symbols".Formatted(MAX_LINE_LENGTH);
            }
        }

        public RuleViolationCollection Violations { get; private set; }

        public void Apply(RuleContext context)
        {
            Violations = new RuleViolationCollection();
            var lines = context.SourceCode.ToLines();

            lines.ForEachWithIndex((i, line) =>
            {
                if (line.Length > MAX_LINE_LENGTH)
                {
                    var violationText = "Line {0} has {1} symbols".Formatted(i, line.Length);
                    var violation = new RuleViolation(violationText);
                    Violations.Add(violation);
                }
            });
        }
    }
}
