using KMorcinek.FilesBulkUpdater.Plugins;
using System.Linq;
using Xunit;
using Xunit.Should;

namespace KMorcinek.FilesBulkUpdater.Tests.Plugins
{
    public class JavaScritpIIFEPluginTests
    {
        private readonly JavaScriptIIFEPlugin plugin = new JavaScriptIIFEPlugin();

        [Fact]
        public void Test()
        {
            string[] result = plugin.Convert(new[] { "angular.anythin();" }).ToArray();
            result[0].ShouldBe("(function () {");
            result[1].ShouldBe("    'use strict';");
            result[2].ShouldBe("");
            result[3].ShouldBe("    angular.anythin();");
            result[4].ShouldBe("})();");
        }
    }
}
