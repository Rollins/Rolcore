//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Text.RegularExpressions
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the string to a <see cref="RegEx"/>.
        /// </summary>
        /// <param name="s">Specifies the string to convert.</param>
        /// <param name="options">A bitwise combination of the enumeration values that modify the 
        /// regular expression.</param>
        /// <returns>A regular expression</returns>
        public static Regex ToRegEx(this string s, RegexOptions options)
        {
            return new Regex(s, options);
        }

        public static string Between(this string s, string start, string end)
        {
            string result = "";

            int startIndex = s.IndexOf(start) + start.Length;
            int endIndex = startIndex + s.Substring(startIndex).IndexOf(end);
            result = s.Substring(startIndex, endIndex - startIndex); // Write matching lines from Source to output

            return result;
        }
    }
}
