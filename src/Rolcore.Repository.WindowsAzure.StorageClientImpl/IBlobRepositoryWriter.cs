using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.WindowsAzure.StorageClient;

namespace Rolcore.Repository.WindowsAzure.StorageClientImpl
{
    public interface IBlobRepositoryWriter<T> where T: Stream
    {
        CloudBlob Write(T stream, string blobName);
        CloudBlob WriteBlock(T stream, string blobName, string blockID);
        CloudBlob CommitBlocks(string blobName, IEnumerable<string> blockIDs);
    }
}
