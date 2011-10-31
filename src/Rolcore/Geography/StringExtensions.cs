using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Rolcore.Geography
{
    public static class StringExtensions
    {
        private static Regex CreateUsa5DigitPostalCodeMatcher()
        {
            return new Regex(@"(^\d{5}$)|(^\d{5}-\d{4}$)");
        }

        /// <summary>
        /// Verifies that the input is a valid U.S. postal code.
        /// </summary>
        /// <param name="s">The postal code to validate.</param>
        /// <returns>True if the string is a postal code.</returns>
        public static bool IsInUsaPostalCodeFormat(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            Regex zipFinder = CreateUsa5DigitPostalCodeMatcher();
            return zipFinder.Match(s).Success;
        }
    }
}
