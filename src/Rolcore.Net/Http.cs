using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;

namespace Rolcore.Net
{
    public static class Http
    {
        public static byte[] Post(string uri, NameValueCollection data)
        {
            byte[] response = null;
            using (WebClient client = new WebClient())
            {
                response = client.UploadValues(uri, data);
            }

            return response;
        } //TODO: Unit test

        public static HttpWebResponse Get(string uri, string userAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727)", string accept = "*/*")
        {
            if (uri == null)
                throw new ArgumentNullException("uri", "uri is null.");

            //TODO: Ensure URL is for HTTP or HTTPS.

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";

            request.ProtocolVersion = HttpVersion.Version11;
            request.AllowAutoRedirect = false;
            request.Accept = accept;
            request.UserAgent = userAgent;
            request.Headers.Add("Accept-Language", "en-us");
            request.KeepAlive = true;
            // Get response for http web request
            return (HttpWebResponse)request.GetResponse();
        } //TODO: Unit test
    }
}
