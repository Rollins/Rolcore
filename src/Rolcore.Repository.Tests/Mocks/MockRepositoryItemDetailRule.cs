using System.Diagnostics;

namespace Rolcore.Repository.Tests.Mocks
{
    public class MockRepositoryItemDetailRule<TItem> : IRepositoryItemRule<TItem>
        where TItem : class
    {
        public MockRepositoryItemDetailRule()
        {
            ApplyWasCalled = false;
        }

        public void Apply(TItem item)
        {
            Debug.WriteLine(
                string.Format("Rule {0} applied to detail item {1}", this, item));
            ApplyWasCalled = true;
        }

        public bool ApplyWasCalled
        {
            get;
            private set;
        }
    }
}
