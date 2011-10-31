using System;
using System.Collections.Specialized;
using System.Net;
using System.Text.RegularExpressions;
using Rolcore.Collections.Specialized;

namespace Rolcore.Web
{
    /// <summary>
    /// Provides utility functions for HTTP.
    /// </summary>
    public static class HttpUtils
    {

        /// <summary>
        /// Takes a QueryString a rewrites it according to the supplied replacement rules.
        /// </summary>
        /// <param name="queryString">Name=Value collection in the form of a string.</param>
        /// <param name="replacementRules">Replacement rules used to rewrite the queryString.</param>
        /// <returns>The queryString after all replacement rules have been applied.</returns>
        public static string ReplaceQSValues(string queryString, string replacementRules)
        {
            return ReplaceQSValues(queryString, replacementRules, '&');
        }

        /// <summary>
        /// Takes a QueryString a rewrites it according to the supplied replacement rules.
        /// </summary>
        /// <param name="queryString">Name=Value collection in the form of a string.</param>
        /// <param name="replacementRules">Replacement rules used to rewrite the queryString.</param>
        /// <param name="ruleSeperator">Character used to delimit name/value pairs within the string.</param>
        /// <returns>The queryString after all replacement rules have been applied.</returns>
        public static string ReplaceQSValues(string queryString, string replacementRules, char ruleSeperator)
        {
            if (replacementRules.Trim().Length == 0)
                return queryString;

            string result = queryString;
            NameValueCollection rules = replacementRules.ToNameValueCollection(ruleSeperator, '=');

            foreach (string Name in rules.AllKeys)
            {
                Match regExPair = Regex.Match(queryString, String.Format(@"(?<name>{0})=(?<value>\w+)", Name));

                if (regExPair.Success)
                {
                    string ruleValue = rules[Name];
                    string originalValue = regExPair.Result("${value}");
                    Match regExValueFormat = Regex.Match(ruleValue, @"(?<pre>.*){(?<param>\w+)}(?<post>.*)", RegexOptions.IgnoreCase);

                    if (regExValueFormat.Success)
                    {//if the value contains curly braces then its a reference to a QS variable

                        Match regExValue = Regex.Match(queryString,
                            String.Format(@"{0}=(?<value>[\w]+)",
                            regExValueFormat.Result("${param}")));

                        if (regExValue.Success)
                        {
                            ruleValue = String.Format("{0}{2}{1}"
                                , regExValueFormat.Result("${pre}")
                                , regExValueFormat.Result("${post}")
                                , regExValue.Result("${value}"));
                        }
                        else
                        {//variable doesn't exist in the QS, so just use the original value
                            ruleValue = originalValue;
                        }
                    }

                    result = result.Replace(originalValue, ruleValue);
                }
            }

            return result;
        }
    }
}
