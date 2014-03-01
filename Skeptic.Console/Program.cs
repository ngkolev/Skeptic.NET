using Skeptic.Core;
using Skeptic.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeptic.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ensure correct arguments
            if (args.Length != 1)
            {
                System.Console.WriteLine("Call using path to the source code file");
            }

            // Read file and create rule context
            var sourceCodePath = args[0];
            var sourceCode = TryReadSourceCode(sourceCodePath);

            if (sourceCode != null)
            {
                var ruleContext = new RuleContext(sourceCode);

                // Evaluate the code with skeptic
                var critic = new Critic(ruleContext);
                critic.Criticize();

                // Output the result
                var skepticWriter = new SkepticWriter(critic, System.Console.Out);
                skepticWriter.Write();
            }
            else // The file was not found
            {
                System.Console.WriteLine("File not found");
            }
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
