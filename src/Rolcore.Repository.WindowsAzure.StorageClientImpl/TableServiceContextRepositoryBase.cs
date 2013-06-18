using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;

namespace Rolcore.Repository.WindowsAzure.StorageClientImpl
{
    /// <summary>
    /// Base class for implementing a repository based on the Azure Table service.
    /// </summary>
    public abstract class TableServiceContextRepositoryBase
    {
        /// <summary>
        /// Gets or sets the entity set name where the repository's entities are stored.
        /// </summary>
        protected string EntitySetName { get; private set; }

        /// <summary>
        /// Gets or sets the backing <see cref="TableServiceContext"/> instance.
        /// </summary>
        protected TableServiceContext Context { get; private set; }

        #region Constructors
        /// <summary>
        /// Initializes a new <see cref="TableServiceContextRepositoryBase"/>.
        /// </summary>
        /// <param name="context">The value for <see cref="Context"/>.</param>
        /// <param name="entitySetName">The value for <see cref="EntitySetName"/>.</param>
        protected TableServiceContextRepositoryBase(TableServiceContext context, string entitySetName)
        {
            Context = context;
            Context.IgnoreResourceNotFoundException = true;
            EntitySetName = entitySetName;
        } // Tested

        /// <summary>
        /// Initializes a new <see cref="TableServiceContextRepositoryBase"/>.
        /// </summary>
        /// <param name="client">A <see cref="CloudTableClient"/> that provides access to the 
        /// backing <see cref="TableServiceContext"/></param>
        /// <param name="entitySetName">The value for <see cref="EntitySetName"/>.</param>
        protected TableServiceContextRepositoryBase(CloudTableClient client, string entitySetName)
            : this(client.GetDataServiceContext(), entitySetName)
        {
            client.CreateTableIfNotExist(entitySetName);
        } // Tested

        /// <summary>
        /// Initializes a new <see cref="TableServiceContextRepositoryBase"/>.
        /// </summary>
        /// <param name="storageAccount">Specifies the <see cref="CloudStorageAccount"/> in which 
        /// entities are to be stored.</param>
        /// <param name="entitySetName">The value for <see cref="EntitySetName"/>.</param>
        protected TableServiceContextRepositoryBase(CloudStorageAccount storageAccount, string entitySetName)
            : this(storageAccount.CreateCloudTableClient(), entitySetName)
        {
        } // Tested

        /// <summary>
        /// Initializes a new <see cref="TableServiceContextRepositoryBase"/>.
        /// </summary>
        /// <param name="connectionString">Specifies the connection string to the cloud storage 
        /// account in which entities are to be stored.</param>
        /// <param name="entitySetName">The value for <see cref="EntitySetName"/>.</param>
        protected TableServiceContextRepositoryBase(string connectionString, string entitySetName)
            : this(CloudStorageAccount.Parse(connectionString), entitySetName)
        {
        } // Tested
        #endregion Constructors

        public const string DefaultConnectionStringName = "StorageConnectionString";
    }
}
