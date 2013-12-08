using System.IO;
using System.Net;

namespace DuckDuckGo
{
    /// <summary>
    /// Search Providers - Add new providers depending on client you wish to call
    /// </summary>
    internal class SearchClient
    {
        public string FromWebClient(string uri)
        {
            using (var wc = new WebClient())
            {
                return wc.DownloadString(uri);
            }
        }
        public string FromFile(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
