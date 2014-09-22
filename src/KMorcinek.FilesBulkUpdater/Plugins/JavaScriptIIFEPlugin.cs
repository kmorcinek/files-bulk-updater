using System;
using System.Collections.Generic;
using System.Linq;
using KMorcinek.FilesBulkUpdater.Extensions;
using System.Text.RegularExpressions;

namespace KMorcinek.FilesBulkUpdater.Plugins
{
    public class JavaScriptIIFEPlugin : IPlugin
    {
        public string IncludePattern
        {
            get { return "*.js"; }
        }

        public string Convert(string text)
        {
            string[] lines = text.SplitByString(Environment.NewLine);
            IEnumerable<string> convertedLines = Convert(lines);

            return string.Join(Environment.NewLine, convertedLines);
        }

        public IEnumerable<string> Convert(IEnumerable<string> lines)
        {
            lines = lines.Select(line => PluginConstants.Indent + line);
            lines = new[] { "(function () {", PluginConstants.Indent + "'use strict';", "" }.Concat(lines).ConcatAfter("})();");
            return lines;
        }

        public bool IsConverted(string text)
        {
            return new Regex(@"\s*\(\s*function\s*\(\)\s*{").IsMatch(text);
        }
    }
}
