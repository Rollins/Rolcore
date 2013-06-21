using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Rolcore.Reflection;

namespace Rolcore.Repository.ListImpl
{
    /// <summary>
    /// Base class for a <see cref="List<>"/>-backed repository reader or writer.
    /// </summary>
    /// <typeparam name="TItem">Specifies the type of item stored in the repository.</typeparam>
    public abstract class ListRepositoryBase<TItem>
        where TItem : class
    {
        protected static TItem CloneItem(TItem item)
        {
            TItem result;
            var clonable = (item as ICloneable);
            if (clonable != null)
                result = (TItem)clonable.Clone();
            else
            {
                result = Activator.CreateInstance<TItem>();
                item.CopyMatchingObjectPropertiesTo(result);
            }

            return result;
        }

        /// <summary>
        /// Gets and sets the list which contains the repository.
        /// </summary>
        protected internal IList<TItem> List { get; set; }

        /// <summary>
        /// Initializes a new list repository.
        /// </summary>
        /// <param name="list">Specifies the initial <see cref="List"/> value.</param>
        /// <param name="safeCopy">Specifies if the list items should be copied into a new list. If
        /// the list is not copied then a direct reference to the specified list will be kept.</param>
        protected ListRepositoryBase(IList<TItem> list, bool safeCopy = true)
        {
            Contract.Requires<ArgumentNullException>(list != null, "list cannot be null");
            Contract.Ensures(List != null, "List cannot be null");

            if (safeCopy)
                List = new List<TItem>(list); // Our own copy
            else
                List = list;
        } // TOOD: Unit test
    }
}
