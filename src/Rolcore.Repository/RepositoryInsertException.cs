using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Rolcore.Repository
{
    [Serializable]
    public class RepositoryInsertException : RepositoryPersistenceException
    {
        public RepositoryInsertException()
        {
            
        }

        public RepositoryInsertException(string message)
            : base(message)
        {
            
        }

        public RepositoryInsertException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }

        protected RepositoryInsertException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            
        }
    }
}
