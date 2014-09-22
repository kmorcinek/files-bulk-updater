using System.Collections.Generic;
using System.Linq;

namespace KMorcinek.FilesBulkUpdater.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<string> Concat(this string item, IEnumerable<string> items)
        {
            return new[] { item }.Concat(items);
        }

        public static IEnumerable<string> ConcatAfter(this IEnumerable<string> items, string item)
        {
            return items.Concat(new[] { item });
        }
    }
}
