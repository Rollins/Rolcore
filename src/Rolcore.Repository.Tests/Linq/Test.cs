//-----------------------------------------------------------------------
// <copyright file="TestDataContext.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Repository.Tests.Linq
{
    using System;
    using System.Data.Linq;
    using Rolcore.Diagnostics;
    using Rolcore.Repository.Tests.Mocks;

    partial class TestDataContext
    {
        partial void OnCreated()
        {
#if (DEBUG)
            this.Log = new DebuggerWriter();
#endif
        }
    }
    public partial class TestItem : MockEntity<Binary>
    {
        
        partial void OnValidate(ChangeAction action)
        {
            if (action == ChangeAction.Insert && RowKey == null)
            {
                RowKey = Guid.NewGuid().ToString();
            }
        }

        public override string ToString()
        {
            return string.Format("RowKey: {0} PartitionKey: {1}", this.RowKey, this.PartitionKey);
        }
    }

    public partial class TestItemDetail : MockDetailEntity<Binary>
    {
        partial void OnValidate(ChangeAction action)
        {
            if (action == ChangeAction.Insert && RowKey == null)
            {
                RowKey = Guid.NewGuid().ToString();

            }
        }

        public override string ToString()
        {
            return string.Format("RowKey: {0} PartitionKey: {1} DetailProperty: {2}", this.RowKey, this.PartitionKey, this.DetailProperty);
        }
    }
}
