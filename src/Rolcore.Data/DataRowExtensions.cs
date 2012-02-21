using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using Rolcore.Reflection;

namespace Rolcore.Data
{
    /// <summary>
    /// Extension utility methods for <see cref="DataRow"/>.
    /// </summary>
    public static class DataRowExtensions
    {
        /// <summary>
        /// Copies data from one row to another.
        /// </summary>
        /// <param name="source">Specifies the source to copy from.</param>
        /// <param name="destination">Specifies the destination to copy to.</param>
        /// <param name="columnMap">Optionally provides column name mappings, e.g. from a column 
        /// called "ID" to a column called "MyID".</param>
        public static void CopyTo(this DataRow source, DataRow destination, NameValueCollection columnMap = null)
        {
            //
            // Pre-conditions

            if (source == null)
                throw new ArgumentNullException("source", "source is null.");
            if (destination == null)
                throw new ArgumentNullException("destination", "destination is null.");

            //
            // Copy

            columnMap = source.Table.GetColumnMapOrDefault(columnMap);

            foreach (string sourceColumnName in columnMap.AllKeys)
                destination[columnMap[sourceColumnName]] = source[sourceColumnName];

            //TODO: Unit test
        }

        /// <summary>
        /// Copies data from a <see cref="DataRow"/> to an <see cref="object"/>.
        /// </summary>
        /// <param name="source">The source row.</param>
        /// <param name="destination">The destination object.</param>
        /// <param name="ignoreBlankValues">When true, empty values in the source are ignored.</param>
        /// <param name="columnMap">A <see cref="NameValueCollection"/> for mapping the name of a 
        /// column in the source to the name of a property in the destination.</param>
        /// <returns>A <see cref="Dictionary<string, object>"/> of changed properties and their 
        /// original values from the destination object.</returns>
        public static Dictionary<string, object> CopyRowData(this DataRow source, object destination, bool ignoreBlankValues, NameValueCollection columnMap, params string[] ignoreColumnNames)
        {
            //
            // Pre-conditions

            if (source == null)
                throw new ArgumentNullException("source", "source is null.");
            if (destination == null)
                throw new ArgumentNullException("destination", "destination is null.");

            //
            // Map properties

            columnMap = source.Table.GetColumnMapOrDefault(columnMap);
            var destinationType = destination.GetType();
            System.Reflection.PropertyInfo[] destinationProperties = destinationType.GetProperties();
            var result = new Dictionary<string, object>(columnMap.Count);

            //
            // Copy

            foreach (string sourceColumnName in columnMap.AllKeys)
            {
                var propertyName = columnMap[sourceColumnName];
                var sourceValue = source[sourceColumnName] ?? string.Empty;
                if ((ignoreBlankValues) && (sourceValue.ToString() == string.Empty))
                    continue;
                if (destinationProperties.Count(prop => prop.Name == propertyName) == 1)
                {
                    result.Add(propertyName, sourceValue);
                    var destinationValue = destination.GetPropertyValue(propertyName);
                    if (!sourceValue.Equals(destinationValue) && !ignoreColumnNames.Contains(sourceColumnName))
                        destination.SetPropertyValue(propertyName, sourceValue);
                }
            }

            return result;

            //TODO: Unit test
        }
    }
}
