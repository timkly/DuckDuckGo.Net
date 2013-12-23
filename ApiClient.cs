using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace DuckDuckGo.Net
{
    /// <summary>
    /// IApiClient Interface
    /// </summary>
    public interface IApiClient
    {
        string Load(string uri);
    }


    /// <summary>
    /// IApiClient implementation using the WebRequest/WebResponse object
    /// </summary>
    public class HttpWebApi : IApiClient
    {
        /// <summary>
        /// Makes web request to server and returns response body (Sample implementation)
        /// </summary>
        /// <param name="uri">The URI to be parsed for query term extraction</param>
        /// <returns>A string containing the text contained with the file</returns>
        public string Load(string uri)
        {
            var request = WebRequest.Create(uri) as HttpWebRequest;
            if (request != null)
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (request.HaveResponse && response != null)
                    {
                        if (response.StatusCode.Equals(HttpStatusCode.OK))
                        {
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                return reader.ReadToEnd();
                            }
                        }
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode,
                                                          response.StatusDescription));
                    }
                }
            }
            return string.Empty;
        }
    }


    /// <summary>
    /// IApiClient implementation using the FileSystem object
    /// </summary>
    public class FileApi : IApiClient
    {
        /// <summary>
        /// Opens, reads and returns the text contained within a file (Sample implementation)
        /// </summary>
        /// <param name="uri">The URI to be parsed for query term extraction</param>
        /// <returns>A string containing the text contained with the file</returns>
        public string Load(string uri)
        {
            //Match the query from the search URI
            var query = Regex.Match(uri, @"q=([^&#]+)", RegexOptions.IgnoreCase);
            //Extract the query
            var searchTerm = query.Success ? query.Groups[1].Value : string.Empty;
            //Open in bin directory
            return File.ReadAllText(string.Format(@"{0}\{1}", Environment.CurrentDirectory, searchTerm));
        }
    }
}
