using System;
using System.Data.Services.Common;

namespace Rolcore.Repository.Tests.Mocks
{
    [DataServiceKey("RowKey", "DetailProperty")]
    public class MockDetailEntity<TConcurrency> : ICloneable
    {
        public virtual string RowKey { get; set; }
        public virtual string PartitionKey { get; set; }
        public virtual TConcurrency Timestamp { get; set; }

        public virtual string DetailProperty { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3}", PartitionKey, RowKey, DetailProperty, Timestamp);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
                return true;

            var item = obj as MockDetailEntity<TConcurrency>;
            if (item == null)
                return false;

            return (item.PartitionKey == PartitionKey)
                && (item.DetailProperty == DetailProperty)
                && (item.RowKey == RowKey)
                && (item.Timestamp.Equals(Timestamp));
        }

        public override int GetHashCode()
        {
            return (PartitionKey ?? string.Empty).GetHashCode()
                + (DetailProperty ?? string.Empty).GetHashCode()
                + (RowKey ?? string.Empty).GetHashCode()
                + ((Timestamp == null) ? string.Empty.GetHashCode() : Timestamp.GetHashCode());
        }

        public object Clone()
        {
            return new MockDetailEntity<TConcurrency>()
            {
                RowKey = this.RowKey,
                PartitionKey = this.PartitionKey,
                DetailProperty = this.DetailProperty,
                Timestamp = this.Timestamp
            };
        }
    }
}
