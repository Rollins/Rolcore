using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Collections;

namespace Rolcore.Repository.ListImpl
{
    /// <summary>
    /// Provides read-access to a repository via the <see cref="IList<>"/> interface.
    /// </summary>
    /// <typeparam name="TItem">Specifies the type of item in the list.</typeparam>
    public class ListRepositoryReader<TItem> : ListRepositoryBase<TItem>, IRepositoryReader<TItem>
        where TItem : class
    {
        private readonly IList<TItem> _CloneList = new List<TItem>();

        private void EnsureClone()
        {
            _CloneList.Clear();
            foreach (var item in List)
            {
                var newItem = CloneItem(item);
                _CloneList.Add(newItem);
            }
        }

        /// <summary>
        /// Initializes a new ListRepositoryReader.
        /// </summary>
        /// <param name="list">Specifies the initial <see cref="List"/> value.</param>
        /// <param name="safeCopy">Specifies if the list items should be copied into a new list. If
        /// the list is not copied then a direct reference to the specified list will be kept.</param>
        public ListRepositoryReader(IList<TItem> list, bool safeCopy = true)
            : base(list, safeCopy)
        {
            Contract.Requires<ArgumentNullException>(list != null, "list cannot be null");
        } // Tested

        /// <summary>
        /// Gets an <see cref="IEnumerable"/> of all items available in the list.
        /// </summary>
        public IEnumerable<TItem> Items
        {
            get
            {
                EnsureClone();
                return _CloneList;
            }
        } // Tested
    }
}
