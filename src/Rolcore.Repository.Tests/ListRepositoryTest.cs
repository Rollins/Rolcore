using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rolcore.Repository.ListImpl;
using Rolcore.Repository.Tests.Mocks;
using System.Collections.Generic;
using System.Linq;

namespace Rolcore.Repository.Tests
{
    /// <summary>
    ///This is a test class for ListRepositoryTest and is intended
    ///to contain all ListRepositoryTest Unit Tests
    ///</summary>
    [TestClass]
    public class ListRepositoryTest : IRepositoryTests<IRepository<MockEntity<int>, int>, int>
    {
        private readonly List<MockEntity<int>> _List = new List<MockEntity<int>>();
        private readonly List<MockDetailEntity<int>> _DetailList = new List<MockDetailEntity<int>>();

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        protected override IRepository<MockEntity<int>, int> CreateTargetRepository()
        {
            return new ListRepository<MockEntity<int>, int>(
                _List,
                // setConcurrency
                (item, timestamp) =>
                {
                    item.Timestamp = timestamp;
                },
                // generateKey
                (item) =>
                {
                    // Do nothing, key is generated in database
                },
                // findByItemIdent
                (item, list) =>
                {
                    return list
                        .Where(lItem =>
                            lItem.PartitionKey == item.PartitionKey
                         && lItem.RowKey == item.RowKey)
                        .SingleOrDefault();
                },
                // findConcurrentlyByItem
                (item, list) =>
                {
                    return list
                        .Where(lItem =>
                            lItem.PartitionKey == item.PartitionKey
                         && lItem.RowKey == item.RowKey
                         && lItem.Timestamp == item.Timestamp)
                        .SingleOrDefault();
                },
                // findConcurrently
                (rowKey, concurrency, partitionKey, list) =>
                {
                    return list
                        .Where(lItem =>
                            lItem.PartitionKey == partitionKey
                         && lItem.RowKey == rowKey
                         && lItem.Timestamp == concurrency)
                        .SingleOrDefault();
                },
                // newConcurrencyValue
                () =>
                {
                    return ThreadSafeRandom.Next(int.MaxValue);
                },
                false);
        }
        protected override IRepository<MockDetailEntity<int>, int> CreateDetailTargetRepository()
        {
            return new ListRepository<MockDetailEntity<int>, int>(
                _DetailList,
                // setConcurrency
                (item, timestamp) =>
                {
                    item.Timestamp = timestamp;
                },
                // generateKey
                (item) =>
                {
                    // Do nothing, key is generated in database
                },
                // findByItemIdent
                (item, list) =>
                {
                    return list
                        .Where(lItem =>
                            lItem.PartitionKey == item.PartitionKey
                         && lItem.RowKey == item.RowKey
                         && lItem.DetailProperty == item.DetailProperty)
                        .SingleOrDefault();
                },
                // findConcurrentlyByItem
                (item, list) =>
                {
                    return list
                        .Where(lItem =>
                            lItem.RowKey == item.RowKey
                         && lItem.DetailProperty == item.DetailProperty
                         && lItem.Timestamp == item.Timestamp)
                        .SingleOrDefault();
                },
                // findConcurrently
                (rowKey, concurrency, detailProperty, list) =>
                {
                    return list
                        .Where(lItem =>
                            lItem.RowKey == rowKey
                         && lItem.DetailProperty == detailProperty
                         && lItem.Timestamp == concurrency)
                        .SingleOrDefault();
                },
                // newConcurrencyValue
                () =>
                {
                    return ThreadSafeRandom.Next(int.MaxValue);
                },
                false);
        }

        protected override void ClearTestData()
        {
            _DetailList.Clear();
            _List.Clear();
        }
    }
}
