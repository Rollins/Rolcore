//-----------------------------------------------------------------------
// <copyright file="DateTimeUnit.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore
{
    /// <summary>
    /// A unit of measurement for dates and times.
    /// </summary>
    public enum DateTimeUnit
    {
        /// <summary>
        /// One millisecond of time.
        /// </summary>
        Millisecond = 0,
        /// <summary>
        /// One second of time.
        /// </summary>
        Second = 1,
        /// <summary>
        /// One minute of time.
        /// </summary>
        Minute = Seconds.PerMinute,
        /// <summary>
        /// One hour of time.
        /// </summary>
        Hour = Seconds.PerHour,
        /// <summary>
        /// One day of time.
        /// </summary>
        Day = Seconds.PerDay,
        /// <summary>
        /// One week of time.
        /// </summary>
        Week = Seconds.PerWeek,
        /// <summary>
        /// One month of time.
        /// </summary>
        Month = (int)Seconds.PerMonth,
        /// <summary>
        /// One year of time.
        /// </summary>
        Year = Seconds.PerYear
    }
}
