namespace Rolcore.Repository.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(IRepositoryWriter<,>))]
    public abstract class IRepositoryWriterContracts<TItem, TConcurrency> : IRepositoryWriter<TItem, TConcurrency>
        where TItem : class
    {
        void IRepositoryWriter<TItem, TConcurrency>.ApplyRules(params TItem[] items)
        {
            Contract.Requires<ArgumentNullException>(items != null, "items cannot be null");
            Contract.Requires<ArgumentException>(items.Length > 0, "items cannot be empty");
        }

        IEnumerable<TItem> IRepositoryWriter<TItem, TConcurrency>.Save(params TItem[] items)
        {
            Contract.Requires<ArgumentNullException>(items != null, "items cannot be null");
            Contract.Requires<ArgumentException>(items.Length > 0, "items cannot be empty");

            // For unknown reasons, the following line *sometimes* causes the Save() method to be called two extra times!
            //Contract.Ensures(Contract.Result<IEnumerable<TItem>>().Count() == items.Length, "All items do not appear to have been saved");

            return default(TItem[]);
        }

        int IRepositoryWriter<TItem, TConcurrency>.Delete(params TItem[] items)
        {
            Contract.Requires<ArgumentNullException>(items != null, "items cannot be null");
            Contract.Requires<ArgumentException>(items.Length > 0, "items cannot be empty");

            return default(int);
        }

        int IRepositoryWriter<TItem, TConcurrency>.Delete(string rowKey, TConcurrency concurrency, string partitionKey)
        {
            Contract.Requires<ArgumentNullException>(rowKey != null, "rowKey cannot be null");
            Contract.Requires<ArgumentNullException>(!concurrency.Equals(null), "concurrency cannot be null");

            return default(int);
        }

        public IEnumerable<TItem> Insert(params TItem[] items)
        {
            Contract.Requires<ArgumentNullException>(items != null, "items cannot be null");
            Contract.Requires<ArgumentException>(items.Length > 0, "items cannot be empty");

            return default(IEnumerable<TItem>);
        }

        public IEnumerable<TItem> Update(params TItem[] items)
        {
            Contract.Requires<ArgumentNullException>(items != null, "items cannot be null");
            Contract.Requires<ArgumentException>(items.Length > 0, "items cannot be empty");

            return default(IEnumerable<TItem>);
        }

        [ImportMany]
        public IEnumerable<IRepositoryItemRule<TItem>> Rules { get; set; }
    }
}
