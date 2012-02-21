using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;

namespace Rolcore.Data
{
    public static class DataRowExtensions
    {
        /// <summary>
        /// Copies data from one row to another.
        /// </summary>
        /// <param name="source">Specifies the source to copy from.</param>
        /// <param name="destination">Specifies the destination to copy to.</param>
        /// <param name="columnMap">Optionally provides column name mappings, e.g. from a column 
        /// called "ID" to a column called "MyID".</param>
        public static void CopyRowData(this DataRow source, DataRow destination, NameValueCollection columnMap = null)
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
    }
}
