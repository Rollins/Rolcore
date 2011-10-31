using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rolcore
{
    public class DateTimeUtils
    {
        /// <summary>
        /// Gets a list of <see cref="DateTime"/>s that are holidays in the given year.
        /// </summary>
        /// <param name="year">The year in which to calculate holidays for.</param>
        /// <returns>An array of <see cref="DateTime"/>.</returns>
        public static DateTime[] HolidaysByYear(int year)
        {
            DateTime yearStart = new DateTime(year, 1, 1);
            List<DateTime> result = new List<DateTime>(8);
            // New Year's Day
            result.Add(yearStart.NearestWeekday());
            // Birthday of Martin Luther King, Third Monday in January
            //Not a Rolcore Holiday: result.Add(GetNthDayOfWeekInMonth(year, 1, DayOfWeek.Monday, 3));
            // Memorial Day, last Monday in May (since 1971 - years 1868 to 1970 not supported)
            result.Add(GetLastDayOfWeekInMonth(year, MonthOfYear.May, DayOfWeek.Monday).NearestWeekday());
            // Independence Day, July 4
            result.Add((new DateTime(year, 7, 4).NearestWeekday()));
            // Labor Day, first Monday in September
            result.Add(GetNthDayOfWeekInMonth(year, MonthOfYear.September, DayOfWeek.Monday, 1).NearestWeekday());
            // Veterans Day, November 11th (except from 1971 to 1977, inclusive, when it was celebrated on the fourth Monday in October; formerly known as Armistice - not supported).
            // Not a Rolcore Holiday: result.Add(new DateTime(year, 11, 11));
            // Thanksgiving Day, fourth Thursday in November
            result.Add(GetNthDayOfWeekInMonth(year, MonthOfYear.November, DayOfWeek.Thursday, 4).NearestWeekday());
            // Christmas Day, December 25th.
            result.Add((new DateTime(year, 12, 25)).NearestWeekday());

            return result.ToArray(); //TODO: Cache result instead of using HolidaysForThisYearNextYear.
        }

        /// <summary>
        /// Gets the "Nth" day of the month (for example, the 3rd Monday in January) as a 
        /// <see cref="DateTime"/>.
        /// </summary>
        /// <param name="year">The year in which the date should be calculated.</param>
        /// <param name="month">The month (1 through 12) in which the date should be calculated.</param>
        /// <param name="dayOfWeek">The day of week to calculate in the given month and year.</param>
        /// <param name="dayOccurences">Which occurance to calculate (1 through 5).</param>
        /// <returns>A <see cref="DateTime"/> that represents the desired occurance of the desired
        /// <see cref="DayOfWeek"/> in the given month and year.</returns>
        public static DateTime GetNthDayOfWeekInMonth(int year, MonthOfYear month, DayOfWeek dayOfWeek, short dayOccurences)
        {
            if (dayOccurences < 1) throw new ArgumentOutOfRangeException("dayOccurence", "There cannot be less than one occurence of a day in a month.");
            if (dayOccurences > 5) throw new ArgumentOutOfRangeException("dayOccurence", "There cannot be more than 5 occurences of a day in a month.");
            
            DateTime result = new DateTime(year, (int)month, 1);

            int occurences = 0;
            while (occurences < dayOccurences)
            {
                if (result.DayOfWeek == dayOfWeek)
                {
                    if (++occurences == dayOccurences)
                        return result;
                }
                result = result.AddDays(1.00);
                if ((result.Month > (int)month) || (result.Year > year))
                    break;
            }
            throw new InvalidOperationException(string.Format("There are not {0} {1}s in {2}/{3}.", dayOccurences, dayOfWeek, month, year));
        }

        /// <summary>
        /// Gets the last occurance of the specified <see cref="DayOfWeek"/> within the given month
        /// and year.
        /// </summary>
        /// <param name="year">The year for which to calculate the desired date.</param>
        /// <param name="month">The month for which to calculate the desired date.</param>
        /// <param name="dayOfWeek">The <see cref="DayOfWeek"/> to calculate.</param>
        /// <returns>A <see cref="DateTime"/> that represents the last of the specified 
        /// <see cref="DayOfWeek"/> within the given month and year.</returns>
        public static DateTime GetLastDayOfWeekInMonth(int year, MonthOfYear month, DayOfWeek dayOfWeek)
        {
            try
            {
                return GetNthDayOfWeekInMonth(year, month, dayOfWeek, 5);
            }
            catch (InvalidOperationException)
            {
                return GetNthDayOfWeekInMonth(year, month, dayOfWeek, 4);
            }
        }
    }
}
