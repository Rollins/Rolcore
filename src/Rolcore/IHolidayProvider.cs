using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rolcore
{
    public interface IHolidayProvider
    {
        DateTime[] HolidaysByYear(int year);
    }
}
