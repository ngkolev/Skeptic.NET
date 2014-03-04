using System.Collections.Generic;

namespace Skeptic.Abstraction
{
    public interface IRuleProvider
    {
        IEnumerable<IRule> GetRules();
    }
}
