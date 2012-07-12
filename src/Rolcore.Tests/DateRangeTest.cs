using Rollins;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Rolcore;

namespace Rollins.Tests
{
    
    
    /// <summary>
    ///This is a test class for DateRangeTest and is intended
    ///to contain all DateRangeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DateRangeTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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


        /// <summary>
        /// A test for DateRange default constructor.
        ///</summary>
        [TestMethod()]
        public void DateRangeDefaultConstructorTest()
        {
            DateRange target = new DateRange(); // Defaults to infinite range.
            Assert.IsTrue(target.Includes(DateTime.MinValue));
            Assert.IsTrue(target.Includes(DateTime.MaxValue));
        }

        /// <summary>
        ///A test for DateRange(DateTime, DateTime) Constructor
        ///</summary>
        [TestMethod()]
        public void DateRangeConstructorTest()
        {
            Nullable<DateTime> startDate = DateTime.Today;
            Nullable<DateTime> endDate = DateTime.Today.AddDays(1);
            DateRange target = new DateRange(startDate, endDate);
            Assert.AreEqual(startDate, target.StartDate);
            Assert.AreEqual(endDate, target.EndDate);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            DateRange target = new DateRange();
            DateRange other = new DateRange();

            bool expected = true;
            bool actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new DateRange(DateTime.Today, DateTime.Now);
            actual = target.Equals(other);
            expected = false;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetIntersection
        ///</summary>
        [TestMethod()]
        public void GetIntersectionTest()
        {
            var target = new DateRange(DateTime.Today.AddDays(-1), DateTime.Today);
            var otherTarget = new DateRange(DateTime.Today, DateTime.Today.AddDays(1));
            var expectedIntersection = new DateRange(DateTime.Today, DateTime.Today);

            Assert.IsTrue(expectedIntersection.Equals(target.GetIntersection(otherTarget)));
        }

        /// <summary>
        ///A test for Intersects
        ///</summary>
        [TestMethod()]
        public void IntersectsTest()
        {
            var target = new DateRange();
            var otherTarget = new DateRange(DateTime.MinValue, DateTime.MaxValue);

            Assert.IsTrue(otherTarget.Intersects(target));
            Assert.IsTrue(DateRange.Now.Intersects(target));
            Assert.IsTrue(DateRange.Today.Intersects(target));

            target = DateRange.Today;
            Assert.IsTrue(DateRange.Now.Intersects(target));

            target = new DateRange(DateTime.Today.AddDays(-1), DateTime.Today);
            otherTarget = new DateRange(DateTime.Today, DateTime.Today.AddDays(1));

            Assert.IsTrue(otherTarget.Intersects(target)); // juuuust touches
        }

        /// <summary>
        ///A test for OccursWithin
        ///</summary>
        [TestMethod()]
        public void OccursWithinTest()
        {
            var target = new DateRange();
            var otherTarget = new DateRange(DateTime.MinValue, DateTime.MaxValue);

            Assert.IsTrue(otherTarget.OccursWithin(target));
            Assert.IsTrue(DateRange.Now.OccursWithin(target));
            Assert.IsTrue(DateRange.Today.OccursWithin(target));

            target = DateRange.Today;
            Assert.IsTrue(DateRange.Now.OccursWithin(target));

            target = new DateRange(DateTime.Today.AddDays(-1), DateTime.Today);
            otherTarget = new DateRange(DateTime.Today, DateTime.Today.AddDays(1));
            Assert.IsFalse(otherTarget.OccursWithin(target));
        }

        /// <summary>
        /// A test for Today
        ///</summary>
        [TestMethod()]
        public void TodayTest()
        {
            DateRange actual = DateRange.Today;
            Assert.AreEqual(0, actual.StartDate.Value.Hour);
            Assert.AreEqual(0, actual.StartDate.Value.Minute);
            Assert.AreEqual(0, actual.StartDate.Value.Second);
            Assert.AreEqual(0, actual.StartDate.Value.Millisecond);
            
            Assert.AreEqual(Milliseconds.PerDay, actual.TimeSpan.Value.TotalMilliseconds);
        }

        /// <summary>
        /// A test for TimeSpan
        ///</summary>
        [TestMethod()]
        public void TimeSpanTest()
        {
            // Infinite \\
            DateRange target = new DateRange();
            Nullable<TimeSpan> actual = target.TimeSpan;
            Assert.AreEqual(null, actual);

            target = new DateRange(DateTime.Now, null);
            actual = target.TimeSpan;
            Assert.AreEqual(null, actual);

            target = new DateRange(null, DateTime.Now);
            actual = target.TimeSpan;
            Assert.AreEqual(null, actual);

            DateTime now = DateTime.Now;

            // Instant \\
            target = new DateRange(now, now);
            actual = target.TimeSpan;
            Assert.AreEqual(0, actual.Value.TotalMilliseconds);

            // One Second \\
            target = new DateRange(now, now.AddSeconds(1));
            actual = target.TimeSpan;
            Assert.AreEqual(1000, actual.Value.TotalMilliseconds);

            target = new DateRange(now.AddSeconds(-1), now);
            actual = target.TimeSpan;
            Assert.AreEqual(1000, actual.Value.TotalMilliseconds);

            // One Day \\
            target = new DateRange(now, now.AddDays(1));
            actual = target.TimeSpan;
            Assert.AreEqual(86400000, actual.Value.TotalMilliseconds);

            target = new DateRange(now.AddDays(-1), now);
            actual = target.TimeSpan;
            Assert.AreEqual(86400000, actual.Value.TotalMilliseconds);
        }

        /// <summary>
        ///A test for StartDate
        ///</summary>
        [TestMethod()]
        public void StartDateTest()
        {
            DateRange target = new DateRange();
            Nullable<DateTime> expected = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> actual;
            target.StartDate = expected;
            actual = target.StartDate;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Now
        ///</summary>
        [TestMethod()]
        public void NowTest()
        {
            DateTime beforeNow = DateTime.Now.AddSeconds(-1);
            DateRange actual = DateRange.Now;
            Assert.AreEqual(actual.StartDate, actual.EndDate);
            Assert.IsNotNull(actual.StartDate);
            Assert.IsNotNull(actual.EndDate);
            Assert.IsTrue(actual.StartDate > beforeNow, "Start Date too small");
            Assert.IsTrue(actual.EndDate > beforeNow, "End Date too small");
            Assert.IsTrue(actual.StartDate < DateTime.Now.AddSeconds(1), "Start Date too large");
            Assert.IsTrue(actual.EndDate < DateTime.Now.AddSeconds(1), "End Date too large");
        }

        /// <summary>
        ///A test for EndDate
        ///</summary>
        [TestMethod()]
        public void EndDateTest()
        {
            DateRange target = new DateRange(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> expected = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> actual;
            target.EndDate = expected;
            actual = target.EndDate;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
