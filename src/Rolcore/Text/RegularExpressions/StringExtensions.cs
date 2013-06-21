//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
using System.Diagnostics.Contracts;
namespace Rolcore.Text.RegularExpressions
{
    using System;
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
            Contract.Requires<ArgumentNullException>(s != null, "s is null.");
            return new Regex(s, options);
        }

        public static string Between(this string s, string start, string end)
        {
            Contract.Requires<ArgumentNullException>(s != null, "s is null.");
            Contract.Requires<ArgumentNullException>(start != null, "start is null.");
            Contract.Requires<ArgumentNullException>(end != null, "end is null.");

            var startIndex = s.IndexOf(start) + start.Length;
            var endIndex = startIndex + s.Substring(startIndex).IndexOf(end);
            var result = s.Substring(startIndex, endIndex - startIndex); // Write matching lines from Source to output

            return result;
        }
    }
}
