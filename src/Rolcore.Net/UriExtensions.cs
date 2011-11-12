using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;


namespace Rolcore.Net
{
    /// <summary>
    /// Extensions for <see cref="Uri"/>.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Does an HTTP GET on the given URL and returns the response recieved.
        /// </summary>
        /// <param name="url">The URL to perform the GET on.</param>
        /// <returns>An <see cref="HttpWebResponse"/> containing the response from the server.</returns>
        public static HttpWebResponse HttpGet(this Uri uri, string userAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727)", string accept = "*/*")
        {
            if (uri == null)
                throw new ArgumentNullException("uri", "uri is null.");

            //TODO: Ensure URL is for HTTP or HTTPS.

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri.ToString());
            request.Method = "GET";
            request.ProtocolVersion = HttpVersion.Version11;
            request.AllowAutoRedirect = false;
            request.Accept = accept;
            request.UserAgent = userAgent;
            request.Headers.Add("Accept-Language", "en-us");
            request.KeepAlive = true;
            // Get response for http web request
            return (HttpWebResponse)request.GetResponse();
        }


        /// <summary>
        /// Does an HTTP GET on the given URL and returns the response recieved as a string.
        /// </summary>
        /// <param name="url">The URL to perform the GET on.</param>
        /// <returns>A string containing the response from the server.</returns>
        public static string HttpGetText(this Uri uri)
        {
            return new System.IO.StreamReader(uri.HttpGet().GetResponseStream()).ReadToEnd();
        }
    }
}
