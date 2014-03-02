using Skeptic.Core;
using Skeptic.Model;
using System.Collections.Generic;
using System.IO;
using Common;

namespace Skeptic.Console
{
    internal class SkepticConsole
    {
        public SkepticConsole(ICollection<string> args)
        {
            SettingsBuilder = new SettingsBuilder(args);
        }

        public SettingsBuilder SettingsBuilder { get; private set; }

        public void Criticize()
        {
            // Ensure correct arguments
            if (SettingsBuilder.TryBuild())
            {
                CriticizeInternal();
            }
            else
            {
                "Call using path to the source code file as a first argument".ConsoleWriteLine();
            }
        }

        private void CriticizeInternal()
        {
            // Read file and create rule context
            var sourceCodePath = SettingsBuilder.RulePath;
            var sourceCode = TryReadSourceCode(sourceCodePath);

            if (sourceCode != null)
            {
                var ruleContext = new RuleContext(sourceCode);

                // Evaluate the code with skeptic
                Critic critic = TryCreateCritic(ruleContext);
                if (critic != null)
                {
                    critic.Criticize();

                    // Output the result
                    var skepticWriter = new SkepticWriter(critic, System.Console.Out);
                    skepticWriter.Write();
                }
            }
            else // The file was not found
            {
                "Cannot load file to evaluate".ConsoleWriteLine();
            }
        }

        private Critic TryCreateCritic(RuleContext ruleContext)
        {
            Critic critic = null;
            try
            {
                critic = new Critic(ruleContext, SettingsBuilder.Settings);
            }
            catch (FileNotFoundException ex)
            {
                "Cannot load rules file - {0}".ConsoleWriteLine(ex.FileName);
            }
            return critic;
        }

        private static string TryReadSourceCode(string sourceCodePath)
        {
            string result = null;
            using (var reader = new StreamReader(sourceCodePath))
            {
                try
                {
                    result = reader.ReadToEnd();
                }
                catch (FileNotFoundException) { }
                catch (DirectoryNotFoundException) { }
            }
            return result;
        }

    }
}
