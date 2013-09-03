//-----------------------------------------------------------------------
// <copyright file="TableServiceContextRepositoryWriter.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Repository.WindowsAzure.StorageClientImpl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Data.Services.Client;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;
    using Rolcore.Reflection;

    /// <summary>
    /// Implements <see cref="IRepositoryWriter{}"/> using a <see cref="TableServiceContext"/> as 
    /// the storage mechanism.
    /// </summary>
    /// <typeparam name="TItem">Specifies the type of item stored in the repository.</typeparam>
    public class TableServiceContextRepositoryWriter<TItem> 
        : TableServiceContextRepositoryBase, 
          IRepositoryWriter<TItem, DateTime>
        where TItem : class
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TableServiceContextRepositoryWriter{TItem}"/> class.
        /// </summary>
        /// <param name="context">The value for <see cref="Context"/>.</param>
        /// <param name="entitySetName">The value for <see cref="EntitySetName"/>.</param>
        public TableServiceContextRepositoryWriter(TableServiceContext context, string entitySetName)
            : base(context, entitySetName)
        {   
        } // Tested

        /// <summary>
        /// Initializes a new instance of the <see cref="TableServiceContextRepositoryWriter{TItem}"/> class.
        /// </summary>
        /// <param name="client">A <see cref="CloudTableClient"/> that provides access to the 
        /// backing <see cref="TableServiceContext"/></param>
        /// <param name="entitySetName">The value for <see cref="EntitySetName"/>.</param>
        public TableServiceContextRepositoryWriter(CloudTableClient client, string entitySetName)
            : base(client, entitySetName)
        {
        } // Tested

        /// <summary>
        /// Initializes a new instance of the <see cref="TableServiceContextRepositoryWriter{TItem}"/> class.
        /// </summary>
        /// <param name="storageAccount">Specifies the <see cref="CloudStorageAccount"/> in which 
        /// entities are to be stored.</param>
        /// <param name="entitySetName">The value for <see cref="EntitySetName"/>.</param>
        public TableServiceContextRepositoryWriter(CloudStorageAccount storageAccount, string entitySetName)
            : base(storageAccount, entitySetName)
        {
        } // Tested

        /// <summary>
        /// Initializes a new instance of the <see cref="TableServiceContextRepositoryWriter{TItem}"/> class.
        /// </summary>
        /// <param name="connectionString">Specifies the connection string to the cloud storage 
        /// account in which entities are to be stored.</param>
        /// <param name="entitySetName">The value for <see cref="EntitySetName"/>.</param>
        public TableServiceContextRepositoryWriter(string connectionString, string entitySetName)
            : base(connectionString, entitySetName)
        {
        } // Tested
        #endregion Constructors

        /// <summary>
        /// Gets or sets the rules to apply to items prior to insert or update operations.
        /// </summary>
        [ImportMany]
        public IEnumerable<IRepositoryItemRule<TItem>> Rules { get; set; }

        #region DataServiceClientException Handling Methods
        /// <summary>
        /// Force "insert or replace" to work on the local storage emulator. From 
        /// http://www.windowsazure.com/en-us/develop/net/how-to-guides/table-services/#insert-entity:
        /// "Note that insert-or-replace is not supported on the local storage emulator, so this 
        /// code runs only when using an account on the table service."
        /// </summary>
        /// <param name="items">Specifies the items on which the exception occurred.</param>
        /// <param name="ex">Specifies the exception</param>
        /// <returns>The persisted items</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        private void SaveUsingDevStorage(TableServiceContext context, TItem item)
        {
            dynamic dynItem = (item as dynamic);
            var entityCheat = new EntityCheat() { PartitionKey = dynItem.PartitionKey, RowKey = dynItem.RowKey, Timestamp = dynItem.Timestamp };

            var existingEntity = context.CreateQuery<EntityCheat>(EntitySetName)
                .Where(e =>
                    e.PartitionKey == entityCheat.PartitionKey
                 && e.RowKey == entityCheat.RowKey)
                .SingleOrDefault();
            if (existingEntity == null)
            {
                context.AddObject(EntitySetName, item);
            }
            else
            {
                if (existingEntity.Timestamp != dynItem.Timestamp)
                {
                    throw new RepositoryConcurrencyException(
                        "Record has been modified outside the current save operation.");
                }
                context.Detach(existingEntity);
                context.AttachTo(EntitySetName, item, "*");
                context.UpdateObject(item);
            }
        }

        private TItem[] Handle400DataServiceClientException(TItem[] items, DataServiceRequestException ex)
        {
            // This sometimes happens during an update in dev storage during upsert, though it's 
            // not really clear why.

            Trace.TraceWarning("Local dev storage detected. If you are reading this in production, you may wish to freak out.");
            Trace.Indent();
            Trace.TraceError(ex.ToString());
            Trace.Unindent();

            return this.Update(items);
        }
        #endregion DataServiceClientException Handling Methods

        #region Private Methods
        private static void EnsureRowKey(TItem item)
        {
            dynamic dynItem = (dynamic)item;
            if (string.IsNullOrEmpty(dynItem.RowKey))
            {
                var rowKey = Guid.NewGuid().ToString();
                dynItem.RowKey = rowKey;
                Trace.TraceWarning(
                    string.Format(
                        "Item {0} probably should have had a value for RowKey when it was saved but didn't, so one was generated.",
                        rowKey));
            }
        }

        /// <summary>
        /// Clones the backing <see cref="TableServiceContext"/>.
        /// </summary>
        /// <returns>A copy of <see cref="Context"/>.</returns>
        private TableServiceContext CloneContext()
        {
            var result = new TableServiceContext(this.Context.BaseUri.ToString(), Context.StorageCredentials);
            Context.CopyMatchingObjectPropertiesTo(result);
            result.IgnoreResourceNotFoundException = true; // prevents SingleOrDefault from throwing an exception
            return result;
        }

        /// <summary>
        /// Gets the specified items ETag.
        /// </summary>
        /// <param name="item">Specifies the item.</param>
        /// <returns>The ETag value of the item.</returns>
        private string GetETag(TItem item)
        {
            dynamic dynItem = item;
            return Context.Entities
                .Where(e =>
                    ((dynamic)e.Entity).PartitionKey == dynItem.PartitionKey
                    && ((dynamic)e.Entity).RowKey == dynItem.RowKey)
                .Select(e => e.ETag)
                .SingleOrDefault();
        }

        /// <summary>
        /// Attaches the specified item to the specified context.
        /// </summary>
        /// <param name="context">The context to attach the item to.</param>
        /// <param name="item">The item to attach.</param>
        private void AttachTo(TableServiceContext context, TItem item)
        {
            EnsureRowKey(item);

            var etag = GetETag(item);

            if (etag != null)
            {
                context.AttachTo(EntitySetName, item, etag);
            }
            else
            {
                context.AttachTo(EntitySetName, item);
            }
        }
        #endregion Private Methods

        /// <summary>
        /// Applies associated <see cref="Rules"/> to the specified items.
        /// </summary>
        /// <param name="items">Specifies the items to apply rules to.</param>
        public void ApplyRules(params TItem[] items)
        {
            this.ApplyRulesDefaultImplementation(items);
        }

        /// <summary>
        /// Inserts or updates the specified items in the repository.
        /// </summary>
        /// <param name="items">Specifies the items to insert or update.</param>
        /// <returns>The saved items. Note that depending on the implementation, the result may 
        /// be copies of items passed in; the items therefore may not reflect changes caused by
        /// the backing repository (for example, an auto-generated key).</returns>
        public TItem[] Save(params TItem[] items)
        {
            this.ApplyRules(items);
            var context = this.CloneContext();
            foreach (TItem item in items)
            {
                if (context.BaseUri.IsLoopback)
                {
                    this.SaveUsingDevStorage(context, item);
                }
                else
                {
                    this.AttachTo(context, item);
                    context.UpdateObject(item);
                }
            }

            try
            {
                context.SaveChangesWithRetries(SaveChangesOptions.ReplaceOnUpdate);
                return items;
            }
            catch (DataServiceRequestException ex)
            {
                var innerException = ex.InnerException as DataServiceClientException;

                if (innerException == null)
                {
                    throw;
                }

                // Exceptions: http://technet.microsoft.com/en-us/library/dd179438.aspx

                // 400 BadRequest: DuplicatePropertiesSpecified, EntityTooLarge, InvalidValueType, 
                //   PropertiesNeedValue, PropertyNameTooLong, TooManyProperties, 
                //   XMethodIncorrectCount, XMethodIncorrectValue, XMethodNotUsingPost.
                if (innerException.StatusCode == 400) // 400 = "Bad Request"
                {
                    return Handle400DataServiceClientException(items, ex);
                }
                // 412 UpdateConditionNotSatisfied (concurrency)
                else if (innerException.StatusCode != 412)
                {
                    throw new RepositoryConcurrencyException(
                        "Record has been modified outside the current save operation.",
                        innerException);
                }
                else
                {
                    throw;
                }
            }
        } // Tested

        /// <summary>
        /// Inserts the specified items.
        /// </summary>
        /// <param name="items">Specifies the items to insert.</param>
        /// <returns>The inserted items.</returns>
        public TItem[] Insert(params TItem[] items)
        {
            this.ApplyRules(items);
            var context = CloneContext();
            var result = new List<TItem>(items.Length);
            foreach (TItem item in items)
            {
                EnsureRowKey(item);
                context.AddObject(EntitySetName, item);
                result.Add(item);
            }

            try
            {
                context.SaveChangesWithRetries(SaveChangesOptions.Batch);
            }
            catch (DataServiceRequestException ex)
            {
                throw new RepositoryInsertException(ex.Message, ex);
            }

            return result.ToArray();
        } // Tested

        /// <summary>
        /// Inserts the specified items.
        /// </summary>
        /// <param name="items">Specifies the items to insert.</param>
        /// <returns>The inserted items.</returns>
        public TItem[] Update(params TItem[] items)
        {
            this.ApplyRules(items);
            var context = CloneContext();
            var result = new List<TItem>(items.Length);
            foreach (TItem item in items)
            {
                this.AttachTo(context, item);
                context.UpdateObject(item);
                result.Add(item);
            }

            context.SaveChangesWithRetries(SaveChangesOptions.Batch);

            return result.ToArray();
        } // TODO: Test

        /// <summary>
        /// Deletes the specified items in the repository and returns the number of items deleted.
        /// </summary>
        /// <param name="items">Specifies the items to delete.</param>
        /// <returns>The number of items deleted.</returns>
        public int Delete(params TItem[] items)
        {
            var context = CloneContext();
            foreach (var item in items)
            {
                AttachTo(context, item);
                context.DeleteObject(item);
            }

            context.SaveChangesWithRetries(SaveChangesOptions.Batch);

            return items.Count();
        }// Tested

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
        public int Delete(string rowKey, DateTime concurrency, string partitionKey)
        {
            var itemsToDelete = Context.CreateQuery<TItem>(this.EntitySetName).AsEnumerable()
                .Where(item => 
                    (item as dynamic).RowKey == rowKey &&
                    (item as dynamic).Timestamp == concurrency);

            if (partitionKey != null)
            {
                itemsToDelete = itemsToDelete
                    .Where(item =>
                        (item as dynamic).PartitionKey == partitionKey);
            }

            if (itemsToDelete.Any())
            {
                return Delete(itemsToDelete.ToArray());
            }

            return 0;
        } // Tested
    }
}
