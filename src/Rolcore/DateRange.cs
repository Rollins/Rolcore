//-----------------------------------------------------------------------
// <copyright file="DateRange.cs" company="Rollins, Inc.">
//     Code by Jason Hunt, found at http://www.noticeablydifferent.com/CodeSamples/DateRange.aspx.
//     Modified by Rollins, Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore
{
    using System;
    using System.Diagnostics.Contracts;

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
        /// <summary>
        /// Stores the <see cref="StartDate"/> property value.
        /// </summary>
        private readonly DateTime? startDate;

        /// <summary>
        /// Stores the <see cref="EndDate"/> property value.
        /// </summary>
        private readonly DateTime? endDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class as an infinite range.
        /// </summary>
        public DateRange() : this(null, null) 
        { 
        }  // Tested

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class with the specified 
        /// <see cref="StartDate"/> and <see cref="EndDate"/> values.
        /// </summary>
        /// <param name="startDate">The initial value for <see cref="StartDate"/>.</param>
        /// <param name="endDate">The initial value for <see cref="EndDate"/>.</param>
        public DateRange(DateTime? startDate, DateTime? endDate)
        {
            Contract.Requires<ArgumentException>(
                !((startDate.HasValue && endDate.HasValue) && (endDate.Value < startDate.Value)),
                "startDate is greater than endDate");

            this.startDate = startDate;
            this.endDate = endDate;
        } // Tested

        /// <summary>
        /// Gets a <see href="DateRange"/> object with both the <see cref="StartDate"/> and 
        /// <see cref="EndDate"/> values set to the current date and time on this computer, 
        /// expressed as the local time.
        /// </summary>
        public static DateRange Now
        {
            get
            {
                var now = DateTime.Now;
                return new DateRange(now, now);
            }
        } // Tested

        /// <summary>
        /// Gets a <see href="DateRange"/> object with both the <see cref="StartDate"/> and 
        /// <see cref="EndDate"/> values set to the current date and time on this computer, 
        /// expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public static DateRange UtcNow
        {
            get
            {
                var now = DateTime.UtcNow;
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
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);
                return new DateRange(today, tomorrow);
            }
        } // Tested

        /// <summary>
        /// Gets a <see cref="Nullable{TimeSpan}"/> value for the time between the 
        /// <see cref="StartDate"/> and <see cref="EndDate"/>. A null result represents an infinite
        /// time span.
        /// </summary>
        public TimeSpan? TimeSpan
        {
            get { return this.EndDate - this.StartDate; }
        } // TODO: Test

        /// <summary>
        /// Gets the start date time of the date range. A null value represents an infinitely past
        /// start.
        /// </summary>
        public DateTime? StartDate
        {
            get { return this.startDate; }
        } // TODO: Test

        /// <summary>
        /// Gets the end date time of the date range. A null value represents an infinitely future
        /// end.
        /// </summary>
        public DateTime? EndDate
        {
            get { return this.endDate; }
        } // TODO: Test

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
            Contract.Requires<ArgumentNullException>(other != null, "other is null.");
            Contract.Requires<InvalidOperationException>(this.Intersects(other), "other does not intersect with current instance.");

            var laterStartDate = this.GetLaterStartDate(other.StartDate);
            var earlierEndDate = this.GetEarlierEndDate(other.EndDate);

            return new DateRange(laterStartDate, earlierEndDate);
        } // Tested

        /// <summary>
        /// Determines if the current instance occurs within the given instance.
        /// </summary>
        /// <param name="other">The date range that the current instance may occur 
        /// between.</param>
        /// <returns>True if the current instance occurs between the <see cref="StartDate"/> and
        /// <see cref="EndDate"/> of the "other" instance.</returns>
        public bool OccursWithin(DateRange other)
        {
            if (!this.Intersects(other))
            {
                return false;
            }

            var intersection = this.GetIntersection(other);
            return intersection.Equals(this);
        } // Tested

        /// <summary>
        /// Determines if the specified <see cref="DateRange"/> intersect with the current instance.
        /// </summary>
        /// <param name="other">The <see cref="DateRange"/> to compare to the current 
        /// instance.</param>
        /// <returns>True if there is any overlap between the current 
        /// <see cref="DateRange"/> instance and the "other" instance.</returns>
        [Pure]
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

        /// <summary>
        /// Determines whether the specified <see cref="DateTime"/> occurs within the current 
        /// <see cref="DateRange"/>.
        /// </summary>
        /// <param name="d">The <see cref="DateTime"/> to test.</param>
        /// <returns>true if the specified <see cref="DateTime"/> occur within the current 
        /// <see cref="DateRange"/>; otherwise, false.</returns>
        public bool Includes(DateTime d)
        {
            return this.Intersects(new DateRange(d, d));
        } // TODO: Test

        /// <summary>
        /// Determines whether the specified <see cref="Object"/> is equal to the current 
        /// <see cref="DateRange"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(obj, this))
            {
                return true;
            }

            var dateRange = obj as DateRange;
            if (dateRange == null)
            {
                return false;
            }

            return (this.StartDate == dateRange.StartDate) && (this.EndDate == dateRange.EndDate);
        } // Tested

        /// <summary>
        /// Retrieves the hash code of the <see cref="DateRange"/>.
        /// </summary>
        /// <returns>The hash code of the object, or zero of the <see cref="StartDate"/> and 
        /// <see cref="EndDate"/> values are both null.</returns>
        public override int GetHashCode()
        {
            if (!this.StartDate.HasValue && !this.EndDate.HasValue)
            {
                return 0;
            }

            // Overflow is fine, just wrap
            unchecked 
            {
                var result = 17;
                
                if (this.StartDate.HasValue)
                {
                    result = (result * 23) + this.StartDate.GetHashCode();
                }

                if (this.EndDate.HasValue)
                {
                    result = (result * 23) + this.EndDate.GetHashCode();
                }

                return result;
            }

            // Hat tip to Jon Skeet:
            // http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
        }

        /// <summary>
        /// Indicates whether the current <see cref="DateRange"/> is equal to another 
        /// <see cref="DateRange"/>.
        /// </summary>
        /// <param name="other">A <see cref="DateRange"/> to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        bool IEquatable<DateRange>.Equals(DateRange other)
        {
            return this.Equals(other);
        }

        /// <summary>
        /// Gets the larger (most recent) value for <see cref="StartDate"/>.
        /// </summary>
        /// <param name="other">The start date to compare the current instance's 
        /// <see cref="StartDate"/> to.</param>
        /// <returns>The latest value for <see cref="StartDate"/>.</returns>
        private DateTime? GetLaterStartDate(DateTime? other)
        {
            return Nullable.Compare<DateTime>(this.StartDate, other) >= 0 ? this.StartDate : other;
        }

        /// <summary>
        /// Gets the smallest (least recent) value for <see cref="EndDate"/>.
        /// </summary>
        /// <param name="other">The end date to compare the current instance's 
        /// <see cref="EndDate"/> to.</param>
        /// <returns>The earliest value for <see cref="EndDate"/>.</returns>
        private DateTime? GetEarlierEndDate(DateTime? other)
        {
            return (Nullable.Compare<DateTime>(this.EndDate, other) == 0)
                ? other
                : (this.EndDate.HasValue && !other.HasValue)
                    ? this.EndDate
                    : (!this.EndDate.HasValue && other.HasValue)
                        ? other
                        : (Nullable.Compare<DateTime>(this.EndDate, other) >= 0)
                            ? other
                            : this.EndDate;
        } 
    }
}