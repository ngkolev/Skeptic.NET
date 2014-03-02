using Common;
using Skeptic.Abstraction;
using Skeptic.Model;
using System.Composition;

namespace Skeptic.Rules
{
    [Export(typeof(IRule))]
    public class MaxLengthOfALine : IRule
    {
        private const int DEFAULT_MAX_LINE_LENGTH = 79;
        private const string MAX_LINE_LENGTH_SETTING_KEY = "max-line-length";

        public string Name
        {
            get { return "Max length of a line"; }
        }

        public string Description
        {
            get
            {
                return "The length of a line should be no more than {0} symbols".Formatted(MaxLineLength);
            }
        }

        public RuleViolationCollection Violations { get; private set; }

        public Settings Settings { get; set; }

        public int MaxLineLength
        {
            get
            {
                return Settings[MAX_LINE_LENGTH_SETTING_KEY].TryParseAsInt() ?? DEFAULT_MAX_LINE_LENGTH;
            }
        }

        public void Apply(RuleContext context)
        {
            Violations = new RuleViolationCollection();
            var lines = context.SourceCode.ToLines();

            lines.ForEachWithIndex((i, line) =>
            {
                if (line.Length > MaxLineLength)
                {
                    var violationText = "Line {0} has {1} symbols".Formatted(i, line.Length);
                    var violation = new RuleViolation(violationText);
                    Violations.Add(violation);
                }
            });
        }
    }
}
