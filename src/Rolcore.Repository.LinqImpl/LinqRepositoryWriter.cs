using System.Threading.Tasks;
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
    using System.ComponentModel.Composition;
    using System.Data.SqlClient;
    using System.Reflection;
    
    public class LinqRepositoryWriter<TDataContext, TItem, TBase, TConcurrency>
        : LinqRepositoryBase<TDataContext, TItem, TBase>, 
          IRepositoryWriter<TBase, TConcurrency>
        where TDataContext : DataContext
        where TBase : class
        where TItem : class, TBase
    {
        private readonly IEnumerable<PropertyInfo> entitySetProperties = typeof(TItem).GetProperties().Where(t => t.PropertyType.IsGenericType && t.PropertyType.GetGenericTypeDefinition() == typeof(EntitySet<>));
        private readonly Type itemType;
        private readonly Action<TItem, string, TConcurrency, string> setKeyAndConcurrencyValues;
        private readonly Func<TBase, Table<TItem>, bool> itemExists;

        private static TBase NewItem()
        {
            Contract.Ensures(Contract.Result<TBase>() != null, "NewItem() returned null.");
            var result = Activator.CreateInstance<TItem>();
            return result;
        }

        private TItem[] SubmitChanges(TDataContext context)
        {
            try
            {
                var changes = context.GetChangeSet();
                var result = changes.Inserts
                    .Where(insert =>
                        insert.GetType() == itemType)
                    .Union(changes.Updates
                        .Where(update =>
                            update.GetType() == itemType))
                    .Union(changes.Deletes
                        .Where(delete =>
                            delete.GetType() == itemType))
                    .Cast<TItem>()
                    .ToArray();

                context.SubmitChanges();

                return result;
            }
            catch (ChangeConflictException exception)
            {
                // Required by interface (see unit tests for IRepositoryWriter)
                throw new RepositoryConcurrencyException(exception.Message, exception);
            }
            catch (DuplicateKeyException exception)
            {
                // Required by interface (see unit tests for IRepositoryWriter)
                throw new RepositoryInsertException(exception.Message, exception);
            }
        }

        /// <summary>
        /// Utility function for "converting up" - taking an object of TBase and returning the 
        /// equivalent TConcrete instance. Typically this can be done by type casting (e.g. item 
        /// as TConcrete), but not always. For example, if the current repository is passed an item
        /// from a separate repository using a different type for TConcrete or a "plain" TBase 
        /// instance that is not of type TConcrete.
        /// </summary>
        /// <param name="item">Specifies the item which is to  be converted to a TItem.</param>
        /// <returns>The TConcrete equivalent of the specified TBase. This may be the same 
        /// instance, or it may be a copy; so BEWARE!</returns>
        protected static TItem ConcreteFromBase(TBase item)
        {
            Contract.Requires<ArgumentNullException>(item != null, "item cannot be null");
            Contract.Ensures(Contract.Result<TItem>() != null, "ConcreteFromBase() returned null."); // http://stackoverflow.com/questions/4852022/codecontract-think-assigned-readonly-field-can-be-null

            var result = item as TItem;
            if (result != null)
            {
                return result;
            }

            result = (TItem)NewItem();
            Contract.Assume(result != null, "Result was null somehow.");
            item.CopyMatchingObjectPropertiesTo(result);
            return result;
        }

        /// <summary>
        /// Utility function for "converting up" - taking an enumerable of TBase and returning the 
        /// equivalent TConcrete enumerable.
        /// </summary>
        /// <param name="items">Specifies the items to convert to TConcrete instances.</param>
        /// <returns>An IEnumerable of TConcretes, equivalent of the specified TBase enumerable. 
        /// This may contain the same instances, different instances, or a mix; so BEWARE!</returns>
        protected static IEnumerable<TItem> ConcreteFromBase(IEnumerable<TBase> items)
        {
            Contract.Requires<ArgumentNullException>(items != null, "items is null");
            foreach (var item in items)
            {
                yield return ConcreteFromBase(item);
            }
        }

        protected static Table<TItem> GetTable(TDataContext context)
        {
            var result = context.GetTable<TItem>();
            if (result == null)
            {
                throw new InvalidOperationException(string.Format("{0} does not contain a table for type {1}", context.GetType(), typeof(TItem)));
            }

            return result;
        }

        private void EnsureItemIsAttached(Table<TItem> table, TItem item, bool asModified)
        {
            Contract.Requires<ArgumentNullException>(item != null, "item is null");

            TItem original = table.GetOriginalEntityState(item);
            if (original == null)
            {
                Parallel.ForEach(entitySetProperties, entitySet =>
                {
                    entitySet.SetValue(item, null);
                });
                try
                {
                    table.Attach(item, asModified);
                    Debug.WriteLine(String.Format("Item attached: {0}", item));
                }
                catch (DuplicateKeyException ex)
                {
                    Debug.WriteLine(String.Format("A copy of the item is already attached: {0}", item));
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public LinqRepositoryWriter(
            Func<TDataContext> dataContextFactory, 
            Action<TItem, string, TConcurrency, string> setKeyAndConcurrencyValues, 
            Func<TBase, Table<TItem>, bool> itemExists)
            : base(dataContextFactory)
        {
            Contract.Requires<ArgumentNullException>(setKeyAndConcurrencyValues != null, "setKeyAndConcurrencyValues cannot be null");
            Contract.Requires<ArgumentNullException>(itemExists != null, "itemExists cannot be null");
            Contract.Ensures(itemType != null);
            Contract.Ensures(itemExists != null);

            this.itemType = typeof(TItem);
            this.setKeyAndConcurrencyValues = setKeyAndConcurrencyValues;
            this.itemExists = itemExists;
        }

        public void ApplyRules(params TBase[] items)
        {
            this.ApplyRulesDefaultImplementation(items);
        }

        public TBase[] Save(params TBase[] items)
        {
            this.ApplyRules(items);
            using (var context = this.CreateDataContext())
            {
                var table = GetTable(context);
                foreach (var item in items)
                {
                    var concrete = ConcreteFromBase(item);
                    if (ItemExists(concrete, table))
                    {
                        EnsureItemIsAttached(table, concrete, true);
                    }
                    else
                    {
                        table.InsertOnSubmit(concrete);
                    }
                }

                return SubmitChanges(context);
            }
        }

        public TBase[] Insert(params TBase[] items)
        {
            this.ApplyRules(items);
            using (var context = this.CreateDataContext())
            {
                var table = GetTable(context);
                foreach (var item in items)
                {
                    var concrete = ConcreteFromBase(item);
                    table.InsertOnSubmit(concrete);
                    Debug.WriteLine(string.Format("Inserting: {0}", item));
                }

                try
                {
                    return this.SubmitChanges(context);
                }
                catch (SqlException ex)
                {
                    throw new RepositoryInsertException(ex.Message, ex);
                }
            }
        }

        public TBase[] Update(params TBase[] items)
        {
            this.ApplyRules(items);
            using (var context = this.CreateDataContext())
            {
                var table = GetTable(context);
                foreach (var item in items)
                {
                    var concrete = ConcreteFromBase(item);
                    EnsureItemIsAttached(table, concrete, true);
                }

                return this.SubmitChanges(context);
            }
        }

        public int Delete(params TBase[] items)
        {
            var concreteItems = ConcreteFromBase(items);
            using (var context = this.CreateDataContext())
            {
                var table = GetTable(context);
                foreach (var item in concreteItems)
                {
                    EnsureItemIsAttached(table, item, false);
                }

                table.DeleteAllOnSubmit(concreteItems);
                return this.SubmitChanges(context).Length;
            }
        }

        public int Delete(string rowKey, TConcurrency concurrency, string partitionKey = null)
        {
            var item = Activator.CreateInstance<TItem>();
            setKeyAndConcurrencyValues(item, rowKey, concurrency, partitionKey);

            using (var context = this.CreateDataContext())
            {
                var table = GetTable(context);
                if (!this.ItemExists(item, table))
                {
                    return 0;
                }

                this.EnsureItemIsAttached(table, item, false);
                table.DeleteOnSubmit(item);
                table.Context.SubmitChanges();
                return 1;
            }
        }

        [ImportMany]
        public IEnumerable<IRepositoryItemRule<TBase>> Rules { get; set; }

        public bool ItemExists(TBase item, Table<TItem> table)
        {
            return itemExists(item, table);
        }
    }
}
