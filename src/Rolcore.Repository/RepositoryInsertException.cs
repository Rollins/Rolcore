//-----------------------------------------------------------------------
// <copyright file="RepositoryInsertException.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an error that occurs during an insert (create) operation.
    /// </summary>
    [Serializable]
    public class RepositoryInsertException : RepositoryPersistenceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryInsertException"/> class.
        /// </summary>
        public RepositoryInsertException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryInsertException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RepositoryInsertException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryInsertException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, 
        /// or a null reference if no inner exception is specified.</param>
        public RepositoryInsertException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryInsertException"/> class.
        /// </summary>
        /// <param name="context">The <see cref="SerializationInfo"/> that holds the serialized 
        /// object data about the exception being thrown.</param>
        /// <param name="info">The <see cref="StreamingContext"/> that contains contextual 
        /// information about the source or destination.</param>
        protected RepositoryInsertException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            
        }
    }
}
