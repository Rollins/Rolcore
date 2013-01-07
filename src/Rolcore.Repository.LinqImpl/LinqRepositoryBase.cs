namespace Rolcore.Repository.LinqImpl
{
    using System;
    using System.Data.Linq;
    using System.Diagnostics.Contracts;

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

            this.Table = table;

            #if(DEBUG)
            if(this.Table.Context.Log == null)
                this.Table.Context.Log = new DebuggerWriter();
            #endif
        }
    }
}
