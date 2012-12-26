//-----------------------------------------------------------------------
// <copyright file="NameValueCollectionExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Collections.Specialized
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;

    /// <summary>
    /// Provides extension methods related to <see cref="NameValueCollection"/>.
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// Creates a copy of the current instance that is read-only.
        /// </summary>
        /// <param name="value">Specifies the collection to convert to read-only.</param>
        /// <returns>A read-only instance.</returns>
        public static NameValueCollection ToReadOnly(this NameValueCollection value)
        {
            if (value == null)
                throw new ArgumentNullException("value", "value is null.");
            
            NameValueCollectionEx result = new NameValueCollectionEx();
            result.Add(value);
            return result.ToReadOnly();
        }

        /// <summary>
        /// Creates a <see cref="NameValueCollection"/> from the specified string and seperators.
        /// </summary>
        /// <param name="value">Specifies the string that is to be converted to a 
        /// <see cref="NameValueCollection"/>.</param>
        /// <param name="listItemSeperator">Specifies the character that seperates name-value 
        /// entries.</param>
        /// <param name="keyValueSeperator">Specifies the character that seperates the name from 
        /// the value in each name-value pair.</param>
        /// <returns>A <see cref="NameValueCollection"/> representation of the specified string.</returns>
        public static NameValueCollection ToNameValueCollection(this string value, char listItemSeperator, char keyValueSeperator)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("value is null or empty.", "value");
            
            NameValueCollection result = new NameValueCollection();
            string[] nameValuePairs = value.Split(new char[] { listItemSeperator });
            for (int i = 0; i < nameValuePairs.Length; i++)
            {
                string[] nameValue = nameValuePairs[i].Split(new char[] { keyValueSeperator });
                result.Add(nameValue[0].Trim(), nameValue[1].Trim());
            }
            return result;
        }

        /// <summary>
        /// Determines if the <see cref="NameValueCollection"/>s contain exactly the same keys and
        /// values.
        /// </summary>
        /// <param name="value">The first <see cref="NameValueCollection"/> to compare.</param>
        /// <param name="compare">The other <see cref="NameValueCollection"/> to compare.</param>
        /// <returns>True if the <see cref="NameValueCollection"/>s are equivelant.</returns>
        public static bool IsEquivelantTo(this NameValueCollection value, NameValueCollection compare)
        {   //TODO: Unit test
            if(value == compare)
                return true;

            if ((value == null && compare != null) || (value != null && compare == null))
                return false;

            if(value.Count != compare.Count)
                return false;

            foreach (string key in value.AllKeys)
            {
                string keyValue = value[key];
                string compareValue = compare[key];
                if(keyValue != compareValue)
                    return false;
            }

            return true;
        }

        //TODO: Document
        public static NameValueCollection FromKeys(this NameValueCollection collection, params string[] keys)
        {
            if (collection == null)
                throw new ArgumentNullException("collection", "collection is null.");
            if(keys == null || keys.Length == 0)
                throw new ArgumentException("keys is null or empty", "keys");
            
            NameValueCollection result = new NameValueCollection(collection.Count);
            foreach (string key in collection)
            {
                if (keys.Contains(key))
                {
                    string value = collection[key];
                    result.Add(key, value);
                }
            }

            return result;
        }
    }
}
