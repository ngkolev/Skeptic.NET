using Skeptic.Core.Model;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeptic.Core.Abstraction
{
    public interface IRule
    {
        string Name { get; }
        string Description { get; }

        RuleViolationCollection Violations { get; }

       void Apply(RuleContext context);
    }
}
