//-----------------------------------------------------------------------
// <copyright file="TestDataContext.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Repository.Tests.Linq
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Data.Linq;
    using System.Linq;
    using Rolcore.Repository.LinqImpl;
    using Rolcore.Repository.Tests.Mocks;

    public partial class TestItem : MockEntity<Binary>
    {
        partial void OnValidate(ChangeAction action)
        {
            if (action == ChangeAction.Insert && RowKey == null)
                RowKey = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return string.Format("RowKey: {0} PartitionKey: {1}", this.RowKey, this.PartitionKey);
        }
    }
}
