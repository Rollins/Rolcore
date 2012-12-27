using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Rolcore.Reflection;
using System.Threading;
using Rolcore;

namespace Rolcore.Repository.ListImpl
{
    public class ListRepositoryWriter<TItem, TConcurrency> : ListRepositoryBase<TItem>, IRepositoryWriter<TItem, TConcurrency>
        where TItem : class
    {
        private readonly Action<TItem, TConcurrency> _SetConcurrency;
        private readonly Func<TItem, IList<TItem>, TItem> _FindByItemIdent;
        private readonly Func<TItem, IList<TItem>, TItem> _FindConcurrentlyByItem;
        private readonly Func<string, TConcurrency, string, IList<TItem>, TItem> _FindConcurrently;
        private readonly Func<TConcurrency> _NewConcurrencyValue;

        /// <summary>
        /// Initializes a new instance of ListRepositoryWriter.
        /// </summary>
        /// <param name="list">Specifies the list that stores the repository</param>
        /// <param name="setConcurrency">An Action that sets the concurrency values (fromItem, toItem).</param>
        /// <param name="findByItemIdent">A Func that returns an item with the same identity values as the one specified (itemToFind, list).</param>
        /// <param name="findConcurrentlyByItem">A Func that returns an item with the same identity and concurrency values as the one specified (itemToFind, list).</param>
        /// <param name="findConcurrently">A Fonc that returns an item with the specified identity and concurrency values (rowKey, concurrency, partitionKey, list).</param>
        /// <param name="setIdentAndConcurrency">Specifies a method that enables the repository writer to set an 
        /// item's key and concurrency properties (which ares unknown to the repository).</param>
        /// <param name="safeCopy">Specifies if the list items should be copied into a new list. If
        /// the list is not copied then a direct reference to the specified list will be kept.</param>
        public ListRepositoryWriter(
            IList<TItem> list,
            Action<TItem, TConcurrency> setConcurrency, 
            Func<TItem, IList<TItem>, TItem> findByItemIdent,
            Func<TItem, IList<TItem>, TItem> findConcurrentlyByItem,
            Func<string, TConcurrency, string, IList<TItem>, TItem> findConcurrently, 
            Func<TConcurrency> newConcurrencyValue,
            bool safeCopy = true)
            : base(list, safeCopy)
        {
            Contract.Requires<ArgumentNullException>(list != null, "list cannot be null");
            Contract.Requires<ArgumentNullException>(setConcurrency != null, "setSameIdentAndConcurrency cannot be null");

            _SetConcurrency = setConcurrency;
            _FindByItemIdent = findByItemIdent;
            _FindConcurrentlyByItem = findConcurrentlyByItem;
            _FindConcurrently = findConcurrently;
            _NewConcurrencyValue = newConcurrencyValue;
        } // Tested

        /// <summary>
        /// Inserts or updates the specified items in the repository.
        /// </summary>
        /// <param name="items">Specifies the items to insert or update.</param>
        /// <returns>The updated items. Note that depending on the implementation, the result may 
        /// be copies of items passed in; the items therefore may not reflect changes caused by
        /// the backing repository (for example, an auto-generated key).</returns>
        public IEnumerable<TItem> Save(params TItem[] items)
        {
            Debug.WriteLine(String.Format("Saving {0} items.", items.Length));
            foreach (var item in items)
            {
                Debug.WriteLine("Saving item: " + item);

                var result = _FindConcurrentlyByItem(item, List);
                result = (result == null) 
                    ? result = Insert(item).Single()
                    : result = Update(item).Single();

                yield return result;
            }
        } // Tested

        public IEnumerable<TItem> Insert(params TItem[] items)
        {
            foreach (TItem item in items)
            {
                var existingItem = _FindByItemIdent(item, List);
                if (existingItem != null)
                    throw new DBConcurrencyException("Item already exists: " + item);
                
                Debug.WriteLine("Item does not exist, inserting.");

                existingItem = CloneItem(item);

                var concurrency = _NewConcurrencyValue();
                _SetConcurrency(item, concurrency);
                _SetConcurrency(existingItem, concurrency);

                List.Add(existingItem);
                yield return existingItem;
            }
        } //TODO: Test

        public IEnumerable<TItem> Update(params TItem[] items)
        {
            foreach (var item in items)
            {
                var existingItem = _FindConcurrentlyByItem(item, List);

                if (existingItem == null)
                    throw new DBConcurrencyException("Item does not exist to update: " + item);

                item.CopyMatchingObjectPropertiesTo(existingItem);

                var concurrency = _NewConcurrencyValue();
                _SetConcurrency(item, concurrency);
                _SetConcurrency(existingItem, concurrency); 

                Debug.WriteLine(item + " saved");

                yield return existingItem;
            }
        } //TODO: Test

        public int Delete(params TItem[] items)
        {
            var itemsToDelete = new LinkedList<TItem>();
            var result = 0;
            
            foreach (var item in items)
            {
                var existingItem = _FindConcurrentlyByItem(item, List);

                if (existingItem != null)
                {
                    itemsToDelete.AddLast(existingItem);
                    result++;
                }
            }

            Debug.Assert(result == items.Length);                

            foreach (var item in itemsToDelete)
                List.Remove(item);

            return result;
        } // Tested

        public int Delete(string rowKey, TConcurrency concurrency, string partitionKey = null)
        {
            Debug.WriteLine(String.Format("Concurrency value ({0}) ignored.", concurrency));

            var existingItem = _FindConcurrently(rowKey, concurrency, partitionKey, List);

            if (existingItem == null)
                return 0;
            
            var index = List.IndexOf(existingItem);

            if (index < 0)
                return 0;

            List.RemoveAt(index);

            return 1;
        } // Tested
    }
}
