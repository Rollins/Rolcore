namespace Rolcore.Repository.ListImpl
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    public class ListRepository<TItem, TConcurrency> : Repository<TItem, TConcurrency>
        where TItem : class
    {
        public ListRepository(
            IList<TItem> list,
            Action<TItem, TConcurrency> setConcurrency,
            Func<TItem, IList<TItem>, TItem> findByItemIdent,
            Func<TItem, IList<TItem>, TItem> findConcurrentlyByItem,
            Func<string, TConcurrency, string, IList<TItem>, TItem> findConcurrently, 
            Func<TConcurrency> newConcurrencyValue,
            bool safeCopy)
            : base(
                new ListRepositoryReader<TItem>(list, false),
                new ListRepositoryWriter<TItem, TConcurrency>(
                    list, setConcurrency, findByItemIdent, findConcurrentlyByItem, findConcurrently, newConcurrencyValue, safeCopy))
        {
            Contract.Requires<ArgumentNullException>(list != null, "list is null");
            Contract.Requires<ArgumentNullException>(setConcurrency != null, "setConcurrency is null");

            // Ensure the reader and writer are both accessing the same repository
            ((ListRepositoryReader<TItem>)base._Reader).List = ((ListRepositoryWriter<TItem, TConcurrency>)base._Writer).List;

        } // Tested
    }
}
