﻿using System;
using System.ComponentModel.Composition;
using System.Data.Linq;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Rolcore.Repository.LinqImpl
{
    public class LinqRepositoryReader<TDataContext, TItem, TBase>
        : LinqRepositoryBase<TDataContext, TItem, TBase>,
          IRepositoryReader<TBase>
        where TDataContext : DataContext
        where TBase : class
        where TItem : class, TBase
    {
        protected Lazy<Table<TItem>> Table { get; private set; }

        public LinqRepositoryReader() : base()
        {
            
        }

        [ImportingConstructor]
        public LinqRepositoryReader(Func<TDataContext> dataContextFactory)
            : base(dataContextFactory)
        {
            Contract.Requires<ArgumentNullException>(dataContextFactory != null, "dataContextFactory is null");
            Contract.Ensures(dataContextFactory != null, "dataContextFactory is null");
        }

        public IQueryable<TBase> Items()
        {
            return GetTable(this.CreateDataContext());
        }
    }
}
