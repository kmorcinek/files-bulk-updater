using System;
using System.Text.RegularExpressions;

namespace KMorcinek.FilesBulkUpdater.Plugins
{
    public class AngularPluginHelper
    {
        public const string ControllerServiceOrFactoryRegex = @"(?:controller|service|factory|config|directive)";

        public string MakeAngularDefinitionWithIndents(string text)
        {
            string angularDeclarationText = GetAngularDeclarationText(text);

            string withIndents = GetWithIndents(angularDeclarationText);

            return text.Replace(angularDeclarationText, withIndents);
        }

        public string GetWithIndents(string text)
        {
            Regex regex = new Regex(@"\s*\.(module|controller|service|factory|config|directive)");
            string delimitedWithNewLines = regex.Replace(text, Environment.NewLine + PluginConstants.Indent + @".$1");

            return delimitedWithNewLines;
        }

        public string GetAngularDeclarationText(string text)
        {
            Regex angularDeclarationRegex = new Regex(
                @"angular\s*.module\('[\w.]+'.*\)\s*\." + ControllerServiceOrFactoryRegex + @"\(.*\);", RegexOptions.Singleline);

            return angularDeclarationRegex.Match(text).Value;
        }
    }
}
