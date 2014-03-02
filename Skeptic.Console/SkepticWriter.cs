using Skeptic.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common;

namespace Skeptic.Console
{
    internal class SkepticWriter
    {
        public SkepticWriter(Critic critic, TextWriter writer)
        {
            Critic = critic;
            Writer = writer;
        }

        public TextWriter Writer { get; private set; }
        public Critic Critic { get; private set; }

        public void Write()
        {
            var violatedRules = Critic.ViolatedRules.ToArray();
            if (violatedRules.Any())
            {
                Writer.WriteLine("Skeptic found the following code violations:");
                violatedRules.ForEachWithIndex((i, rule) =>
                {
                    Writer.WriteLine("{0}. {1} - {2}:", (i + 1), rule.Name, rule.Description);

                    foreach (var ruleViolation in rule.Violations)
                    {
                        Writer.WriteLine("\t{0}", ruleViolation.Text);
                    }
                });
            }
            else
            {
                Writer.WriteLine("No code violations");
            }
        }
    }
}
