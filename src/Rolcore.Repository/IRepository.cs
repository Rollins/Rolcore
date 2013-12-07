//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Repository
{
    /// <summary>
    /// Represents a generic read/write repository of objects.
    /// </summary>
    /// <typeparam name="TItem">Specifies the type of item stored in the repository.</typeparam>
    /// <typeparam name="TConcurrency">Specifies the type of the field used for optimistic 
    /// concurrency (for example, a DateTime or GUID).</typeparam>
    public interface IRepository<TItem, TConcurrency> : IRepositoryReader<TItem>, IRepositoryWriter<TItem, TConcurrency>
        where TItem : class
    {
        
    }
}