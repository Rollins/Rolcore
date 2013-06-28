using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Diagnostics.Contracts;
using System.Collections;
using System.ComponentModel.Composition;

namespace Rolcore.Repository.LinqImpl
{
    public class LinqRepositoryReader<TDataContext, TItem, TBase>
        : LinqRepositoryBase<TDataContext, TItem, TBase>,
          IRepositoryReader<TBase>
        where TDataContext : DataContext
        where TBase : class
        where TItem : class, TBase
    {
        private void SetupTable()
        {
            this.Table = new Lazy<Table<TItem>>(() =>
            {
                return GetTable(this.CreateDataContext());
            });
        }

        protected Lazy<Table<TItem>> Table { get; private set; }

        public LinqRepositoryReader() : base()
        {
            this.SetupTable();
        }

        [ImportingConstructor]
        public LinqRepositoryReader(Func<TDataContext> dataContextFactory)
            : base(dataContextFactory)
        {
            Contract.Requires<ArgumentNullException>(dataContextFactory != null, "dataContextFactory is null");
            Contract.Ensures(dataContextFactory != null, "dataContextFactory is null");
            this.SetupTable();
        }

        public IQueryable<TBase> Items
        {
            get { return this.Table.Value; }
        }
    }
}
