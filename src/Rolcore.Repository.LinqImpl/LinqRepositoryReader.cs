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
        }

        public IEnumerable<TBase> Items
        {
            get { return Table.Cast<TBase>(); }
        }
    }
}
