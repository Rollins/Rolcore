//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.IO
{
    using System.IO;

    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the specified string to a <see cref="TextReader"/>.
        /// </summary>
        /// <param name="s">Specifies the string to convert.</param>
        /// <returns>A <see cref="TextReader"/> that reads the specified string.</returns>
        public static TextReader ToTextReader(this string s)
        {
            return new StringReader(s);
        }
    }
}
