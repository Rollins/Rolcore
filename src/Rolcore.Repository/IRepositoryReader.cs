using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rolcore.Repository
{
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