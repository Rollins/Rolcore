//-----------------------------------------------------------------------
// <copyright file="IRepositoryReader.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Repository
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a read-only repository.
    /// </summary>
    /// <typeparam name="T">Specifies the type of items stored in the repository.</typeparam>
    public interface IRepositoryReader<T>
        where T : class
    {
        /// <summary>
        /// Gets an <see cref="IEnumerable"/> of all items available in the repository.
        /// </summary>
        IEnumerable<T> Items { get; }
    }
}