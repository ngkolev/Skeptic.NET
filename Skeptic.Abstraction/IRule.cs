using Skeptic.Model;

namespace Skeptic.Abstraction
{
    public interface IRule
    {
        string Name { get; }
        string Description { get; }

        Settings Settings { get; set; }

        RuleViolationCollection Violations { get; }

        void Apply(RuleContext context);
    }
}
