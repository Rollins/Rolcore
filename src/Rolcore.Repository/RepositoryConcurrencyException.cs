using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Rolcore.Repository
{
    [Serializable]
    public class RepositoryConcurrencyException : RepositoryPersistenceException
    {
        public RepositoryConcurrencyException()
        {
            
        }

        public RepositoryConcurrencyException(string message)
            : base(message)
        {
            
        }

        public RepositoryConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }

        protected RepositoryConcurrencyException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            
        }
    }
}
