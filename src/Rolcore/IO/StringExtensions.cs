//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
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
        public static TextReader ToTextReader(this string s)
        {
            return new StringReader(s);
        }
    }
}
