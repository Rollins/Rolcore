//-----------------------------------------------------------------------
// <copyright file="IRepositoryReader.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Repository
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// Represents a read-only repository.
    /// </summary>
    /// <typeparam name="T">Specifies the type of items stored in the repository.</typeparam>
    [ContractClass(typeof(Contracts.IRepositoryReaderContracts<>))]
    public interface IRepositoryReader<T>
        where T : class
    {
        /// <summary>
        /// Gets an <see cref="IEnumerable"/> of all items available in the repository.
        /// </summary>
        IQueryable<T> Items();
    }
}