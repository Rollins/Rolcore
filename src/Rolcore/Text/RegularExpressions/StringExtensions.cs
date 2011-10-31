using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Rolcore.Text.RegularExpressions
{
    public static class StringExtensions
    {
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
