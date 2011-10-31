using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Rolcore.Web.UI.WebControls
{
    public static class LinqDataSourceExtensionMethods
    {
        /// <summary>
        /// Creates a new instance of <see cref="LinqDataSource"/> with the same properties as the
        /// given <see cref="LinqDataSource"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static LinqDataSource Clone(this LinqDataSource source) //TODO: Unit Test
        {
            // Pre-conditions
            if (source == null)
                throw new ArgumentNullException("source", "source is null");

            LinqDataSource result = new LinqDataSource
            {
                ContextTypeName = source.ContextTypeName,
                TableName = source.TableName,
                EnableDelete = source.EnableDelete,
                EnableInsert = source.EnableInsert,
                EnableUpdate = source.EnableUpdate,
                Where = source.Where
            };

            foreach (Parameter parameter in source.WhereParameters)
                result.WhereParameters.Add(parameter);

            return result;
        }
    }
}
