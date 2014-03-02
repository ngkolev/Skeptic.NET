using System.Text.RegularExpressions;
using System.Linq;
using Common;

namespace Skeptic.Model
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

            // Remove multiline strings. 
            var codeWithoutMultilineStrings = GetCodeWithoutMultilines(codeWithoutStrings);

            // Remove comments, summaries and empty lines
            var result = Regex.Replace(
                codeWithoutMultilineStrings,
                "(//.*?$)|(///.*?)|(^/s*?$)",
                "");

            return result;
        }

        private string GetCodeWithoutMultilines(string code)
        {
            // Note that we want to preserve new lines to have proper line count
            var inStringFlag = false;
            var lines = code.ToLines();
            var result = lines.Select(line =>
            {
                if (inStringFlag)
                {
                    if (line.Contains('"'))
                    {
                        inStringFlag = false;
                        return Regex.Replace(line, "^.*?\"", "");
                    }
                    else
                    {
                        return "";
                    }
                }
                else if (line.Contains("@\""))
                {
                    inStringFlag = true;
                    return Regex.Replace(line, "@\".*?$", "");
                }
                else
                {
                    return line;
                }

            }).Joined();

            return result;
        }
    }
}
