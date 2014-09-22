using KMorcinek.FilesBulkUpdater.Plugins;
using Xunit;
using Xunit.Should;

namespace KMorcinek.FilesBulkUpdater.Tests.Plugins
{
    class PluginHelperTests
    {
        private readonly PluginHelper pluginHelper = new PluginHelper();

        [Fact]
        public void AngularDeclarationIsWithoutNewLines()
        {
            string input = @"angular.module('YetAnotherTodo').controller('AdminCtrl', AdminCtrl
);";
            string result=  pluginHelper.GetWithoutNewLines(input);
            result.ShouldBe("angular.module('YetAnotherTodo').controller('AdminCtrl', AdminCtrl);");
        }
    }
}
