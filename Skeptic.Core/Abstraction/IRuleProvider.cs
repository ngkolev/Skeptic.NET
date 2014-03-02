using Skeptic.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeptic.Core.Abstraction
{
    public interface IRuleProvider
    {
        IEnumerable<IRule> GetRules();
    }
}
