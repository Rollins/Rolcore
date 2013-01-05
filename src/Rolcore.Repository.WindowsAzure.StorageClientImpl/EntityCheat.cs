using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;
using System.Data.Services.Client;
using System.Data;
using System.Diagnostics;
using Microsoft.WindowsAzure;

namespace Rolcore.Repository.WindowsAzure.StorageClientImpl
{
    public class EntityCheat : TableServiceEntity
    {
        public EntityCheat(string partitionKey, string rowKey)
            : base(partitionKey, rowKey)
        {

        }

        public EntityCheat()
        {

        }

    }
}
