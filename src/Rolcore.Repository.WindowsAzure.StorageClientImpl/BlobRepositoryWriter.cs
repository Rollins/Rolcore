using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.IO;

namespace Rolcore.Repository.WindowsAzure.StorageClientImpl
{
    public class BlobRepositoryWriter<TItem>:
        BlobContainerRepositoryBase, IBlobRepositoryWriter<TItem>
        where TItem: Stream

    {
        public BlobRepositoryWriter(CloudBlobClient blobClient, string containerName) : base(blobClient, containerName) { }

        public BlobRepositoryWriter(CloudStorageAccount storageAccount, string containerName) : base(storageAccount, containerName) { }

        public BlobRepositoryWriter(string connectionString, string containerName) : base(connectionString, containerName) { }
        
        CloudBlob IBlobRepositoryWriter<TItem>.Write(TItem stream, string blobName)
        {
            CloudBlob cloudBlob = Container.GetBlobReference(blobName);
            cloudBlob.UploadFromStream(stream);
            return cloudBlob;
        }

        CloudBlob IBlobRepositoryWriter<TItem>.WriteBlock(TItem stream, string blobName, string blockID)
        {
            CloudBlockBlob cloudBlob = Container.GetBlockBlobReference(blobName);
            cloudBlob.PutBlock(blockID, stream, null);
            return cloudBlob;
        }

        CloudBlob IBlobRepositoryWriter<TItem>.CommitBlocks(string blobName, IEnumerable<string> blockIDs)
        {
            CloudBlockBlob cloudBlob = Container.GetBlockBlobReference(blobName);
            cloudBlob.PutBlockList(blockIDs);
            return cloudBlob;
        }
    }
}
