using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Rolcore.Repository
{
    public class RepositoryPersistenceException : Exception
    {
        public RepositoryPersistenceException()
        {
            
        }
        public RepositoryPersistenceException(string message)
            : base(message)
        {
            
        }
        public RepositoryPersistenceException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
        protected RepositoryPersistenceException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            
        }
    }
}
