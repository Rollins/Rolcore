using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Common;
using System.Runtime.Serialization;

namespace Rolcore.Repository.Tests.Mocks
{
    [DataServiceKey("PartitionKey", "RowKey")]
    public class MockEntity<TConcurrency>
    {
        public virtual string RowKey { get; set; }
        public virtual string PartitionKey { get; set; }
        public virtual TConcurrency Timestamp { get; set; }

        public virtual string StringProperty { get; set; }
        public virtual DateTime DateTimeProperty { get; set; }
        public virtual int IntProperty { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", PartitionKey, RowKey, Timestamp);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
                return true;

            var item = obj as MockEntity<TConcurrency>;
            if (item == null)
                return false;

            return (item.PartitionKey == PartitionKey)
                && (item.RowKey == RowKey)
                && (item.Timestamp.Equals(Timestamp));
        }

        public override int GetHashCode()
        {
            return (PartitionKey ?? string.Empty).GetHashCode()
                + (RowKey ?? string.Empty).GetHashCode()
                + ((Timestamp == null) ? string.Empty.GetHashCode() : Timestamp.GetHashCode());
        }
    }
}
