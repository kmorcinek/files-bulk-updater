using KMorcinek.FilesBulkUpdater.Plugins;
using Xunit;
using Xunit.Should;

namespace KMorcinek.FilesBulkUpdater.Tests.Plugins
{
    public class AngularPluginHelperTests
    {
        private readonly AngularPluginHelper angularPluginHelper = new AngularPluginHelper();

        [Fact]
        public void AngularDefinitionWithIndents()
        {
            string input = @"angular.module('app').controller('AdminCtrl', AdminCtrl);";

            string result = angularPluginHelper.GetWithIndents(input);

            result.ShouldBe(@"angular
    .module('app')
    .controller('AdminCtrl', AdminCtrl);");
        }

        [Fact]
        public void WithIndentsForDottedModule()
        {
            string input = @"angular.module('admin.customer').service('AdminService', AdminService);";

            string result = angularPluginHelper.GetWithIndents(input);

            result.ShouldBe(@"angular
    .module('admin.customer')
    .service('AdminService', AdminService);");
        }

        [Fact]
        public void GetAngularDeclarationText()
        {
            string input = @"angular.module('app')
.controller('AdminService', AdminService);";

            string result = angularPluginHelper.GetAngularDeclarationText(input);

            result.ShouldBe(@"angular.module('app')
.controller('AdminService', AdminService);");
        }

        [Fact]
        public void GetAngularDeclarationTextForService()
        {
            string input = @"angular.module('app').service('AdminService', AdminService);";

            string result = angularPluginHelper.GetAngularDeclarationText(input);

            result.ShouldBe(@"angular.module('app').service('AdminService', AdminService);");
        }
    }
}
