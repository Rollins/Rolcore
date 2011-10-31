using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Rolcore.Text.RegularExpressions;
using System.Diagnostics.Contracts;

namespace Rolcore.Web
{
    public static class StringExtensions
    {

        /// <summary>
        /// Returns true if the string contains HTML.
        /// </summary>
        /// <param name="s">The string to check for HTML.</param>
        /// <returns>True if the string contains HTML.</returns>
        public static bool ContainsHtml(this string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (s.Length == 0)
                return false;

            Regex htmlTagMatcher = CommonExpressions.HtmlTag.ToRegEx(RegexOptions.IgnoreCase);
            Match htmlTagMatch = htmlTagMatcher.Match(s);
            return htmlTagMatch.Success;
        }

        public static string RemoveHtml(this string s)
        {
            Contract.Requires<ArgumentNullException>(s != null, "s is null.");

            if (s.Length == 0)
                return s;

            Regex htmlTagMatcher = CommonExpressions.HtmlTag.ToRegEx(RegexOptions.IgnoreCase);

            return htmlTagMatcher.Replace(s, String.Empty);
        }

        public static string ReplaceVariables(this string s, string variableFormat, System.Collections.Specialized.NameValueCollection data)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder(s);
            
            data.AllKeys.ToList().ForEach(key =>
                result.Replace(String.Format(variableFormat, key), data[key])
            );

            //make sure there aren't any variables that weren't caught
            //and if there were, replace them w/ empty strings


            //the {0} in the formt variable will be escaped by Regex.Escape()
            //(and we don't want that) so replace it w/ something we know won't be escaped
            string regExPattern = String.Format(variableFormat, "~~~");
            regExPattern = Regex.Escape(regExPattern);
            regExPattern = regExPattern.Replace("~~~", ".*");

            return Regex.Replace(
                result.ToString(), 
                regExPattern, 
                String.Empty, 
                RegexOptions.IgnoreCase);
        }
    }
}
