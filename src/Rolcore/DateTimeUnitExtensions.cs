using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rolcore
{
    public static class DateTimeUnitExtensions
    {
        /// <summary>
        /// Calculates the number of milliseconds in a given <see cref="DateTimeUnit"/>. For 
        /// example, if the DateTimeUnit passed in is Days the unit multiplier would be 
        /// 24*60*60*1000 = 86,400,000 (24 hours per day * 60 minutes per hour * 60 seconds per 
        /// minute * 1,000 miliseconds per second).
        /// </summary>
        /// <param name="unit">Specifies the unit of measure.</param>
        /// <returns></returns>
        public static long ToMilliseconds(this DateTimeUnit dateTimeUnit)
        {
            switch (dateTimeUnit)
            {
                case DateTimeUnit.Millisecond:
                    return Milliseconds.PerMillisecond;
                case DateTimeUnit.Second:
                    return Milliseconds.PerSecond;
                case DateTimeUnit.Minute:
                    return Milliseconds.PerMinute;
                case DateTimeUnit.Hour:
                    return Milliseconds.PerHour;
                case DateTimeUnit.Day:
                    return Milliseconds.PerDay;
                case DateTimeUnit.Week:
                    return Milliseconds.PerWeek;
                case DateTimeUnit.Month:
                    return Convert.ToInt64(Milliseconds.PerMonth);
                case DateTimeUnit.Year: // too big
                default:
                    throw new InvalidOperationException(string.Format("Cannot convert {0} to milliseconds.", dateTimeUnit));
            }
        }

        public static long ToSeconds(this DateTimeUnit dateTimeUnit)
        {
            switch (dateTimeUnit)
            {
                case DateTimeUnit.Millisecond:
                case DateTimeUnit.Second:
                case DateTimeUnit.Minute:
                case DateTimeUnit.Hour:
                case DateTimeUnit.Day:
                case DateTimeUnit.Week:
                case DateTimeUnit.Month:
                    return dateTimeUnit.ToMilliseconds() / Milliseconds.PerSecond;
                case DateTimeUnit.Year: // too big
                    return Seconds.PerYear;
                default:
                    throw new InvalidOperationException(string.Format("Cannot convert {0} to milliseconds.", dateTimeUnit));
            }
            
        }

        public static TimeSpan ToTimeSpan(this DateTimeUnit dateTimeUnit)
        {
            switch (dateTimeUnit)
            {
                case DateTimeUnit.Millisecond:
                    return new TimeSpan(0, 0, 0, 0, Milliseconds.PerMillisecond);
                case DateTimeUnit.Second:
                    return new TimeSpan(0, 0, 0, 0, Milliseconds.PerSecond);
                case DateTimeUnit.Minute:
                    return new TimeSpan(0, 0, 0, 0, Milliseconds.PerMinute);
                case DateTimeUnit.Hour:
                    return new TimeSpan(0, 0, 0, 0, Milliseconds.PerHour);
                case DateTimeUnit.Day:
                    return new TimeSpan(0, 0, 0, 0, Milliseconds.PerDay);
                case DateTimeUnit.Week:
                    return new TimeSpan(0, 0, 0, 0, Milliseconds.PerWeek);
                case DateTimeUnit.Month:
                    return new TimeSpan(0, 0, 0, Convert.ToInt32(Seconds.PerMonth));
                case DateTimeUnit.Year:
                    return new TimeSpan(Days.PerYear, 0, 0, 0);

                default:
                    throw new InvalidOperationException("Unknown DateTimeUnit: " + dateTimeUnit);
            }
        }
    }
}
