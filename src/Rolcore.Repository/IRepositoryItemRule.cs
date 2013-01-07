using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rolcore.Repository
{
    [ContractClass(typeof(Contracts.IRepositoryItemRuleContracts<>))]
    public interface IRepositoryItemRule<T>
        where T : class
    {
        void Apply(T item);
    }
}
