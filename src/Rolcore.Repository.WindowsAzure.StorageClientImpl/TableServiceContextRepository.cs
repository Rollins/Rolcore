using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace Rolcore.Repository.WindowsAzure.StorageClientImpl
{
    public class TableServiceContextRepository<TItem> : Repository<TItem, DateTime>, IRepositoryReader<TItem>, IRepositoryWriter<TItem, DateTime>
        where TItem : class
    {
        public TableServiceContextRepository(CloudStorageAccount account, string entitySetName) 
            : base (
                new TableServiceContextRepositoryReader<TItem>(account, entitySetName), 
                new TableServiceContextRepositoryWriter<TItem>(account, entitySetName))
        { }

        public TableServiceContextRepository(CloudTableClient client, string entitySetName)
            : base(
                new TableServiceContextRepositoryReader<TItem>(client, entitySetName),
                new TableServiceContextRepositoryWriter<TItem>(client, entitySetName))
        { }

        public TableServiceContextRepository(string connectionString, string entitySetName)
            : base(
                new TableServiceContextRepositoryReader<TItem>(connectionString, entitySetName),
                new TableServiceContextRepositoryWriter<TItem>(connectionString, entitySetName))
        { }

        public TableServiceContextRepository(TableServiceContext context, string entitySetName)
            : base(
                new TableServiceContextRepositoryReader<TItem>(context, entitySetName),
                new TableServiceContextRepositoryWriter<TItem>(context, entitySetName))
        { }
    }
}
