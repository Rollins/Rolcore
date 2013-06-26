//-----------------------------------------------------------------------
// <copyright file="DateRangeTest.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rollins.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore;
    
    /// <summary>
    /// This is a test class for DateRangeTest and is intended to contain all DateRangeTest Unit 
    /// Tests.
    /// </summary>
    [TestClass]
    public class DateRangeTest
    {
        #region Constructor Tests
        /// <summary>
        /// A test for DateRange default constructor.
        ///</summary>
        [TestMethod]
        public void ConstructorDefault_ConstructsInfiniteDateRange()
        {
            var target = new DateRange(); // Defaults to infinite range.
            Assert.IsTrue(target.Includes(DateTime.MinValue));
            Assert.IsTrue(target.Includes(DateTime.MaxValue));
        }

        /// <summary>
        ///A test for DateRange(DateTime, DateTime) Constructor
        ///</summary>
        [TestMethod]
        public void Constructor_SetsStartDateAndEndDateValues()
        {
            DateTime? startDate = DateTime.Today;
            DateTime? endDate = DateTime.Today.AddDays(1);
            var target = new DateRange(startDate, endDate);
            Assert.AreEqual(startDate, target.StartDate);
            Assert.AreEqual(endDate, target.EndDate);
        }
        #endregion Constructor Tests

        #region Equals() Tests
        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void Equals_ReturnsTrueForInifiniteDateRanges()
        {
            var target = new DateRange();
            var other = new DateRange();

            Assert.AreEqual(true, target.Equals(other));
        }

        [TestMethod]
        public void Equals_ReturnsTrueForEquivelantStartAndEndDates()
        {
            var start = DateTime.Today;
            var end = DateTime.Now;
            var target = new DateRange(start, end);
            var other = new DateRange(start, end);

            Assert.AreEqual(true, target.Equals(other));
        }

        [TestMethod]
        public void Equals_ReturnsTrueForEquivelantStartAndNullEndDates()
        {
            var start = DateTime.Today;
            var target = new DateRange(start, null);
            var other = new DateRange(start, null);

            Assert.AreEqual(true, target.Equals(other));
        }

        [TestMethod]
        public void Equals_ReturnsTrueForNullStartAndEquivelantEndDates()
        {
            var end = DateTime.Today;
            var target = new DateRange(null, end);
            var other = new DateRange(null, end);

            Assert.AreEqual(true, target.Equals(other));
        }

        [TestMethod]
        public void Equals_ReturnsFalseForEquivelantStartAndDifferentEndDates()
        {
            var start = DateTime.Today;
            var end1 = start.AddMilliseconds(1);
            var end2 = start.AddMilliseconds(2);
            var target = new DateRange(start, end1);
            var other = new DateRange(start, end2);

            Assert.AreEqual(false, target.Equals(other));
        }

        [TestMethod]
        public void Equals_ReturnsFalseForDifferentStartAndEquivelantEndDates()
        {
            var start1 = DateTime.Today;
            var start2 = DateTime.Today.AddMilliseconds(1);
            var end = DateTime.Now;
            var target = new DateRange(start1, end);
            var other = new DateRange(start2, end);

            Assert.AreEqual(false, target.Equals(other));
        }
        #endregion Equals() Tests

        #region Includes() Tests
        [TestMethod]
        public void Includes_IsTrueForDateBetweenStartAndEndDates()
        {
            var target = new DateRange(DateTime.Today, DateTime.Today.AddDays(1));
            Assert.IsTrue(target.Includes(DateTime.Today.AddHours(12)));
        }

        [TestMethod]
        public void Includes_IsAlwaysTrueForInfiniteRange()
        {
            var target = new DateRange();
            Assert.IsTrue(target.Includes(DateTime.Today));
            Assert.IsTrue(target.Includes(DateTime.MinValue));
            Assert.IsTrue(target.Includes(DateTime.MaxValue));
        }

        [TestMethod]
        public void Includes_IsTrueForStartDate()
        {
            var target = new DateRange(DateTime.Today, DateTime.Now);
            Assert.IsTrue(target.Includes(DateTime.Today));
        }

        [TestMethod]
        public void Includes_IsTrueForEndDate()
        {
            var now = DateTime.Now;
            var target = new DateRange(DateTime.Today, now);
            Assert.IsTrue(target.Includes(now));
        }

        [TestMethod]
        public void Includes_IsFalseForDateAfterEndDate()
        {
            var now = DateTime.Now;
            var target = new DateRange(DateTime.Today, now);
            Assert.IsFalse(target.Includes(now.AddMilliseconds(1)));
        }

        [TestMethod]
        public void Includes_IsFalseForDateBeforeEndDate()
        {
            var target = new DateRange(DateTime.Today, DateTime.Now);
            Assert.IsFalse(target.Includes(DateTime.Today.AddMilliseconds(-1)));
        }
        #endregion Includes() Tests

        #region TimeSpan Tests

        [TestMethod]
        public void TimeSpan_IsTimeSpanOfDateRange()
        {
            var now = DateTime.Now;
            var target = new DateRange(now, now.AddDays(1));
            Assert.AreEqual(Milliseconds.PerDay, target.TimeSpan.Value.TotalMilliseconds);
        }

        [TestMethod]
        public void TimeSpan_IsNullForInfiniteDateRange()
        {
            var target = new DateRange();
            Assert.AreEqual(null, target.TimeSpan);
        }

        [TestMethod]
        public void TimeSpan_IsNullForInfiniteDateRangeWithFiniteStart()
        {
            var target = new DateRange(DateTime.Now, null);
            Assert.AreEqual(null, target.TimeSpan);
        }

        [TestMethod]
        public void TimeSpan_IsNullForInfiniteDateRangeWithFiniteEnd()
        {
            var target = new DateRange(DateTime.Now, null);
            Assert.AreEqual(null, target.TimeSpan);
        }

        [TestMethod]
        public void TimeSpan_IsZeroForInstantDateRange()
        {
            var now = DateTime.Now;
            var target = new DateRange(now, now);
            Assert.AreEqual(0, target.TimeSpan.Value.Milliseconds);
        }
        #endregion TimeSpan Tests

        /// <summary>
        ///A test for GetIntersection
        ///</summary>
        [TestMethod]
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
        [TestMethod]
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
        [TestMethod]
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
        [TestMethod]
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
        ///A test for Now
        ///</summary>
        [TestMethod]
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
    }
}
