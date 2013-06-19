//-----------------------------------------------------------------------
// <copyright file="IEnumerableExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Text;

    /// <summary>
    /// Extension methods for <see cref="IEnumerable{}"/>
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Combines the specified strings.
        /// </summary>
        /// <param name="strings">The strings to combine.</param>
        /// <param name="prepend">The value to prepend to the combined value.</param>
        /// <param name="append">The value to append to the combined value.</param>
        /// <param name="itemPrepend">The value to prepend before each item in the combined value.</param>
        /// <param name="itemAppend">The value to append after each item in the combined value.</param>
        /// <returns>A string with all the given elements, prepended and appended according to the
        /// values specified.</returns>
        public static string Combine(this IEnumerable<string> strings, string prepend, string append, string itemPrepend, string itemAppend)
        {
            Contract.Requires<ArgumentNullException>(strings != null, "strings is null.");
            Contract.Ensures(Contract.Result<string>() != null, "result is null");
            
            var result = new StringBuilder(prepend ?? string.Empty);
            foreach (var s in strings)
            {
                result
                    .Append(itemPrepend ?? string.Empty)
                    .Append(s)
                    .Append(itemAppend ?? string.Empty);
            }

            return result.Append(append ?? string.Empty).ToString();
        } // Tested
    }
}
