using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Diagnostics.Contracts;

namespace Rolcore.Repository.LinqImpl
{
    /// <summary>
    /// A LINQ implementation for <see cref="IRepository{,}"/>.
    /// </summary>
    /// <typeparam name="TItem">The LINQ-specific type stored in the repository.</typeparam>
    /// <typeparam name="TBase">The base class (typically a POCO) for the type of object stored in 
    /// the repository.</typeparam>
    /// <typeparam name="TConcurrency">The type of value used for concurrency.</typeparam>
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
