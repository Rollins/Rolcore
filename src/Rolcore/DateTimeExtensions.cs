//-----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Extension methods for <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets the nearest weekday (Monday through Friday) for the given 
        /// <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date">The date on which to calculate the nearest weekday.</param>
        /// <returns>A <see cref="DateTime"/> value that represents a valid weekday day.</returns>
        public static DateTime NearestWeekday(this DateTime date)
        {
            DateTime result = date;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    result = date.AddDays(-1);
                    break;
                case DayOfWeek.Sunday:
                    result = date.AddDays(1);
                    break;
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Determines if the given <see cref="DateTime"/> occurs on a weekend.
        /// </summary>
        /// <param name="date">The date to determine if it occurs on a weekend.</param>
        /// <returns>True if the date occurs on a weekend, otherwise false.</returns>
        public static bool IsWeekend(this DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Saturday) || (date.DayOfWeek == DayOfWeek.Sunday);
        }

        /// <summary>
        /// Determines if the given <see cref="DateTime"/> is a holiday or not.
        /// </summary>
        /// <param name="date">The date to determine if it is a holiday.</param>
        /// <returns>True if the date is a holiday, otherwise false.</returns>
        public static bool IsHoliday(this DateTime date)
        {
            DateTime[] holidays = DateTimeUtils.HolidaysByYear(date.Year);
            return holidays.Contains(date.Date);
        }

        public static IEnumerable<DateTime> Next(this DateTime date, DateTimeUnit unit, int count)
        {
            DateTime result = date;
            for (int i = 0; i < count; i++)
            {
                result = result.Add(unit.ToTimeSpan());
                yield return result;
            }
        }

        public static IEnumerable<DateTime> Previous(this DateTime date, DateTimeUnit unit, int count)
        {
            DateTime result = date;
            for (int i = 0; i < count; i++)
            {
                result = result.Subtract(unit.ToTimeSpan());
                yield return result;
            }
        }

        /// <summary>
        /// Gets the given Date in sortable datetime format
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Sortable date time format ex: 2012-06-26T11:55:36 </returns>
        public static string GetDateInSortableDateTimeFormat(this DateTime date)
        {
            return date.ToString("s", CultureInfo.InvariantCulture);
        }
    }
}
