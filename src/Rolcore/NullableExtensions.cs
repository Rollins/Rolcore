//-----------------------------------------------------------------------
// <copyright file="NullableExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Extension methods for <see cref="Nullable{}"/> types.
    /// </summary>
    public static class NullableExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="Nullable{struct}"/> instance to a string and allows an optional 
        /// default to be specified.
        /// </summary>
        /// <typeparam name="T">Specifies the type of <see cref="Nullable{struct}"/> object.</typeparam>
        /// <param name="value">Specifies the <see cref="Nullable{struct}"/> instance to convert.</param>
        /// <param name="format">Specifies the format string.</param>
        /// <param name="nullResult">Specifies the default result to return when the 
        /// <see cref="Nullable{struct}"/> instance is null (default is null).</param>
        /// <returns>A string representing the <see cref="Nullable{struct}"/> instance.</returns>
        public static string NullableToString<T>(this T? value, string format, string nullResult = null) 
            where T : struct
        {
            Contract.Requires<InvalidOperationException>(!value.HasValue || format != null, "format not supplied for non null value");
            return value.HasValue 
                ? string.Format(format, value.Value) 
                : nullResult;
        } // TODO: Test
    }
}
