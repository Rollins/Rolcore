namespace Rolcore.Repository.Contracts
{
    using System;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(IRepositoryItemRule<>))]
    public abstract class IRepositoryItemRuleContracts<T> : IRepositoryItemRule<T>
        where T : class
    {
        void IRepositoryItemRule<T>.Apply(T item)
        {
            Contract.Requires<ArgumentNullException>(item != null, "item is null");
        }
    }
}
