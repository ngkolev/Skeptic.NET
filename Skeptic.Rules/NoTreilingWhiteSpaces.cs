using Common;
using Skeptic.Abstraction;
using Skeptic.Model;
using System.Composition;
using System.Text.RegularExpressions;

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
                .ToLines();

            lines.ForEachWithIndex((i, line) =>
            {
                if (Regex.Match(line, "\\s+$").Captures.Count > 0)
                {
                    var violationText = "Line {0} has trailing white spaces".Formatted(i);
                    var violation = new RuleViolation(violationText);
                    Violations.Add(violation);
                }
            });
        }
    }
}
