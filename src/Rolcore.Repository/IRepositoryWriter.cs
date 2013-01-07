//-----------------------------------------------------------------------
// <copyright file="IRepositoryReader.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Repository
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Represents generic write operations for a repository.
    /// </summary>
    /// <typeparam name="TItem">Specifies the type of item stored in the repository.</typeparam>
    /// <typeparam name="TConcurrency">Specifies the type of the field used for optimistic 
    /// concurrency (for example, a DateTime or GUID).</typeparam>
    [ContractClass(typeof(Contracts.IRepositoryWriterContracts<,>))]
    public interface IRepositoryWriter<TItem, TConcurrency>
        where TItem : class
    {
        /// <summary>
        /// Applies associated <see cref="Rules"/> to the specified items.
        /// </summary>
        /// <param name="items">Specifies the items to apply rules to.</param>
        void ApplyRules(params TItem[] items);

        /// <summary>
        /// Inserts the specified items.
        /// </summary>
        /// <param name="items">Specifies the items to insert.</param>
        /// <returns>The inserted items. Note that depending on the implementation, the objects in
        /// the result may be copies of items passed or they may be the original instances.</returns>
        IEnumerable<TItem> Insert(params TItem[] items);

        /// <summary>
        /// Inserts the specified items.
        /// </summary>
        /// <param name="items">Specifies the items to insert.</param>
        /// <returns>The inserted items. Note that depending on the implementation, the objects in
        /// the result may be copies of items passed or they may be the original instances.</returns>
        IEnumerable<TItem> Update(params TItem[] items);

        /// <summary>
        /// Inserts or updates the specified items in the repository.
        /// </summary>
        /// <param name="items">Specifies the items to insert or update.</param>
        /// <returns>The saved items. Note that depending on the implementation, the result may 
        /// be copies of items passed in; the items therefore may not reflect changes caused by
        /// the backing repository (for example, an auto-generated key).</returns>
        IEnumerable<TItem> Save(params TItem[] items);

        /// <summary>
        /// Deletes the specified items in the repository and returns the number of items deleted.
        /// </summary>
        /// <param name="items">Specifies the items to delete.</param>
        /// <returns>The number of items deleted.</returns>
        int Delete(params TItem[] items);

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
        int Delete(string rowKey, TConcurrency concurrency, string partitionKey = null);

        [ImportMany]
        IEnumerable<IRepositoryItemRule<TItem>> Rules { get; set; }
    }
}
