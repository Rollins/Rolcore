namespace Rolcore.Repository.LinqImpl
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Linq;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using Rolcore.Reflection;
    
    public class LinqRepositoryWriter<TItem, TBase, TConcurrency> 
        : LinqRepositoryBase<TItem, TBase>, 
          IRepositoryWriter<TBase, TConcurrency>
        where TBase : class
        where TItem : class, TBase
    {
        private readonly Type _ItemType;
        private readonly Action<TItem, string, TConcurrency, string> _SetData;
        private readonly Func<TBase, bool> _ItemExists;

        private static TBase NewItem()
        {
            Contract.Ensures(Contract.Result<TBase>() != null, "NewItem() returned null.");
            var result = Activator.CreateInstance<TItem>();
            return result;
        }

        private IEnumerable<TItem> SubmitChanges()
        {
            try
            {
                var changes = Table.Context.GetChangeSet();
                var result = changes.Inserts
                    .Where(insert =>
                        insert.GetType() == _ItemType)
                    .Union(changes.Updates
                        .Where(update =>
                            update.GetType() == _ItemType))
                    .Union(changes.Deletes
                        .Where(delete =>
                            delete.GetType() == _ItemType))
                    .Cast<TItem>()
                    .ToArray();

                Table.Context.SubmitChanges();

                return result;
            }
            catch (ChangeConflictException exception)
            {
                //
                // Required by interface (see unit tests for IRepositoryWriter)

                throw new DBConcurrencyException(exception.Message, exception);
            }
        }

        /// <summary>
        /// Utility function for "converting up" - taking an object of TBase and returning the 
        /// equivelant TConcrete instance. Typically this can be done by type casting (e.g. item 
        /// as TConcrete), but not always. For example, if the current repository is passed an item
        /// from a separate repository using a different type for TConcrete or a "plain" TBase 
        /// instance that is not of type TConcrete.
        /// </summary>
        /// <param name="item">Specifies the item which is to  be converted to a TItem.</param>
        /// <returns>The TConcrete equivelant of the specified TBase. This may be the same 
        /// instance, or it may be a copy; so BEWARE!</returns>
        protected TItem ConcreteFromBase(TBase item)
        {
            Contract.Requires<ArgumentNullException>(item != null, "item cannot be null");
            Contract.Ensures(Contract.Result<TItem>() != null, "ConcreteFromBase() returned null.");

            var result = item as TItem;
            if (result != null)
                return result;
            result = NewItem() as TItem;
            item.CopyMatchingObjectPropertiesTo(result);
            Debug.Assert(result != null, "TItem does not implement / inherit TBase. That ought to be impossible!");
            return result;
        }

        /// <summary>
        /// Utility function for "converting up" - taking an enumerable of TBase and returning the 
        /// equivelant TConcrete enumerable.
        /// </summary>
        /// <param name="items">Specifies the items to convert to TConcrete instances.</param>
        /// <returns>An IEnumerable of TConcretes, equivelant of the specified TBase enumerable. 
        /// This may contain the same instances, different instances, or a mix; so BEWARE!</returns>
        protected IEnumerable<TItem> ConcreteFromBase(IEnumerable<TBase> items)
        {
            foreach (var item in items)
                yield return ConcreteFromBase(item);
        }

        private void EnsureItemIsAttached(TItem item, bool asModified)
        {
            Contract.Requires<ArgumentNullException>(item != null, "item cannot be null");

            TItem original = Table.GetOriginalEntityState(item);
            if (original == null)
                try
                {
                    Table.Attach(item, asModified);
                    Debug.WriteLine(String.Format("Item attached: {0}", item));
                }
                catch (DuplicateKeyException)
                {
                    Debug.WriteLine(String.Format("A copy of the item is already attached: {0}", item));
                }
        }

        public LinqRepositoryWriter(Table<TItem> table, Action<TItem, string, TConcurrency, string> setData, Func<TBase, bool> itemExists)
            : base(table)
        {
            Contract.Requires<ArgumentNullException>(setData != null, "setData cannot be null");
            Contract.Requires<ArgumentNullException>(itemExists != null, "itemExists cannot be null");
            Contract.Ensures(_ItemType != null);
            Contract.Ensures(_ItemExists != null);

            _ItemType = typeof(TItem);
            _SetData = setData;
            _ItemExists = itemExists;
        }

        public IEnumerable<TBase> Save(params TBase[] items)
        {
            foreach (var item in items)
            {
                var concrete = ConcreteFromBase(item);
                if (ItemExists(concrete))
                    EnsureItemIsAttached(concrete, true);
                else
                {
                    Table.InsertOnSubmit(concrete);
                    Debug.WriteLine(string.Format("Inserting: {0}", item));
                }
            }

            return SubmitChanges();
        }

        public IEnumerable<TBase> Insert(params TBase[] items)
        {
            foreach (var item in items)
            {
                var concrete = ConcreteFromBase(item);
                Table.InsertOnSubmit(concrete);
                Debug.WriteLine(string.Format("Inserting: {0}", item));
            }

            return SubmitChanges();
        }

        public IEnumerable<TBase> Update(params TBase[] items)
        {
            foreach (var item in items)
            {
                var concrete = ConcreteFromBase(item);
                EnsureItemIsAttached(concrete, true);
            }

            return SubmitChanges();
        }

        public int Delete(params TBase[] items)
        {
            var concreteItems = ConcreteFromBase(items);
            foreach (var item in concreteItems)
                EnsureItemIsAttached(item, false);

            Table.DeleteAllOnSubmit(concreteItems);

            return SubmitChanges().Count();
        }

        public int Delete(string rowKey, TConcurrency concurrency, string partitionKey = null)
        {
            var item = Activator.CreateInstance<TItem>();
            _SetData(item, rowKey, concurrency, partitionKey);
            if (!ItemExists(item))
                return 0;
            EnsureItemIsAttached(item, false);
            Table.DeleteOnSubmit(item);
            Table.Context.SubmitChanges();
            return 1;
        }

        public bool ItemExists(TBase item)
        {
            return _ItemExists(item);
        }
    }
}
