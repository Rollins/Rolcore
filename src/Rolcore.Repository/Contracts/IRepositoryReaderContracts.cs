namespace Rolcore.Repository.Contracts
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// Contract class for <see cref="IRepositoryReader{}"/>.
    /// </summary>
    /// <typeparam name="T">The type of items stored in the repository.</typeparam>
    [ContractClassFor(typeof(IRepositoryReader<>))]
    public abstract class IRepositoryReaderContracts<T> : IRepositoryReader<T>
        where T : class
    {
        /// <summary>
        /// Requires Items to return a non-null result.
        /// </summary>
        public IQueryable<T> Items()
        {
            Contract.Ensures(Contract.Result<IQueryable<T>>() != null, "Items is null");
            return default(IQueryable<T>);
        }
    }
}
