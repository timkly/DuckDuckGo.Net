using System;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace DuckDuckGo.Net
{
    /// <summary>
    /// Formatting Options
    /// </summary>
    public enum ResponseFormat
    {
        Json = 1,
        Xml = 2
    }

    /// <summary>
    /// Search Gateway
    /// </summary>
    public class Search
    {
        /// <summary>
        /// The DuckDuckGo Instant Answer API URI
        /// </summary>
        public string Uri
        {
            get { return (IsSecure ? "https:" : "http:") + @"//api.duckduckgo.com/?q={0}&format={2}&t={1}&no_redirect={3}&no_html={4}&skip_disambig={5}"; }
        }

        /// <summary>
        /// Should the request use the HTTPS protocol rather then HTTP
        /// </summary>
        public bool IsSecure { get; set; }

        /// <summary>
        /// Should HTML syntax be removed from the text returned
        /// </summary>
        public bool NoHtml { get; set; }

        /// <summary>
        /// Should redirects be ingored for !Bang commands
        /// </summary>
        public bool NoRedirects { get; set; }

        /// <summary>
        /// Should the disambiguation text not be returned in the query
        /// </summary>
        public bool SkipDisambiguation { get; set; }

        /// <summary>
        /// Gets or sets the ApiClient to be used in the request
        /// </summary>
        private IApiClient _apiClient;
        public IApiClient ApiClient
        {
            get
            {
                return _apiClient ?? new SimpleApiClient();
            } 
            set { _apiClient = value; }
        }


        /// <summary>
        /// Perform a query and return the result as a 'SearchResult' object
        /// </summary>
        /// <param name="searchTerm">The term to be queried for</param>
        /// <param name="applicationName">The application name identifying the requesting entity</param>
        /// <param name="apiClient">The class implementation of the IApiClient interface which fetches and returns the response body for a given URI</param>
        /// <returns>Populated SearchResult object when results are found</returns>
        public SearchResult Query(string searchTerm, string applicationName)
        {
            return ParseJson(TextQuery(searchTerm, applicationName, ResponseFormat.Json));
        }

        /// <summary>
        /// Perform a query and return the result as a string in either JSON (Default) or XML format
        /// </summary>
        /// <param name="searchTerm">The term to be queried for</param>
        /// <param name="applicationName">The application name identifying the requesting entity</param>
        /// <param name="apiClient">The class implementation of the IApiClient interface which fetches and returns the response body for a given URI</param>
        /// <param name="responseFormat">The format the response should be structured as. Either JSON (Default) or XML</param>
        /// <returns>The result as a string formatted as either JSON (Default) or XML</returns>
        public string TextQuery(string searchTerm, string applicationName, ResponseFormat responseFormat)
        {
            if (string.IsNullOrEmpty(searchTerm)) throw new ArgumentNullException("searchTerm");
            if (string.IsNullOrEmpty(applicationName)) throw new ArgumentNullException("applicationName");

            
            return ApiClient.Load(BuildRequestUri(searchTerm, applicationName, responseFormat));
        }

        /// <summary>
        /// Parses a JSON string and returns a SearchResult object
        /// </summary>
        /// <param name="json">The JSON string to be parsed</param>
        /// <returns>Populated SearchResult object when results are found</returns>
        private static SearchResult ParseJson(string json)
        {
            return JsonConvert.DeserializeObject<SearchResult>(json);
        }

        /// <summary>
        /// Build the Uri for the request
        /// </summary>
        /// <param name="searchTerm">The term to be queried for</param>
        /// <param name="applicationName">The application name identifying the requesting entity</param>
        /// <param name="responseFormat">The format the response should be structured as. Either JSON (Default) or XML</param>
        /// <returns>The result as a string formatted as either JSON (Default) or XML</returns>
        private string BuildRequestUri(string searchTerm, string applicationName, ResponseFormat responseFormat)
        {
            return string.Format(
                    Uri,
                    HttpUtility.UrlEncode(searchTerm),
                    HttpUtility.UrlEncode(applicationName),
                    HttpUtility.UrlEncode(responseFormat.ToString().ToLower()),
                    NoRedirects ? 1 : 0,
                    NoHtml ? 1 : 0,
                    SkipDisambiguation ? 1 : 0);
        }

        /// <summary>
        ///  Default IApiClient implementation for the project   
        /// </summary>
        public class SimpleApiClient : IApiClient
        {
            /// <summary>
            /// Downloads and returns the response body from the nominated uri.
            /// </summary>
            /// <param name="uri">The URI for the resource requested</param>
            /// <returns>A string containing the response body</returns>
            public string Load(string uri)
            {
                using (var wc = new WebClient())
                {
                    return wc.DownloadString(uri);
                }
            }
        }
    }
}
