namespace Rolcore.Repository.LinqImpl
{
    using System;
    using System.ComponentModel.Composition;
    using System.Data.Linq;
    using System.Diagnostics.Contracts;
    using Rolcore.Diagnostics;
    using Rolcore.Reflection;

    public class LinqRepositoryBase<TDataContext, TItem, TBase>
        where TDataContext : DataContext
        where TBase : class
        where TItem : class, TBase
    {
        private readonly Func<TDataContext> dataContextFactory;

        protected static Table<TItem> GetTable(TDataContext context)
        {
            var result = context.GetTable<TItem>();
            if (result == null)
            {
                throw new InvalidOperationException(string.Format(
                    "DdtaContext does not contain a table of type {0}",
                    typeof(TItem)));
            }

            return result;
        }

        protected TDataContext CreateDataContext()
        {
            var result = dataContextFactory();

            if (!result.DatabaseExists())
            {
                result.CreateDatabase();
            }

            return result;
        }

        public LinqRepositoryBase()
            : this(() => { return Activator.CreateInstance<TDataContext>(); })
        {
            Contract.Ensures(this.dataContextFactory != null, "dataContextFactory is null");
        }

        [ImportingConstructor]
        public LinqRepositoryBase(Func<TDataContext> dataContextFactory)
        {
            Contract.Requires<ArgumentNullException>(dataContextFactory != null, "dataContextFactory is null");
            Contract.Ensures(this.dataContextFactory == dataContextFactory, "DataContextFactory is null");

            this.dataContextFactory = dataContextFactory;
        }
    }
}
