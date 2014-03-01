using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
