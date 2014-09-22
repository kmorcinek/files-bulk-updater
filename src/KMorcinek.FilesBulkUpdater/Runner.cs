using KMorcinek.FilesBulkUpdater.Plugins;
using System.Collections.Generic;
using System.IO;

namespace KMorcinek.FilesBulkUpdater
{
    class Runner
    {
        public void Run(IEnumerable<IPlugin> plugins, string basePath)
        {
            FilesFinder filesFinder = new FilesFinder();

            foreach (var plugin in plugins)
            {
                foreach (var filePath in filesFinder.GetFilesByPattern(basePath, plugin))
                {
                    string fileContent = File.ReadAllText(filePath);

                    if (plugin.IsConverted(fileContent))
                        continue;

                    fileContent = plugin.Convert(fileContent);
                    File.WriteAllText(filePath, fileContent);
                }
            }
        }
    }
}
