using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rolcore
{
    public class UsaNationalHolidayProvider : IHolidayProvider
    {
        public static DateTime[] GetHolidaysByYear(int year)
        {
            DateTime yearStart = new DateTime(year, 1, 1);
            List<DateTime> result = new List<DateTime>(8);
            // New Year's Day
            result.Add(yearStart.NearestWeekday());
            // Birthday of Martin Luther King, Third Monday in January
            //Not a Rolcore Holiday: result.Add(GetNthDayOfWeekInMonth(year, 1, DayOfWeek.Monday, 3));
            // Memorial Day, last Monday in May (since 1971 - years 1868 to 1970 not supported)
            result.Add(DateTimeUtils.GetLastDayOfWeekInMonth(year, MonthOfYear.May, DayOfWeek.Monday).NearestWeekday());
            // Independence Day, July 4
            result.Add((new DateTime(year, 7, 4).NearestWeekday()));
            // Labor Day, first Monday in September
            result.Add(DateTimeUtils.GetNthDayOfWeekInMonth(year, MonthOfYear.September, DayOfWeek.Monday, 1).NearestWeekday());
            // Veterans Day, November 11th (except from 1971 to 1977, inclusive, when it was celebrated on the fourth Monday in October; formerly known as Armistice - not supported).
            // Not a Rolcore Holiday: result.Add(new DateTime(year, 11, 11));
            // Thanksgiving Day, fourth Thursday in November
            result.Add(DateTimeUtils.GetNthDayOfWeekInMonth(year, MonthOfYear.November, DayOfWeek.Thursday, 4).NearestWeekday());
            // Christmas Day, December 25th.
            result.Add((new DateTime(year, 12, 25)).NearestWeekday());

            return result.ToArray(); //TODO: Cache result instead of using HolidaysForThisYearNextYear.
        }

        public DateTime[] HolidaysByYear(int year)
        {
            return GetHolidaysByYear(year);
        }
    }
}
