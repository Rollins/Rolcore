using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Linq.Expressions;

namespace Rolcore.Repository
{
    /// <summary>
    /// A generic implementation of <see cref="IRepository<>"/> that delegates operations to 
    /// separate reader and writer instances.
    /// </summary>
    /// <typeparam name="TItem">Specifies the type of item in the repository.</typeparam>
    /// <typeparam name="TConcurrency">Specifies the type used for optimistic concurrency.</typeparam>
    public class Repository<TItem, TConcurrency> : IRepository<TItem, TConcurrency>
        where TItem : class
    {
        protected readonly IRepositoryReader<TItem> _Reader;
        protected readonly IRepositoryWriter<TItem, TConcurrency> _Writer;

        /// <summary>
        /// Instantiates a new repository.
        /// </summary>
        /// <param name="reader">Specifies the source of data for read operations.</param>
        /// <param name="writer">Specifies the destination for write operations.</param>
        public Repository(IRepositoryReader<TItem> reader, IRepositoryWriter<TItem, TConcurrency> writer)
        {
            Contract.Requires<ArgumentNullException>(reader != null, "reader cannot be null");
            Contract.Requires<ArgumentNullException>(writer != null, "writer cannot be null");
            Contract.Ensures(_Reader != null, "reader cannot be null");
            Contract.Ensures(_Writer != null, "writer cannot be null");

            _Reader = reader;
            _Writer = writer;
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable"/> of all items available in the repository.
        /// </summary>
        public IEnumerable<TItem> Items
        {
            get { return _Reader.Items; }
        }

        /// <summary>
        /// Inserts or updates the specified items in the repository.
        /// </summary>
        /// <param name="items">Specifies the items to insert or update.</param>
        /// <returns>The updated items. Note that depending on the implementation, the result may 
        /// be copies of items passed in; the items therefore may not reflect changes caused by
        /// the backing repository (for example, an auto-generated key).</returns>
        public IEnumerable<TItem> Save(params TItem[] items)
        {
            return _Writer.Save(items);
        }

        /// <summary>
        /// Deletes the specified items in the repository and returns the number of items deleted.
        /// </summary>
        /// <param name="items">Specifies the items to delete.</param>
        /// <returns>The number of items deleted.</returns>
        public int Delete(params TItem[] items)
        {
            return _Writer.Delete(items);
        }

        /// <summary>
        /// Deletes the items specified by the given row key, concurrency, and (optional) partition 
        /// key values and returns the number of items deleted. Note that though the most common 
        /// use for this method is to delete a single item, this MAY result in multiple items being
        /// deleted if the partitionKey argument is not specified.
        /// </summary>
        /// <param name="rowKey">Specifies the row key (unique identifier) of the item to delete.</param>
        /// <param name="concurrency">Specifies the value to check for optimistic concurrency.</param>
        /// <param name="partitionKey">Specifies the partition on which the item exists; typically, 
        /// this argument is only used distributed repositories such as Azure's table service.</param>
        /// <returns>The number of items deleted.</returns>
        public int Delete(string rowKey, TConcurrency concurrency, string partitionKey = null)
        {
            return _Writer.Delete(rowKey, concurrency, partitionKey);
        }

        public IEnumerable<TItem> Insert(params TItem[] items)
        {
            return _Writer.Insert(items);
        }

        public IEnumerable<TItem> Update(params TItem[] items)
        {
            return _Writer.Update(items);
        }
    }
}
