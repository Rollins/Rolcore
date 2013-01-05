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
        protected string EntitySetName { get; private set; }
        protected TableServiceContext Context { get; private set; }

        #region Constructors
        protected TableServiceContextRepositoryBase(TableServiceContext context, string entitySetName)
        {
            Context = context;
            Context.IgnoreResourceNotFoundException = true;
            EntitySetName = entitySetName;
        } // Tested

        protected TableServiceContextRepositoryBase(CloudTableClient client, string entitySetName)
            : this(client.GetDataServiceContext(), entitySetName)
        {
        } // Tested

        protected TableServiceContextRepositoryBase(CloudStorageAccount storageAccount, string entitySetName)
            : this(storageAccount.CreateCloudTableClient(), entitySetName)
        {
        } // Tested

        protected TableServiceContextRepositoryBase(string connectionString, string entitySetName)
            : this(CloudStorageAccount.Parse(connectionString), entitySetName)
        {
        } // Tested
        #endregion Constructors

        public const string DefaultConnectionStringName = "StorageConnectionString";
    }
}
