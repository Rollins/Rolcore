﻿using System;
using System.Data.Linq;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rolcore.Repository.Tests.Mocks;

namespace Rolcore.Repository.Tests.Linq
{
    [TestClass]
    public class LinqRepositoryTest : IRepositoryTests<IRepository<MockEntity<Binary>, Binary>, Binary>
    {

        protected override IRepository<MockEntity<Binary>, Binary> CreateTargetRepository()
        {
            var context = new TestRepository();
            
            return context;
        }

        protected override IRepository<MockDetailEntity<Binary>, Binary> CreateDetailTargetRepository()
        {
            var context = new TestDetailRepository();

            return context;
        }

        protected override void ClearTestData()
        {
            using (var context = new TestDataContext())
            {
                if (context.DatabaseExists())
                {
                    try
                    {
                        context.DeleteDatabase(); 
                    }
                    catch
                    {
                        var allTestItems = context.TestItems.ToList();
                        context.TestItems.DeleteAllOnSubmit(allTestItems);
                        context.SubmitChanges();
                    }
                }
            }
        }

        protected override Binary GetDefaultConcurrencyValue()
        {
            return new Binary(new Byte[] { 0 });
        }
    }
}
