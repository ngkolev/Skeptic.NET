using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private string _cleanedSourceCode;

        /// <summary>
        /// Source code without comments, summaries, strings and empty lines
        /// </summary>
        public string SourceCodeCleaned
        {
            get
            {
                if (_cleanedSourceCode == null)
                {
                    _cleanedSourceCode = GetSourceCodeCleaned();
                }
                return _cleanedSourceCode;
            }
        }

        private string GetSourceCodeCleaned()
        {
            // Remove encoded quotes
            var codeWithoutQuotes = SourceCode.Replace("\\\"", "");

            // Remove strings
            var codeWithoutStrings = Regex.Replace(
                codeWithoutQuotes,
                "\".*?\"",
                "");

            // Remove multiline strings
            var codeWithoutMultilineStrings = Regex.Replace(
                codeWithoutQuotes,
                "@\"(.|\n|\r)*?\"",
                "",
                RegexOptions.Multiline);

            // Remove comments, summaries and empty lines
            var result = Regex.Replace(
                codeWithoutMultilineStrings,
                "(//.*?$)|(///.*?)|(^/s*?$)",
                "");

            return result;
        }
    }
}
