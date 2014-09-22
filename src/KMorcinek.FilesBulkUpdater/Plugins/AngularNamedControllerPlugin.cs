using KMorcinek.FilesBulkUpdater.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KMorcinek.FilesBulkUpdater.Plugins
{
    public class AngularNamedControllerPlugin : IPlugin
    {
        private readonly PluginHelper pluginHelper = new PluginHelper();
        private readonly AngularPluginHelper angularPluginHelper = new AngularPluginHelper();

        public string IncludePattern
        {
            get { return ".js"; }
        }

        public string Convert(string text)
        {
            if (IsOnlyModuleDefinition(text))
                return text;

            bool isModuleDefinition = IsModuleDefinition(text);

            if (isModuleDefinition)
            {
                string moduleName = GetModuleName(text);
                string configFunctionName = GetConfigFunctionName(moduleName);

                string anonymousFunctionText = GetAnonymousFunctionText(text);

                string coreFunctionText = GetCoreFunctionText(anonymousFunctionText);

                coreFunctionText = RemoveOneIndent(coreFunctionText);

                text = text.Replace(anonymousFunctionText, configFunctionName);
                text = text.Insert(0, string.Format("function {0}{1}{2}{2}",
                    configFunctionName,
                    coreFunctionText,
                    Environment.NewLine));
            }
            else
            {
                string controllerName = GetControllerName(text);

                string anonymousFunctionText = GetAnonymousFunctionText(text);

                string coreFunctionText = GetCoreFunctionText(anonymousFunctionText);

                coreFunctionText = RemoveOneIndent(coreFunctionText);

                text = text.Replace(anonymousFunctionText, controllerName);
                text = text.Insert(0, string.Format("function {0}{1}{2}{2}",
                    controllerName,
                    coreFunctionText,
                    Environment.NewLine));
            }

            string angularDeclarationText = angularPluginHelper.GetAngularDeclarationText(text);
            string angularDeclarationWithoutNewLines = pluginHelper.GetWithoutNewLines(angularDeclarationText);
            angularDeclarationWithoutNewLines = pluginHelper.GetCompactedWhiteSpaces(angularDeclarationWithoutNewLines);

            text = text.Replace(angularDeclarationText, angularDeclarationWithoutNewLines);

            text = angularPluginHelper.MakeAngularDefinitionWithIndents(text);

            return text;
        }

        public string GetConfigFunctionName(string moduleName)
        {
            IEnumerable<string> parts = 
                moduleName
                .Split('.')
                .Select(StringExtensions.UppercaseFirstLetter)
                .ConcatAfter("Config");

            return string.Join("", parts);
        }

        private static string GetCoreFunctionText(string anonymousFunctionText)
        {
            Regex coreFunctionRegex = new Regex(@"\(.*}", RegexOptions.Singleline);
            string coreFunctionText = coreFunctionRegex.Match(anonymousFunctionText).Value;
            return coreFunctionText;
        }

        private static string GetAnonymousFunctionText(string text)
        {
            Regex anonymousFunctionPartRegex = new Regex(@"function.+}", RegexOptions.Singleline);
            string anonymousFunctionText = anonymousFunctionPartRegex.Match(text).Value;
            return anonymousFunctionText;
        }

        private string GetModuleName(string text)
        {
            Regex regex = new Regex(@"angular\s*.module\('(?<name>.+)', \[.*\]\)");
            return regex.Match(text).Groups["name"].Value;
        }

        private bool IsModuleDefinition(string text)
        {
            Regex regex = new Regex(@"angular\s*.module\('.+', \[.*\]\)");
            return regex.IsMatch(text);
        }

        private bool IsOnlyModuleDefinition(string text)
        {
            Regex regex = new Regex(@"angular\s*.module\('.+', \[.*\]\);\s*$");
            return regex.IsMatch(text);
        }

        private string RemoveOneIndent(string text)
        {
            Regex regex = new Regex("^" + PluginConstants.Indent, RegexOptions.Multiline);
            return regex.Replace(text, "");
        }

        private string GetControllerName(string text)
        {
            Regex regex = new Regex(
                @"\." + AngularPluginHelper.ControllerServiceOrFactoryRegex + @"\('(?<name>\w+)'");
            return regex.Match(text).Groups["name"].Value;
        }

        public IEnumerable<string> Convert(IEnumerable<string> lines)
        {
            throw new NotImplementedException();
        }

        public bool IsConverted(string text)
        {
            Regex regex = new Regex(@"function \w+\(.*\)\s*{");
            return regex.IsMatch(text);
        }
    }
}
