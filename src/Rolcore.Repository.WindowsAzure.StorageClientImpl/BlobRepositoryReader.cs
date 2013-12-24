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
    public class BlobRepositoryReader<TItem> : BlobContainerRepositoryBase, IBlobRepositoryReader<TItem> where TItem : CloudBlob
    {
        public BlobRepositoryReader(CloudStorageAccount storageAccount, string containerName)
            : base(storageAccount, containerName)
        { 
        }

        public BlobRepositoryReader(CloudBlobClient blobClient, string containerName)
            : base(blobClient, containerName)
        { 
        }

        public BlobRepositoryReader(string connectionString, string containerName)
            : base(connectionString, containerName)
        {
 
        }

        public IEnumerable<TItem> GetBlobs()
        {
            return GetBlobs(string.Empty);
        }

        public IEnumerable<TItem> GetBlobs(string relativeURI)
        {
            List<TItem> cloudBlobItems = new List<TItem>();
            IEnumerable<IListBlobItem> blobItems = GetBlobItems(relativeURI);

            foreach (IListBlobItem item in blobItems)
            {
                if (item.GetType() != typeof(CloudBlobDirectory))
                {
                    cloudBlobItems.Add((TItem)item);
                }
            }
            return cloudBlobItems;
        }


        public IEnumerable<CloudBlobDirectory> GetDirectories()
        {
            return GetDirectories(string.Empty);
        }

        public IEnumerable<CloudBlobDirectory> GetDirectories(string relativeURI)
        {
            List<CloudBlobDirectory> cloudBlobDirectories = new List<CloudBlobDirectory>();
            IEnumerable<IListBlobItem> blobItems = GetBlobItems(relativeURI);

            foreach (IListBlobItem item in blobItems)
            {
                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    cloudBlobDirectories.Add((CloudBlobDirectory)item);
                }
            }
            return cloudBlobDirectories;
        }

        private IEnumerable<IListBlobItem> GetBlobItems(string relativeURI)
        {
            IEnumerable<IListBlobItem> blobItems = null;

            if (relativeURI == string.Empty)
            {
                CloudBlobDirectory directory = Container.GetDirectoryReference(relativeURI);
                blobItems = directory.ListBlobs();
            }
            else
            {
                blobItems = Container.ListBlobs();
            }

            return blobItems;
        }



        public TItem GetBlob(string relativePathToContainer)
        {
            return Container.GetBlobReference(relativePathToContainer) as TItem;
        }
    }
}



