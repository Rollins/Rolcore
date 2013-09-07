//-----------------------------------------------------------------------
// <copyright file="RepositoryPersistenceException.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Repository
{
    using System;

    [Serializable]
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
