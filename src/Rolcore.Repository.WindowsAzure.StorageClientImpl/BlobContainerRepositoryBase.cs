using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace Rolcore.Repository.WindowsAzure.StorageClientImpl
{
    public abstract class BlobContainerRepositoryBase
    {
        protected CloudBlobContainer Container { get; private set; }

        #region Constructors
        protected BlobContainerRepositoryBase(CloudStorageAccount storageAccount, string containerName)
            : this(storageAccount.CreateCloudBlobClient(), containerName)
        {
        }

        protected BlobContainerRepositoryBase(string connectionString, string containerName)
            : this(CloudStorageAccount.Parse(connectionString), containerName)
        {
        }

        protected BlobContainerRepositoryBase(CloudBlobClient blobClient, string containerName)
        {
            Container = blobClient.GetContainerReference(containerName);

            //create the container if it doesn't already exist
            Container.CreateIfNotExist();

        }
        #endregion
    }
}


