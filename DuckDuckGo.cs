using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;

namespace DuckDuckGo
{
    /// <summary>
    /// Defines Loader Types
    /// </summary>
    public enum Loader
    {
        WebClient = 1,
        File = 2
    }

    /// <summary>
    /// Formatting Options
    /// </summary>
    public enum Format
    {
        Json = 1,
        Xml = 2
    }

    /// <summary>
    /// Icon
    /// </summary>
    public class Icon
    {
        public string Url { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
    }

    /// <summary>
    /// Individual result set for a query
    /// </summary>
    public class QueryResult
    {
        public string Result { get; set; }
        public Icon Icon { get; set; }
        public string FirstUrl { get; set; }
        public string Text { get; set; }
    }

    /// <summary>
    /// Overal results from query 
    /// </summary>
    public class ResultSet
    {
        public string Definition { get; set; }
        public string DefinitionSource { get; set; }
        public string Heading { get; set; }
        public string AbstractSource { get; set; }
        public string Image { get; set; }
        public List<QueryResult> RelatedTopics { get; set; }
        public string AbstractText { get; set; }
        public string Abstract { get; set; }
        public string AnswerType { get; set; }
        public string Type { get; set; }
        public string Redirect { get; set; }
        public string DefinitionUrl { get; set; }
        public string Answer { get; set; }
        public List<QueryResult> Results { get; set; }
        public string AbstractUrl { get; set; }
    }

    /// <summary>
    /// Search Gateway
    /// </summary>
    public class Search
    {
        private const string SearchUri = @"https://api.duckduckgo.com/?q={0}&format={2}&t={1}&no_redirect={3}&no_html={4}&skip_disambig={5}";

        public static ResultSet Query(string query, string userAgent, Loader? loader = Loader.WebClient, bool noRedirects = false, bool noHtml = false, bool skipDisambiguation = false)
        {
            return Parse(Fetch(query, userAgent, Format.Json, loader, noRedirects, noHtml, skipDisambiguation));
        }
        
        public static string Fetch(string query, string userAgent, Format format = Format.Json, Loader? loader = Loader.WebClient, bool noRedirects = false, bool noHtml = false, bool skipDisambiguation = false)
        {
            var client = new SearchClient();

            switch (loader)
            {
                case Loader.File:
                    return client.FromFile(BuildRequestPath(query));
                default:
                    return client.FromWebClient(BuildRequestUri(query, userAgent, format, noRedirects, noHtml, skipDisambiguation));
            }
        }

        private static string BuildRequestUri(string query, string userAgent, Format format, bool noRedirects, bool noHtml, bool skipDisambiguation)
        {
            return string.Format(
                    SearchUri,
                    HttpUtility.UrlEncode(query),
                    HttpUtility.UrlEncode(userAgent),
                    HttpUtility.UrlEncode(format.ToString().ToLower()),
                    noRedirects ? 1 : 0,
                    noHtml ? 1 : 0,
                    skipDisambiguation ? 1 : 0);
        }
        private static string BuildRequestPath(string query)
        {
            return string.Format(@"{0}\{1}.json", Environment.CurrentDirectory, query);
        }
        private static ResultSet Parse(string result)
        {
            return JsonConvert.DeserializeObject<ResultSet>(result);
        }

    }

}
