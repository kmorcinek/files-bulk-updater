using System;

namespace KMorcinek.FilesBulkUpdater.Extensions
{
    public static class StringExtensions
    {
        public static string[] SplitByString(this string input, string separator)
        {
            return input.Split(new[] { separator }, StringSplitOptions.None);
        }

        public static string UppercaseFirstLetter(this string input)
        {
            string first = input.Substring(0, 1).ToUpperInvariant();
            string rest = input.Substring(1);

            return first + rest;
        }
    }
}
