/*
 * Code by Jason Hunt, found at
 * http://www.noticeablydifferent.com/CodeSamples/DateRange.aspx 
 * 
 * Modified by Rollins, Inc.
 */

using System;

namespace Rolcore
{
    /// <summary>
    /// This class handles all ranges attainable with the DateTime structure. The 
    /// <see cref="StartDate"/> date must precede the <see cref="EndDate"/>. The Start 
    /// and End dates can be null (infinite). When start date is null, it is equivalent 
    /// to negative infinity. When end date is null, it is equivalent to positive 
    /// infinity. Given two date ranges, it is able to calculate whether the ranges 
    /// intersect and what the range of intersection is between the two ranges. Unlike 
    /// the <see cref="TimeSpan"/> structure, this class retains the start and end date 
    /// components of the range (hence the choice for the term "Range" instead of "Span").
    /// </summary>
    public class DateRange : IEquatable<DateRange> 
    {
        private DateTime? _StartDate, _EndDate;

        /// <summary>
        /// Throws an exception if the specified start date occurs after the specified end date
        /// </summary>
        /// <param name="startDate">Specifies the start date</param>
        /// <param name="endDate">Specifies the end date</param>
        private static void EnforceStartDateFollowsEndDate(Nullable<DateTime> startDate, Nullable<DateTime> endDate)
        {
            if ((startDate.HasValue && endDate.HasValue) && (endDate.Value < startDate.Value))
                throw new InvalidOperationException("StartDate must be less than or equal to EndDate");
        }

        /// <summary>
        /// Gets the larger (most recent) value for <see cref="StartDate"/>.
        /// </summary>
        /// <param name="other">The start date to compare the current instance's 
        /// <see cref="StartDate"/> to.</param>
        /// <returns>The latest value for <see cref="StartDate"/>.</returns>
        private Nullable<DateTime> GetLaterStartDate(Nullable<DateTime> other)
        {
            return Nullable.Compare<DateTime>(this.StartDate, other) >= 0 ? this.StartDate : other;
        }

        /// <summary>
        /// Gets the smallest (least recent) value for <see cref="EndDate"/>.
        /// </summary>
        /// <param name="other">The end date to compare the current instance's 
        /// <see cref="EndDate"/> to.</param>
        /// <returns>The earliest value for <see cref="EndDate"/>.</returns>
        private Nullable<DateTime> GetEarlierEndDate(Nullable<DateTime> other)
        {
            //!endDate.HasValue == +infinity, not negative infinity
            //as is the case with !startDate.HasValue
            if (Nullable.Compare<DateTime>(this.EndDate, other) == 0) return other;
            if (this.EndDate.HasValue && !other.HasValue) return this.EndDate;
            if (!this.EndDate.HasValue && other.HasValue) return other;
            return (Nullable.Compare<DateTime>(this.EndDate, other) >= 0) ? other : this.EndDate;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DateRange() : this(new Nullable<DateTime>(), new Nullable<DateTime>()) 
        { 
        }  // Tested

        /// <summary>
        /// Allows the <see cref="StartDate"/> and <see cref="EndDate"/> initial values 
        /// to be specified.
        /// </summary>
        /// <param name="startDate">The initial value for <see cref="StartDate"/>.</param>
        /// <param name="endDate">The initial value for <see cref="EndDate"/>.</param>
        public DateRange(DateTime? startDate, DateTime? endDate)
        {
            EnforceStartDateFollowsEndDate(startDate, endDate);
            this._StartDate = startDate;
            this._EndDate = endDate;
        } // Tested

        /// <summary>
        /// Gets a <see cref="Nullable<TimeSpan>"/> value for the time between the 
        /// <see cref="StartDate"/> and <see cref="EndDate"/>. A null result represents an infinite
        /// timespan.
        /// </summary>
        public Nullable<TimeSpan> TimeSpan
        {
            get { return this.EndDate - this.StartDate; }
        } //TODO: Test

        public Nullable<DateTime> StartDate
        {
            get { return _StartDate; }
            set
            {
                EnforceStartDateFollowsEndDate(value, this._EndDate);
                _StartDate = value;
            }
        } //TODO: Test

        public Nullable<DateTime> EndDate
        {
            get { return _EndDate; }
            set
            {
                EnforceStartDateFollowsEndDate(this._StartDate, value);
                _EndDate = value;
            }
        } //TODO: Test

        /// <summary>
        /// Gets the intersecting <see cref="DateRange"/> that overlaps the current and 
        /// given instances.
        /// </summary>
        /// <param name="other">The <see cref="DateRange"/> to get the intersecting
        /// <see cref="DateRange"/> from.</param>
        /// <returns>A <see cref="DateRabge"/> with latest <see cref="StartDate"/> and
        /// earliest <see cref="EndDate"/> between the current and given instance.</returns>
        public DateRange GetIntersection(DateRange other)
        {
            if (!Intersects(other)) 
                throw new InvalidOperationException("DateRanges do not intersect");

            return new DateRange(GetLaterStartDate(other.StartDate), GetEarlierEndDate(other.EndDate));
        } // Tested

        /// <summary>
        /// Determines if the currnent instance occurs within the given instance.
        /// </summary>
        /// <param name="other">The date range that the current instance may occure 
        /// between.</param>
        /// <returns>True if the current instance occurs between the <see cref="StartDate"/> and
        /// <see cref="EndDate"/> of the "other" instance.</returns>
        public bool OccursWithin(DateRange other)
        {
            if (!Intersects(other))
                return false;
            DateRange intersection = this.GetIntersection(other);
            return intersection.Equals(this);
        } // Tested

        /// <summary>
        /// Determines if two <see cref="DateRange"/>s intersect.
        /// </summary>
        /// <param name="other">The <see cref="DateRange"/> to compare to the current 
        /// instance.</param>
        /// <returns>True if there is any overlap between the current 
        /// <see cref="DateRange"/> instance and the "other" isntance.</returns>
        public bool Intersects(DateRange other)
        {
            return !(// Not
                     // other ends before this begins
                     (this.StartDate.HasValue && other.EndDate.HasValue && other.EndDate.Value < this.StartDate.Value) 
                     // other starts after this ends
                     || (this.EndDate.HasValue && other.StartDate.HasValue && other.StartDate.Value > this.EndDate.Value) 
                     // this ends before other starts
                     || (other.StartDate.HasValue && this.EndDate.HasValue && this.EndDate.Value < other.StartDate.Value)
                     // this starts after other end
                     || (other.EndDate.HasValue && this.StartDate.HasValue && this.StartDate.Value > other.EndDate.Value));
        } // Tested

        public bool Includes(DateTime d)
        {
            return this.Intersects(new DateRange(d, d));
        } //TODO: Test

        public bool Equals(DateRange other)
        {
            if (object.ReferenceEquals(other, null)) 
                return false;

            return ((this.StartDate == other.StartDate) && (this.EndDate == other.EndDate));
        } // Tested

        public static DateRange Now
        {
            get
            {
                DateTime now = DateTime.Now;
                return new DateRange(now, now);
            }
        } // Tested

        /// <summary>
        /// Gets a <see cref="DateRange"/> that represents the current day.
        /// </summary>
        public static DateRange Today
        {
            get
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);
                return new DateRange(today, tomorrow);
            }
        } // Tested
    }
}