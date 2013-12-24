using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.StorageClient;

namespace Rolcore.Repository.WindowsAzure.StorageClientImpl
{
    public interface IBlobRepositoryReader<TItem> where TItem: CloudBlob
    {
        IEnumerable<TItem> GetBlobs();
        IEnumerable<TItem> GetBlobs(string relativeURI);
        TItem GetBlob(string relativePathToContainer);

        IEnumerable<CloudBlobDirectory> GetDirectories(); 
        IEnumerable<CloudBlobDirectory> GetDirectories(string relativeURI);
        

    }
}
