using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public static class StringExtensions
    {
        public static string Formatted(this string text, params object[] args)
        {
            return String.Format(text, args);
        }

        public static ICollection<string> ToLines(this string text)
        {
            var lines = Regex.Split(text, "\r\n|\r|\n").ToArray();
            return lines;
        }
    }
}
