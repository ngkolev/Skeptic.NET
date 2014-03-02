using Common;
using Skeptic.Abstraction;
using Skeptic.Model;
using System.Composition;

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
                .ToLines();

            lines.ForEachWithIndex((i, line) =>
            {
                if (line.Contains("goto"))
                {
                    var violationText = "Line {0} with use of goto".Formatted(i + 1);
                    var violation = new RuleViolation(violationText);
                    Violations.Add(violation);
                }
            });
        }
    }
}
