

namespace Rolcore.Repository.Tests
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.Repository;
    using Rolcore.Repository.Tests.Mocks;
    using System.Collections.Generic;
    
    /// <summary>
    ///This is a test class for RepositoryTest and is intended
    ///to contain all RepositoryTest Unit Tests
    ///</summary>
    [TestClass, Ignore]
    public abstract class IRepositoryTests<TRepository, TConcurrency>
        where TRepository : IRepository<MockEntity<TConcurrency>, TConcurrency>
    {
        #region Test Setup
        protected abstract IRepository<MockEntity<TConcurrency>, TConcurrency> CreateTargetRepository();
        protected abstract void ClearTestData();

        protected virtual TConcurrency GetDefaultConcurrencyValue()
        {
            return default(TConcurrency);
        }

        [TestInitialize]
        public void IRepositoryTestsInitialize()
        {
            ClearTestData();
        }

        protected static MockEntity<TConcurrency> SaveTestEntity(IRepository<MockEntity<TConcurrency>, TConcurrency> target)
        {
            MockEntity<TConcurrency> result = new MockEntity<TConcurrency>()
            {
                RowKey = Guid.NewGuid().ToString(),
                DateTimeProperty = DateTime.Now,
                IntProperty = (new Random()).Next(),
                PartitionKey = "Mocks",
                StringProperty = string.Empty
            };
            result = target.Save(result)[0];

            return result;
        }
        #endregion Test Setup

        #region Delete() Tests

        [TestMethod]
        public virtual void Delete_DeletesAnEntity()
        {
            var target = CreateTargetRepository();

            var insertedEntity = SaveTestEntity(target);

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

        [TestMethod]
        public virtual void Delete_DeletesMultipleEntities()
        {
            var target = CreateTargetRepository();

            var insertedEntities = new[] { SaveTestEntity(target), SaveTestEntity(target), SaveTestEntity(target) };
            var insertedKeys = insertedEntities.Select(e => e.RowKey).ToArray();

            var retrievedEntities = target
                .IterativeWhere(insertedKeys, 
                    (key, item) => { return item.RowKey == ((string)key); })
                .ToArray();

            Assert.AreEqual(insertedEntities.Length, retrievedEntities.Count());

            var actual = target.Delete(retrievedEntities.ToArray());
            Assert.AreEqual(3, actual, "Should have deleted three items");

            retrievedEntities = target
                .IterativeWhere(insertedKeys, 
                    (key, item) => { return item.RowKey == ((string)key); })
                .ToArray();

            Assert.AreEqual(0, retrievedEntities.Length);
        }

        [TestMethod]
        public virtual void Delete_DeletesByRowKeyAndConcurrency() //TODO: What about testing partition key support?
        {
            var target = CreateTargetRepository();

            var insertedEntity = SaveTestEntity(target);

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

        [TestMethod]
        public virtual void Delete_ReturnsZeroIfRecordDoesNotExist() //TODO: What about testing partition key support?
        {
            var target = CreateTargetRepository();

            var concurrency = GetDefaultConcurrencyValue();
            var actual = target.Delete("nonexistent", concurrency);
            Assert.AreEqual(0, actual);
        }
        #endregion Delete() Tests

        #region Save() Tests
        [TestMethod]
        public virtual void Save_InsertsNewEntity()
        {
            var target = CreateTargetRepository();

            var insertedEntity = SaveTestEntity(target);

            var retrievedEntity = target.Items
                .Where(item =>
                    item.RowKey == insertedEntity.RowKey)
                .Single();

            Assert.IsNotNull(retrievedEntity);
        }

        [TestMethod]
        public virtual void Save_UpdatesExistingEntity()
        {
            var target = CreateTargetRepository();

            var insertedEntity = SaveTestEntity(target);

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

        [TestMethod, ExpectedException(typeof(RepositoryConcurrencyException))]
        public virtual void Save_ThrowsConcurrencyException()
        {
            var target = CreateTargetRepository();
            var conflictTarget = CreateTargetRepository();

            var insertedEntity = SaveTestEntity(target);

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

        [TestMethod]
        public void Save_AppliesRules()
        {
            var target = CreateTargetRepository();
            var rules = new List<MockRepositoryItemRule<MockEntity<TConcurrency>>>();
            rules.Add(new MockRepositoryItemRule<MockEntity<TConcurrency>>());
            target.Rules = rules;
            SaveTestEntity(target);

            Assert.IsTrue(rules[0].ApplyWasCalled, "Apply() was not called");
        }

        [TestMethod]
        public void Save_HandlesKookyPartitionAndRowKeys()
        {
            var target = CreateTargetRepository();
            MockEntity<TConcurrency> testEntity = new MockEntity<TConcurrency>()
            {
                PartitionKey = "aHR0cDovL3d3dy5zcHN1LmVkdS8=|MjAxMy0wNi0xNiAyMDoyNToxOFo=",
                RowKey = "aHR0cDovL2NhbGVuZGFyLnNwc3UuZWR1L2NhbC9ldmVudC9ldmVudFZpZXcuZG8_Yj1kZSZjYWxQYXRoPS9wdWJsaWMvY2Fscy9NYWluQ2FsJmd1aWQ9Q0FMLTI4OWMzMDZmLTNjMjhiMWQ3LTAxM2MtMmFjZTM2YTQtMDAwMDIyNjZkZW1vYmVkZXdvcmtAbXlzaXRlLmVkdSZyZWN1cnJlbmNlSWQ9",
                DateTimeProperty = DateTime.Now,
                IntProperty = (new Random()).Next(),
                StringProperty = string.Empty
            };

            testEntity = target.Insert(testEntity).Single();

            testEntity.DateTimeProperty = DateTime.UtcNow;
            testEntity.StringProperty = "arbitrary modification";

            testEntity = target.Save(testEntity).Single(); // This used to cause an exception in azure even though Update() works!

            Assert.AreEqual("arbitrary modification", testEntity.StringProperty);
        }
        #endregion Save() Tests

        #region Insert() Tests
        [TestMethod]
        public virtual void Insert_Inserts()
        {
            var target = CreateTargetRepository();

            var insertedEntity = new MockEntity<TConcurrency>()
            {
                RowKey = Guid.NewGuid().ToString(),
                DateTimeProperty = DateTime.Now,
                IntProperty = (new Random()).Next(),
                PartitionKey = "Mocks",
                StringProperty = string.Empty
            };

            insertedEntity = target.Insert(insertedEntity).Single();

            var retrievedEntity = target.Items
                .Where(item =>
                    item.PartitionKey == insertedEntity.PartitionKey
                 && item.RowKey == insertedEntity.RowKey)
                .Single();

            Assert.IsNotNull(retrievedEntity);
        }

        [TestMethod, ExpectedException(typeof(RepositoryInsertException))]
        public virtual void Insert_ThrowsExceptionWhenItemAlreadyExists()
        {
            var target = CreateTargetRepository();

            var insertedEntity = new MockEntity<TConcurrency>()
            {
                RowKey = Guid.NewGuid().ToString(),
                DateTimeProperty = DateTime.Now,
                IntProperty = (new Random()).Next(),
                PartitionKey = "Mocks",
                StringProperty = string.Empty
            };

            var alreadyInsertedEntity = target.Insert(insertedEntity).Single().Clone() as MockEntity<TConcurrency>;
            target.Insert(alreadyInsertedEntity);
        }

        [TestMethod]
        public virtual void Insert_InsertsAfterItemAlreadyExistsException()
        {
            var target = CreateTargetRepository();

            var insertedEntity = new MockEntity<TConcurrency>()
            {
                RowKey = Guid.NewGuid().ToString(),
                DateTimeProperty = DateTime.Now,
                IntProperty = (new Random()).Next(),
                PartitionKey = "Mocks",
                StringProperty = string.Empty
            };

            var alreadyInsertedEntity = target.Insert(insertedEntity).Single().Clone() as MockEntity<TConcurrency>;

            var exceptionThrown = false;

            try
            {
                target.Insert(alreadyInsertedEntity);
            }
            catch (RepositoryInsertException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            var newEntityToInsert = new MockEntity<TConcurrency>()
            {
                RowKey = Guid.NewGuid().ToString(),
                DateTimeProperty = DateTime.Now,
                IntProperty = (new Random()).Next(),
                PartitionKey = "Mocks",
                StringProperty = string.Empty
            };

            newEntityToInsert = target.Insert(newEntityToInsert).Single();

            var retrievedNewEntity = target.Items
                .Where(item =>
                    item.PartitionKey == newEntityToInsert.PartitionKey
                 && item.RowKey == newEntityToInsert.RowKey)
                .Single();

            Assert.IsNotNull(retrievedNewEntity);
        }

        [TestMethod]
        public virtual void Insert_SupportsMultipleOperations()
        {
            var target = CreateTargetRepository();

            var retrievedNullEntity = target.Items
                .Where(item =>
                    item.PartitionKey == string.Empty
                 && item.RowKey == string.Empty)
                .SingleOrDefault();

            Assert.IsNull(retrievedNullEntity);

            var insertedEntity = new MockEntity<TConcurrency>()
            {
                RowKey = Guid.NewGuid().ToString(),
                DateTimeProperty = DateTime.Now,
                IntProperty = (new Random()).Next(),
                PartitionKey = "Mocks",
                StringProperty = string.Empty
            };

            insertedEntity = target.Insert(insertedEntity).Single();

            var retrievedNewEntity = target.Items
                .Where(item =>
                    item.PartitionKey == insertedEntity.PartitionKey
                 && item.RowKey == insertedEntity.RowKey)
                .Single();

            Assert.IsNotNull(retrievedNewEntity);
        }

        #endregion Insert() Tests

        #region Update() Tests
        [TestMethod]
        public virtual void Update_Updates()
        {
            var target = CreateTargetRepository();

            var testEntity = SaveTestEntity(target);

            var expectedIntProp = ++testEntity.IntProperty;

            target.Update(testEntity);

            var retrievedEntity = target.Items
                .Where(item =>
                    item.PartitionKey == testEntity.PartitionKey
                 && item.RowKey == testEntity.RowKey)
                .Single();

            Assert.AreEqual(expectedIntProp, retrievedEntity.IntProperty);
        }


        [TestMethod]
        public virtual void Update_UpdatesMany()
        {
            Debug.WriteLine(this.GetType().ToString());
            var target = CreateTargetRepository();

            var testEntity1 = SaveTestEntity(target);
            var testEntity2 = SaveTestEntity(target);
            var testEntity3 = SaveTestEntity(target);

            var retrievedEntity1 = target.Items
                .Where(item =>
                    item.PartitionKey == testEntity1.PartitionKey
                 && item.RowKey == testEntity1.RowKey)
                .Single();
            var retrievedEntity2 = target.Items
                .Where(item =>
                    item.PartitionKey == testEntity2.PartitionKey
                 && item.RowKey == testEntity2.RowKey)
                .Single();
            var retrievedEntity3 = target.Items
                .Where(item =>
                    item.PartitionKey == testEntity3.PartitionKey
                 && item.RowKey == testEntity3.RowKey)
                .Single();

            var expectedIntProp1 = ++retrievedEntity1.IntProperty;
            var expectedIntProp2 = ++retrievedEntity2.IntProperty;
            var expectedIntProp3 = ++retrievedEntity3.IntProperty;

            var target2 = CreateTargetRepository();

            target2.Update(retrievedEntity1, retrievedEntity2, retrievedEntity3);

            retrievedEntity1 = target2.Items
                .Where(item =>
                    item.PartitionKey == testEntity1.PartitionKey
                 && item.RowKey == testEntity1.RowKey)
                .Single();
            retrievedEntity2 = target2.Items
                .Where(item =>
                    item.PartitionKey == testEntity2.PartitionKey
                 && item.RowKey == testEntity2.RowKey)
                .Single();
            retrievedEntity3 = target2.Items
                .Where(item =>
                    item.PartitionKey == testEntity3.PartitionKey
                 && item.RowKey == testEntity3.RowKey)
                .Single();

            Assert.AreEqual(expectedIntProp1, retrievedEntity1.IntProperty);
            Assert.AreEqual(expectedIntProp2, retrievedEntity2.IntProperty);
            Assert.AreEqual(expectedIntProp3, retrievedEntity3.IntProperty);
        }
        #endregion Update() Tests

        #region Items Tests
        [TestMethod]
        public virtual void Items_ContainsAllItems()
        {
            ClearTestData();
            var target = CreateTargetRepository();

            var insertedEntities = new[] { SaveTestEntity(target), SaveTestEntity(target), SaveTestEntity(target) };

            Assert.AreEqual(insertedEntities.Length, target.Items.ToArray().Length);
        }
        #endregion Items Tests
    }
}
