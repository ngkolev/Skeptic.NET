using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeptic.Core.Model
{
    public class RuleViolation
    {
        public RuleViolation(string text)
        {
            this.Text = text;
        }

        public string Text { get; private set; }
    }
}
