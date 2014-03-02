using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        public static int? TryParseAsInt(this string text)
        {
            int number;
            if (int.TryParse(text, out number))
            {
                return number;
            }
            return null;
        }

        public static void ConsoleWrite(this string text, params object[] args)
        {
            Console.Write(text, args);
        }

        public static void ConsoleWriteLine(this string text, params object[] args)
        {
            Console.WriteLine(text, args);
        }
    }
}
