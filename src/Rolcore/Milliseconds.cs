//-----------------------------------------------------------------------
// <copyright file="Milliseconds.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore
{
    /// <summary>
    /// Contains constants related to milliseconds. 
    /// </summary>
    public class Milliseconds
    {
        /// <summary>
        /// Duh.
        /// </summary>
        public const byte PerMillisecond = 1;

        /// <summary>
        /// Milliseconds per second.
        /// </summary>
        public const ushort PerSecond = 1000;

        /// <summary>
        /// Milliseconds per minute.
        /// </summary>
        public const ushort PerMinute = Milliseconds.PerSecond * Seconds.PerMinute;

        /// <summary>
        /// Milliseconds per hour.
        /// </summary>
        public const int PerHour = Milliseconds.PerMinute * Minutes.PerHour;

        /// <summary>
        /// Milliseconds per day.
        /// </summary>
        public const int PerDay = Milliseconds.PerHour * Hours.PerDay;

        /// <summary>
        /// Milliseconds per week.
        /// </summary>
        public const int PerWeek = Milliseconds.PerDay * Days.PerWeek;

        /// <summary>
        /// Milliseconds per month.
        /// </summary>
        public const double PerMonth = Milliseconds.PerDay * Days.PerMonth;

        /// <summary>
        /// Milliseconds per month in a leap year.
        /// </summary>
        public const double PerMonthInLeapYear = Milliseconds.PerDay * Days.PerMonthInLeapYear;
    }
}
