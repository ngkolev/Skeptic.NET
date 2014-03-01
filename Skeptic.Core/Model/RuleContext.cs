using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeptic.Core.Model
{
    public class RuleContext 
    {
        public RuleContext(string sourceCode)
        {
            this.SourceCode = sourceCode;
        }

        public string SourceCode { get; private set; }
    }
}
