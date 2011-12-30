using System;

using System.Web;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Rolcore
{
    /// <summary>
    /// Extensions for <see cref="Uri"/>.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Extracts the base URL from a <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">Specifies the URI from which to extract the base URL.</param>
        /// <returns>A base url, for example http://www.foobar.com:8080</returns>
        public static Uri GetBaseUri(this Uri uri)
        {   
            return new Uri(string.Format("{0}{1}{2}", uri.Scheme, Uri.SchemeDelimiter, uri.Authority));
        }

        /// <summary>
        /// Extracts the sub-domain from a URI.
        /// </summary>
        /// <param name="url">The URI from which to extract the sub-domain.</param>
        /// <returns>A sub-domain, for example "www".</returns>
        public static string GetSubDomain(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri", "uri is null.");
            
            string result = uri.GetBaseUri().ToString().Replace(string.Format("{0}://", uri.Scheme), string.Empty);
            if (string.IsNullOrEmpty(result))
                return string.Empty;


            if (result.Contains("."))
                result = result.Substring(0, result.LastIndexOf('.')); // truncate extension (e.g. ".com");

            if (result.Contains("."))
                result = result.Substring(0, result.LastIndexOf('.')); // truncate primary domain
            else
                result = string.Empty; // No sub domain

            return result;
        }

        /// <summary>
        /// Extracts the domain name from a URI.
        /// </summary>
        /// <param name="uri">Specifies the URI from which to extract the domain name.</param>
        /// <returns>A domain, for example "mysite.com" in www.mysite.com.</returns>
        public static string GetDomainName(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri", "uri is null.");
            
            string result = uri.Host;
            if (string.IsNullOrEmpty(result))
                return string.Empty;

            if (!result.Contains("."))
                return result;

            string[] parts = result.Split('.');

            int lastIndex = parts.Length - 1;
            int nextToLastIndex = lastIndex - 1;

            result = string.Format("{0}.{1}", parts[nextToLastIndex], parts[lastIndex]);

            return result;
        }
    }
}
