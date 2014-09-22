using KMorcinek.FilesBulkUpdater.Plugins;
using System.Collections.Generic;

namespace KMorcinek.FilesBulkUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<IPlugin> plugins = new IPlugin[]
            {
                new AngularNamedControllerPlugin(),
                new JavaScriptIIFEPlugin(),
            };

            new Runner().Run(plugins, @"path/to/root/folder");
        }
    }
}
