using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rolcore
{
    public static class Days
    {
        public const int PerWeek = 7;
        public const double PerMonth = Days.PerYear / Months.PerYear;
        public const double PerMonthInLeapYear = Days.PerLeapYear / Months.PerYear;
        public const int PerYear = 365;
        public const int PerLeapYear = 366;
    }
}
