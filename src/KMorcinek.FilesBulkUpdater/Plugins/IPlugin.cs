using System.Collections.Generic;

namespace KMorcinek.FilesBulkUpdater.Plugins
{
    public interface IPlugin
    {
        string IncludePattern { get; }
        string Convert(string text);
        IEnumerable<string> Convert(IEnumerable<string> lines);
        bool IsConverted(string text);
    }
}
