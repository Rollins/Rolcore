//-----------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    /// <summary>
    /// Extension methods for <see cref="Dictionary"/>.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="ictionary<string, string>"/> to a 
        /// <see cref="NameValueCollection"/>.
        /// </summary>
        /// <param name="value">Specifies the value to convert.</param>
        /// <returns></returns>
        public static NameValueCollection ToNameValueCollection(this Dictionary<string, string> value)
        {
            if (value == null)
                throw new ArgumentNullException("value", "value is null.");

            return value.ToArray().ToNameValueCollection();
        }
    }
}
