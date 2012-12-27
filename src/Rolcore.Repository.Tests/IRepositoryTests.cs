using Rolcore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Rolcore.Repository.Tests.Mocks;
using System.Linq;
using System.Data;
using System.Diagnostics;

namespace Rolcore.Repository.Tests
{
    
    
    /// <summary>
    ///This is a test class for RepositoryTest and is intended
    ///to contain all RepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public abstract class IRepositoryTests<TRepository, TConcurrency>
        where TRepository : IRepository<MockEntity<TConcurrency>, TConcurrency>
    {
        protected abstract IRepository<MockEntity<TConcurrency>, TConcurrency> CreateTargetRepository();
        protected abstract void ClearTestData();

        protected virtual TConcurrency GetDefaultConcurrencyValue()
        {
            return default(TConcurrency);
        }

        [TestInitialize()]
        public void IRepositoryTestsInitialize()
        {
            ClearTestData();
        }

        protected MockEntity<TConcurrency> InsertTestEntity(IRepository<MockEntity<TConcurrency>, TConcurrency> target)
        {
            MockEntity<TConcurrency> result = new MockEntity<TConcurrency>()
            {
                RowKey = Guid.NewGuid().ToString(),
                DateTimeProperty = DateTime.Now,
                IntProperty = (new Random()).Next(),
                PartitionKey = "Mocks",
                StringProperty = string.Empty
            };
            result = target.Save(result).Single();

            return result;
        }

        #region Delete() Tests

        [TestMethod()]
        public virtual void Delete_DeletesAnEntity()
        {
            var target = CreateTargetRepository();

            var insertedEntity = InsertTestEntity(target);

            var retrievedEntity = target.Items
                .Where(item => 
                    item.RowKey == insertedEntity.RowKey)
                .Single();

            Assert.IsNotNull(retrievedEntity); // Oddly, this is possible even though Single() is 
                                               // supposed to throw an exception if no items are 
                                               // found. It has something to do with contracts, 
                                               // I don't quite have it figured out.

            target.Delete(retrievedEntity);

            retrievedEntity = target.Items
                .Where(item => 
                    item.RowKey == insertedEntity.RowKey)
                .SingleOrDefault();

            Assert.IsNull(retrievedEntity);
        }

        [TestMethod()]
        public virtual void Delete_DeletesMultipleEntities()
        {
            var target = CreateTargetRepository();

            var insertedEntities = new[] { InsertTestEntity(target), InsertTestEntity(target), InsertTestEntity(target) };
            var insertedKeys = insertedEntities.Select(e => e.RowKey).ToArray();

            var retrievedEntities = target.Items
                .Where(item => 
                    insertedKeys.Contains(item.RowKey))
                .ToArray();
            Assert.AreEqual(insertedEntities.Length, retrievedEntities.Length);

            var actual = target.Delete(retrievedEntities);

            Assert.AreEqual(3, actual); // Should have deleted three items

            retrievedEntities = target.Items
                .Where(item => 
                    insertedKeys.Contains(item.RowKey))
                .ToArray();
            Assert.AreEqual(0, retrievedEntities.Length);
        }

        [TestMethod()]
        public virtual void Delete_DeletesByRowKeyAndConcurrency() //TODO: What about testing partition key support?
        {
            var target = CreateTargetRepository();

            var insertedEntity = InsertTestEntity(target);

            var retrievedEntity = target.Items
                .Where(item => 
                    item.RowKey == insertedEntity.RowKey)
                .Single();
            Assert.IsNotNull(retrievedEntity);

            var deleteCount = target.Delete(retrievedEntity.RowKey, retrievedEntity.Timestamp, retrievedEntity.PartitionKey);

            Assert.AreEqual(1, deleteCount);

            retrievedEntity = target.Items
                .Where(item => 
                    item.RowKey == insertedEntity.RowKey)
                .SingleOrDefault();

            Assert.IsNull(retrievedEntity);
        }

        [TestMethod()]
        public virtual void Delete_ReturnsZeroIfRecordDoesNotExist() //TODO: What about testing partition key support?
        {
            var target = CreateTargetRepository();

            var concurrency = GetDefaultConcurrencyValue();
            var actual = target.Delete("nonexistent", concurrency);
            Assert.AreEqual(0, actual);
        }
        #endregion Delete() Tests

        #region Save() Tests
        [TestMethod()]
        public virtual void Save_InsertsNewEntity()
        {
            var target = CreateTargetRepository();

            var insertedEntity = InsertTestEntity(target);

            var retrievedEntity = target.Items
                .Where(item =>
                    item.RowKey == insertedEntity.RowKey)
                .Single();

            Assert.IsNotNull(retrievedEntity);
        }

        [TestMethod()]
        public virtual void Save_UpdatesExistingEntity()
        {
            var target = CreateTargetRepository();

            var insertedEntity = InsertTestEntity(target);

            var retrievedEntity = target.Items
                .Where(item =>
                    item.RowKey == insertedEntity.RowKey)
                .Single();

            Assert.IsNotNull(retrievedEntity);

            var savedIntValue = retrievedEntity.IntProperty;
            var expectedIntValue = savedIntValue + (new Random()).Next();
            Assert.AreNotEqual(savedIntValue, expectedIntValue);

            retrievedEntity.IntProperty = expectedIntValue;

            retrievedEntity = target.Save(retrievedEntity).Single();
            
            Assert.AreEqual(expectedIntValue, retrievedEntity.IntProperty);
        }

        [TestMethod(), ExpectedException(typeof(DBConcurrencyException))]
        public virtual void Save_ThrowsConcurrencyException()
        {
            var target = CreateTargetRepository();
            var conflictTarget = CreateTargetRepository();

            var insertedEntity = InsertTestEntity(target);

            var retrievedEntity1 = target.Items
                .Where(item =>
                    item.RowKey == insertedEntity.RowKey)
                .Single();
            var retrievedEntity2 = conflictTarget.Items
                .Where(item =>
                    item.RowKey == insertedEntity.RowKey)
                .Single();

            retrievedEntity1.StringProperty = "Save should succeed";
            retrievedEntity2.StringProperty = "Save should fail";

            Assert.AreNotEqual(retrievedEntity1.StringProperty, retrievedEntity2.StringProperty);

            Debug.WriteLine("Saving retrievedEntity1...");
            int savedCount = target.Save(retrievedEntity1).Count();
            Assert.AreEqual(1, savedCount);
            Debug.WriteLine("retrievedEntity1 Saved");

            Debug.WriteLine("Saving retrievedEntity2...");
            savedCount = conflictTarget.Save(retrievedEntity2).Count(); // Exception!
            Assert.AreEqual(1, savedCount);
            Debug.WriteLine("retrievedEntity2 Saved");
        }
        #endregion Save() Tests

        #region Items Tests
        [TestMethod()]
        public virtual void Items_ContainsAllItems()
        {
            ClearTestData();
            var target = CreateTargetRepository();

            var insertedEntities = new[] { InsertTestEntity(target), InsertTestEntity(target), InsertTestEntity(target) };

            Assert.AreEqual(insertedEntities.Length, target.Items.ToArray().Length);
        }
        #endregion Items Tests
    }
}
