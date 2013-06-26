using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Diagnostics.Contracts;

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
        public static LinqDataSource Clone(this LinqDataSource source)
        {
            Contract.Requires<ArgumentNullException>(source != null, "source is null.");
            
            var result = new LinqDataSource
            {
                ContextTypeName = source.ContextTypeName,
                TableName = source.TableName,
                EnableDelete = source.EnableDelete,
                EnableInsert = source.EnableInsert,
                EnableUpdate = source.EnableUpdate,
                Where = source.Where
            };

            foreach (Parameter parameter in source.WhereParameters)
            {
                result.WhereParameters.Add(parameter);
            }

            return result;
        } // TODO: Unit Test
    }
}
