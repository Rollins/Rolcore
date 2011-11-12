using System;
using System.Text;

namespace Rolcore
{
    /// <summary>
    /// Extensions for <see cref="String"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks that the string ends with the specified character. If it doesn't then the 
        /// character is appended.
        /// </summary>
        /// <param name="s">Specifies the string to check.</param>
        /// <param name="c">Specifies the character to check for.</param>
        /// <returns>The string, with the trailing character.</returns>
        public static string EnsureTrailing(this string s, char c)
        {
            if (s == null)
                throw new ArgumentNullException("s", "s is null.");
            
            int length = s.Length;
            
            if(length == 0)
                return c.ToString();
            
            if(s[length-1] != c)
                return s + c;
            
            return s;
        }

        /// <summary>
        /// Returns the first part of a string, up to a specified number of characters.
        /// </summary>
        /// <param name="numberOfCharacters">The number of characters to get from the string</param>
        /// <param name="s">The string from which to get the sub string.</param>
        /// <returns></returns>
        public static string First(this string s, int numberOfCharacters)
        {
            if (numberOfCharacters < 0)
                throw new ArgumentOutOfRangeException("numberofCharacters");

            if (string.IsNullOrEmpty(s))
                return s;
            else if (s.Length > numberOfCharacters)
                return s.Substring(0, numberOfCharacters);

            return s;
        }

        /// <summary>
        /// Repeats a string some number of times.
        /// </summary>
        /// <param name="s">The string to be repeated.</param>
        /// <param name="numberOfTimes">The number of times to repeat the string. A value of zero
        /// returns an empty string, a value of one returns the original string, a value of two or
        /// more returns the string repeated that number of times.</param>
        /// <returns><see cref="string"/>.</returns>
        public static string Repeat(this string s, int numberOfTimes)
        {
            if (numberOfTimes < 0)
                throw new ArgumentOutOfRangeException("numberOfTimes", "numberOfTimes must be at least zero");
            if (numberOfTimes == 0)
                return string.Empty; // repeated zero times
            
            StringBuilder result = new StringBuilder(s);
            for (int i = 1; i < numberOfTimes; i++)
                result.Append(s);

            return result.ToString();
        }

        public static Uri ToUri(this string s, Uri baseUri = null)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentException("Cannot convert an null, empty, or whitespace string to a Uri.", "s");

            Uri result = (baseUri == null)
                ? new Uri(s, UriKind.RelativeOrAbsolute) //the constructor will error if it's passed a relative path but not specied as such
                : new Uri(baseUri, s);

            return result;
        }

        public static string UrlDecode(this string s)
        {
            return System.Web.HttpUtility.UrlDecode(s);
        }

        public static string UrlEncode(this string s)
        {
            return System.Web.HttpUtility.UrlEncode(s);
        }
    }
}
