using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Rolcore.Web.UI
{
    /// <summary>
    /// A static class containing utilities for working with HTML.
    /// </summary>
    public static class HtmlUtils
    {
        /// <summary>
        /// A standard, "blank" HTML document.
        /// </summary>
        public const string EmptyHtmlDoc = "<html>\n<head>\n\t<title></title>\n</head>\n<body>\n\n</body>\n</html>";

        /// <summary>
        /// Creates a <see cref="Regex"/> that matches the specified attribute and it's value.
        /// </summary>
        /// <param name="attributeName">Specifies the name of the attribute to match.</param>
        /// <returns>A regular expression object.</returns>
        public static Regex CreateAttributeRegEx(string attributeName)
        {
            if (string.IsNullOrEmpty(attributeName))
                throw new ArgumentException("attributeName is null or empty.", "attributeName");
            
            //TODO: Validate attributeName fomat

            // "attributeName" plus zero or more spaces plus "=" plus zero or more 
            // spaces plus "'" or '"' or nothing plus one or more of any character other than "'", 
            // '"', or ">" plus "'" or '"' or nothing.
            var result = new Regex(" " + attributeName + @" *= *['""]?([^'"">]+)['""]?");

            return result;
        } // Tested
    }
}
