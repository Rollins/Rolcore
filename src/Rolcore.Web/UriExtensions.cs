using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Diagnostics;

namespace Rolcore.Web
{
    public static class UriExtensions
    {
        public static NameValueCollection GetQueryString(this Uri uri)
        {
            if (!uri.OriginalString.Contains("?"))
                return new NameValueCollection();

            return HttpUtility.ParseQueryString(uri.Query);
        }

        public static string ParseKeyWords(this Uri uri)
        {
            NameValueCollection queryString = uri.GetQueryString();
            if (queryString.Count == 0)
                return null;

            switch (uri.GetDomainName().ToLower())
            {
                case "superpages.com":
                    return queryString["C"];
                case "switchboard.com":
                    return queryString["KW"];
                case "yahoo.com":
                    return queryString["p"] ?? queryString["desc"];
                case "pestanalysis.com":
                    return queryString["term"];
                case "entry-not-found.com":
                case "charter.net":
                case "verizon.net":
                    return queryString["qo"];
                case "yellowpages.com":
                    return queryString["search_terms"];
                case "mywebsearch.com":
                    return queryString["searchfor"];
                case "allhomepestcontrol.info":
                    return queryString["ss"];
                case "att.net":
                    return queryString["string"];
                case "doubleclick.net": // Nested
                    string nestedUrlEncoded = queryString["ref"];
                    try
                    {
                        return (string.IsNullOrWhiteSpace(nestedUrlEncoded))
                            ? null
                            : nestedUrlEncoded.UrlDecode().ToUri().ParseKeyWords();
                    }
                    catch (UriFormatException ex)
                    {
                        Trace.WriteLine(ex); // URI could not be parsed
                        return null;
                    }
                case "googlesyndication.com": // Nested
                    try
                    {
                        nestedUrlEncoded = queryString["url"];
                        return (string.IsNullOrWhiteSpace(nestedUrlEncoded))
                            ? null
                            : nestedUrlEncoded.UrlDecode().ToUri().ParseKeyWords();
                    }
                    catch (UriFormatException ex)
                    {
                        Trace.WriteLine(ex); // URI could not be parsed
                        return null;
                    }
                default:
                    return queryString["q"] // google.com, comcast.net, bing.com, ...?
                        ?? queryString["query"]; // aol.com, cnn.com, information.com, netscape.com, ...?
            }
        }
    }
}
