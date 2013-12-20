using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace Rolcore.Repository.WindowsAzure.StorageClientImpl
{
    public class BlobRepository<TItem> : IBlobRepositoryReader<TItem>, IBlobRepositoryWriter<Stream>
        where TItem : CloudBlob
    {
        protected readonly IBlobRepositoryReader<TItem> _Reader;
        protected readonly IBlobRepositoryWriter<Stream> _Writer;

        public BlobRepository(string connectionString, string containerName)
        {
            _Reader = new BlobRepositoryReader<TItem>(connectionString, containerName);
            _Writer = new BlobRepositoryWriter<Stream>(connectionString, containerName);
        }

        public BlobRepository(CloudStorageAccount storageAccount, string containerName)
        {
            _Reader = new BlobRepositoryReader<TItem>(storageAccount, containerName);
            _Writer = new BlobRepositoryWriter<Stream>(storageAccount, containerName);
        }

        public BlobRepository(CloudBlobClient blobClient, string containerName)
        {
            _Reader = new BlobRepositoryReader<TItem>(blobClient, containerName);
            _Writer = new BlobRepositoryWriter<Stream>(blobClient, containerName);
        }

        public IEnumerable<TItem> GetBlobs()
        {
            return this._Reader.GetBlobs();
        }

        public IEnumerable<TItem> GetBlobs(string relativeURI)
        {
            return this._Reader.GetBlobs(relativeURI);
        }

        public IEnumerable<CloudBlobDirectory> GetDirectories()
        {
            return this._Reader.GetDirectories();
        }

        public IEnumerable<CloudBlobDirectory> GetDirectories(string relativeURI)
        {
            return this._Reader.GetDirectories(relativeURI);
        }

        public CloudBlob Write(Stream stream, string blobName)
        {
            return _Writer.Write(stream, blobName);
        }

        public CloudBlob WriteBlock(Stream stream, string blobName, string blockID)
        {
            return _Writer.WriteBlock(stream, blobName, blockID);
        }

        public CloudBlob CommitBlocks(string blobName, IEnumerable<string> blockIDs)
        {
            return _Writer.CommitBlocks(blobName, blockIDs);
        }
    }
}
