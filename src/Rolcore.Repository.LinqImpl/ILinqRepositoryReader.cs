using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rolcore.Repository.LinqImpl
{
    public interface ILinqRepositoryWriter<T, TConcurrency> : IRepositoryWriter<T, TConcurrency>
        where T : class
    {
        bool ItemExists(T item);
    }
}
