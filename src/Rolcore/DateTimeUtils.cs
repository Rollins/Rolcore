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
        /// Gets the last occurrence of the specified <see cref="DayOfWeek"/> within the given month
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
