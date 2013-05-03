using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Diagnostics.Contracts;
using System.Collections;

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

        public IQueryable<TBase> Items
        {
            get { return Table.Cast<TBase>(); }
        }
    }
}
