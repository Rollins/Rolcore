using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public const int PerMillisecond = 1;

        /// <summary>
        /// Milliseconds per second.
        /// </summary>
        public const int PerSecond = 1000;

        /// <summary>
        /// Milliseconds per minute.
        /// </summary>
        public const int PerMinute = Milliseconds.PerSecond * Seconds.PerMinute;

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
