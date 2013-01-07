using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rolcore.Repository.Tests.Mocks
{
    public class MockRepositoryItemRule<TItem> : IRepositoryItemRule<TItem>
        where TItem : class
    {
        public MockRepositoryItemRule()
        {
            ApplyWasCalled = false;
        }

        public void Apply(TItem item)
        {
            Debug.WriteLine(
                string.Format("Rule {0} applied to item {1}", this, item));
            ApplyWasCalled = true;
        }

        public bool ApplyWasCalled
        {
            get;
            private set;
        }
    }
}
