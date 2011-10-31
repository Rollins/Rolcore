using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Rolcore.Web
{
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Overrrides any existing content with the content specified.
        /// </summary>
        /// <param name="response">Specifies the response to set the content for.</param>
        /// <param name="content">The content to set in the response.</param>
        /// <param name="contentType">The type of content being set</param>
        public static void WriteContent(this HttpResponse response, string content, string contentType)
        {
            // Pre-conditions
            if (response == null)
                throw new ArgumentNullException("response", "response is null.");
            if (content == null)
                throw new ArgumentNullException("content", "content is null (use empty string for empty response).");
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentException("contentType is null or empty.", "contentType");

            response.Clear();
            response.ContentType = contentType;
            response.Write(content);
            response.End();
        }
    }
}
