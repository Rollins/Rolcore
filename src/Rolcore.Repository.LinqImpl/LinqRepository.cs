using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Diagnostics.Contracts;

namespace Rolcore.Repository.LinqImpl
{
    public class LinqRepository<TItem, TBase, TConcurrency> : Repository<TBase, TConcurrency>
        where TBase : class
        where TItem : class, TBase
    {
        public LinqRepository(
            Table<TItem> table, 
            Action<TItem, string, TConcurrency, string> setKeyAndConcurrencyValues, 
            Func<TBase, bool> itemExists)
            : base(
                new LinqRepositoryReader<TItem, TBase>(table), 
                new LinqRepositoryWriter<TItem, TBase, TConcurrency>(table, setKeyAndConcurrencyValues, itemExists))
        {
            Contract.Requires<ArgumentNullException>(setKeyAndConcurrencyValues != null, "setKeyAndConcurrencyValues is null");
            Contract.Requires<ArgumentNullException>(itemExists != null, "itemExists is null");
        } // Tested
    }
}
