using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Diagnostics.Contracts;
using Rolcore.Diagnostics;

namespace Rolcore.Repository.LinqImpl
{
    public class LinqRepositoryBase<TItem, TBase>
        where TBase : class
        where TItem : class, TBase
    {
        protected Table<TItem> Table { get; private set; }

        public LinqRepositoryBase(Table<TItem> table)
        {
            Contract.Requires<ArgumentNullException>(table != null, "table cannot be null");
            Contract.Requires<ArgumentException>(table.Context != null, "table.Context cannot be null");
            Contract.Ensures(table == Table, "table cannot be null");

            Table = table;

#if(DEBUG)
            if(Table.Context.Log == null)
                Table.Context.Log = new DebuggerWriter();
#endif
        }
    }
}
