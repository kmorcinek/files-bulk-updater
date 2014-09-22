using KMorcinek.FilesBulkUpdater.Extensions;
using KMorcinek.FilesBulkUpdater.Plugins;
using System;
using Xunit;
using Xunit.Should;

namespace KMorcinek.FilesBulkUpdater.Tests.Plugins
{
    public class AngularNamedControllerPluginTests
    {
        private readonly AngularNamedControllerPlugin plugin = new AngularNamedControllerPlugin();

        [Fact]
        public void Test()
        {
            string start = @"angular.module('YetAnotherTodo').controller('AdminCtrl',
    function() {
    });";

            string[] result = plugin.Convert(start).SplitByString(Environment.NewLine);

            result[0].ShouldBe("function AdminCtrl() {");
        }

        [Fact]
        public void FunctionBodyIsLessIntended()
        {
            string start = @"angular.module('YetAnotherTodo').controller('AdminCtrl',
    function() {
    });";

            string[] result = plugin.Convert(start).Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            result[1].ShouldBe("}");
        }

        [Fact]
        public void ModuleDefinitionWithConfig()
        {
            string start = @"angular.module('admin.customers', ['ngRoute'])
    .config(function($routeProvider) {
    });";

            string result = plugin.Convert(start);

            result.ShouldBe(@"function AdminCustomersConfig($routeProvider) {
}

angular
    .module('admin.customers', ['ngRoute'])
    .config(AdminCustomersConfig);");
        }

        [Fact]
        public void OnlyModuleDefinition()
        {
            string input = "angular.module('commonDirectives', ['underscore']);";

            string result = plugin.Convert(input);

            result.ShouldBe("angular.module('commonDirectives', ['underscore']);");
        }

        [Fact]
        public void CreateConfigFunctionNameFromModuleName()
        {
            string moduleName = "admin.customers.edit";

            string configFunctionName = plugin.GetConfigFunctionName(moduleName);

            configFunctionName.ShouldBe("AdminCustomersEditConfig");
        }
    }
}
