using KMorcinek.FilesBulkUpdater.Plugins;
using System.Collections.Generic;

namespace KMorcinek.FilesBulkUpdater
{
    public class FilesFinder
    {
        public IEnumerable<string> GetFilesByPattern(string basePath, IPlugin plugin)
        {
            return System.IO.Directory.EnumerateFiles(basePath, "*" + plugin.IncludePattern, System.IO.SearchOption.AllDirectories);
        }
    }
}
