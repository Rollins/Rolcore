//-----------------------------------------------------------------------
// <copyright file="Seconds.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore
{
    /// <summary>
    /// Contains constants related to minutes.
    /// </summary>
    public class Seconds
    {
        /// <summary>
        /// Seconds per minute.
        /// </summary>
        public const int PerMinute = 60;

        /// <summary>
        /// Seconds per hour.
        /// </summary>
        public const int PerHour = Seconds.PerMinute * Minutes.PerHour;

        /// <summary>
        /// Seconds per day.
        /// </summary>
        public const int PerDay = Seconds.PerHour * Hours.PerDay;

        /// <summary>
        /// Seconds per week.
        /// </summary>
        public const int PerWeek = Seconds.PerDay * Days.PerWeek;

        /// <summary>
        /// Seconds per week.
        /// </summary>
        public const double PerMonth = Seconds.PerDay * Days.PerMonth;

        /// <summary>
        /// Seconds per year.
        /// </summary>
        public const int PerYear = Seconds.PerDay * Days.PerYear;

        /// <summary>
        /// Seconds per leap year.
        /// </summary>
        public const int PerLeapYear = Seconds.PerDay * Days.PerLeapYear;
    }
}
