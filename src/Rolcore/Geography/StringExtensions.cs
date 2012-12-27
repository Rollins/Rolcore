//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Geography
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Verifies that the input is a valid U.S. postal code.
        /// </summary>
        /// <param name="s">The postal code to validate.</param>
        /// <returns>True if the string is a postal code.</returns>
        public static bool IsInUsaPostalCodeFormat(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }

            Regex zipFinder = CreateUsa5DigitPostalCodeMatcher();
            return zipFinder.Match(s).Success;
        }

        /// <summary>
        /// Creates a regular expression that recognizes a 5-digit postal code.
        /// </summary>
        /// <returns>A <see cref="Regex"/> that will match a 5-digit postal code.</returns>
        private static Regex CreateUsa5DigitPostalCodeMatcher()
        {
            return new Regex(@"(^\d{5}$)|(^\d{5}-\d{4}$)");
        }
    }
}
