using KMorcinek.FilesBulkUpdater.Extensions;
using System;
using System.Text.RegularExpressions;

namespace KMorcinek.FilesBulkUpdater.Plugins
{
    public class PluginHelper
    {
        public string GetWithoutNewLines(string text)
        {
            string[] lines = text.SplitByString(Environment.NewLine);
            return string.Join("", lines);
        }

        public string GetCompactedWhiteSpaces(string text)
        {
            Regex regex = new Regex(@"\s+");
            return regex.Replace(text, " ");
        }
    }
}
