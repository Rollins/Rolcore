using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;

namespace Rolcore.Repository.WindowsAzure.StorageClientImpl
{
    /// <summary>
    /// Provides an <see cref="IRepositoryReader<>"/> implementation using
    /// <see cref="TableServiceContext"/> as the backing store.
    /// </summary>
    /// <typeparam name="TItem">Specifies the type of item to be contained in the repository.</typeparam>
    public class TableServiceContextRepositoryReader<TItem> : TableServiceContextRepositoryBase, IRepositoryReader<TItem>
        where TItem : class
    {
        
        public TableServiceContextRepositoryReader(TableServiceContext context, string entitySetName)
            : base(context, entitySetName)
        {
            
        } // Tested

        public TableServiceContextRepositoryReader(CloudTableClient client, string entitySetName)
            : base(client, entitySetName)
        {
        } // Tested

        public TableServiceContextRepositoryReader(CloudStorageAccount storageAccount, string entitySetName)
            : base(storageAccount, entitySetName)
        {
        } // Tested

        public TableServiceContextRepositoryReader(string connectionString, string entitySetName)
            : base(connectionString, entitySetName)
        {
        } // Tested

        public IQueryable<TItem> Items
        {
            get 
            {
                return Context.CreateQuery<TItem>(EntitySetName);
            }
        } // Tested
    }
}
