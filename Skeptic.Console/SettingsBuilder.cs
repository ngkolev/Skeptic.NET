using Skeptic.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Skeptic.Console
{
    internal class SettingsBuilder
    {
        public SettingsBuilder(ICollection<string> args)
        {
            this.Args = args;
        }

        public ICollection<string> Args { get; set; }

        public Settings Settings { get; private set; }

        public string RulePath { get; private set; }

        public bool TryBuild()
        {
            // Ensure at least one argument for the path
            if (Args.Count == 0 ||
                Args.First().TrimStart().StartsWith("-"))
            {
                return false;
            }

            var settings = Args.Joined(" ")
                .Split(
                    new[] { " -" },
                    StringSplitOptions.RemoveEmptyEntries);

            RulePath = settings.First().Trim();
            Settings = new Settings();

            var restSettings = settings.Skip(1);
            foreach (var item in restSettings)
            {
                var separatorIndex = item.IndexOf(" ");
                var key = item.Substring(0, separatorIndex).Trim();
                var value = item.Substring(separatorIndex + 1).Trim();
                Settings[key] = value;
            }


            return true;
        }
    }
}
