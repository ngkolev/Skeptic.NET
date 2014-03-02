using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class EnumerableExtensions
    {
        public static void ForEachWithIndex<T>(this IEnumerable<T> items, Action<int, T> action)
        {
            var arr = items.ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                action(i, arr[i]);
            }
        }

        public static string Joined(this IEnumerable<string> strings, string separator = "")
        {
            return String.Join(separator, strings);
        }
    }
}
