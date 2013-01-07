using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Diagnostics.Contracts;

namespace Rolcore.Repository.LinqImpl
{
    public class LinqRepositoryReader<TItem, TBase> 
        : LinqRepositoryBase<TItem, TBase>,
          IRepositoryReader<TBase>
        where TBase : class
        where TItem : class, TBase
    {
        public LinqRepositoryReader(Table<TItem> table) 
            : base(table)
        {
            Contract.Requires<ArgumentNullException>(table != null, "table is null");
            Contract.Requires<ArgumentNullException>(table.Context != null, "table is null");
        }

        public IEnumerable<TBase> Items
        {
            get { return Table.Cast<TBase>(); }
        }
    }
}
