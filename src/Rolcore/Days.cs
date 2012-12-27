//-----------------------------------------------------------------------
// <copyright file="Days.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore
{
    /// <summary>
    /// Static information about days.
    /// </summary>
    public static class Days
    {
        /// <summary>
        /// Number of days per week.
        /// </summary>
        public const int PerWeek = 7;

        /// <summary>
        /// Average number of days per month.
        /// </summary>
        public const double PerMonth = Days.PerYear / Months.PerYear;

        /// <summary>
        /// Average number of days per month in a leap year.
        /// </summary>
        public const double PerMonthInLeapYear = Days.PerLeapYear / Months.PerYear;

        /// <summary>
        /// Number of days per year.
        /// </summary>
        public const int PerYear = 365;

        /// <summary>
        /// Number of days per leap year.
        /// </summary>
        public const int PerLeapYear = 366;
    }
}
