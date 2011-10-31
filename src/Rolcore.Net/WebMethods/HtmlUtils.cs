using System;
using System.Text;
using System.Text.RegularExpressions;
using Rolcore.Text.RegularExpressions;

namespace Rolcore.Net.WebMethods
{
    /// <summary>
    /// Class containing static methods for parsing/manipulating HTML.
    /// </summary>
    public static class HtmlUtils
    {

        /// <summary>
        /// Adds quotes to all un-quoted HTML attributes. Assumes HTML passed is otherwise valid.
        /// </summary>
        /// <param name="html">The HTML to add quotes to.</param>
        /// <returns>The modified HTML.</returns>
        public static string QuoteAllHtmlTagAttributes(string html)
        {
            if (string.IsNullOrEmpty(html))
                throw new ArgumentException("html cannot be null or empty", "html");

            StringBuilder result = new StringBuilder(html);

            Regex htmlTagMatcher = CommonExpressions.HtmlTag.ToRegEx(RegexOptions.IgnoreCase);
            Match htmlTagMatch = htmlTagMatcher.Match(html);
            while (htmlTagMatch.Success)
            {
                string[] tagParts = htmlTagMatch.Value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                tagParts[0] += " ";
                for (int i = 1; i < tagParts.Length; i++)
                {
                    if (tagParts[i].Contains("=\"") || tagParts[i].Contains("='"))
                    {
                        tagParts[i] += " "; //Re-add space removed during Split().
                        continue; // Already quoted
                    }
                    // Quote unquoted attributes
                    tagParts[i] = tagParts[i].Replace("=", "=\"");
                    if (tagParts[i].Contains(">"))
                        tagParts[i] = tagParts[i].Replace(">", "\">");
                    else
                        tagParts[i] += "\" ";
                }
                result.Replace(htmlTagMatch.Value, string.Concat(tagParts));
                htmlTagMatch = htmlTagMatcher.Match(html, htmlTagMatch.Index + htmlTagMatch.Length);
            }

            return result.ToString();
        }
    }
}
