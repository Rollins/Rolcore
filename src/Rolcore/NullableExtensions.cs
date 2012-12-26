//-----------------------------------------------------------------------
// <copyright file="NullableExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore
{
    using System;

    /// <summary>
    /// Extension methods for nullable types.
    /// </summary>
    public static class NullableExtensions
    {
        public static string NullableToString<T>(this Nullable<T> value, string format, string nullResult) where T : struct
        {
            return (value.HasValue) ? string.Format(format, value.Value) : nullResult;
        }
    }
}
