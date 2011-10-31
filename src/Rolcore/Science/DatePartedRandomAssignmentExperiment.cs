using System;
using System.Collections.Generic;
using System.Linq;

namespace Rolcore.Science
{
    public class DatePartedRandomAssignmentExperiment<T> : RandomAssignmentExperiment<T>
    {
        private readonly List<DateRange> _ActiveDatePartRanges = new List<DateRange>();

        /// <summary>
        /// Gets the value for the date ranges (day parts) within which the experiment is active.
        /// This list automatically updates its self so that all <see cref="DateRange"/> instances
        /// occur on <see cref="DateTime.Today"/>.
        /// </summary>
        public List<DateRange> ActiveDatePartRanges
        {
            get
            {
                DateTime today = DateTime.Today;
                IEnumerable<DateRange> expiredDateParts = from dateRange in this._ActiveDatePartRanges
                                                          where dateRange.StartDate.Value < DateTime.Today
                                                          select dateRange;
                foreach (DateRange dateRange in expiredDateParts)
                {
                    DateTime newStartDate = new DateTime( // Use the current date information but
                        today.Year,                       // preserve the time information from
                        today.Month,                      // the original date range to ensure the
                        today.Day,                        // ActiveDatePartRanges property always 
                        dateRange.StartDate.Value.Hour,   // occurs "today".
                        dateRange.StartDate.Value.Minute,
                        dateRange.StartDate.Value.Second,
                        dateRange.StartDate.Value.Millisecond);
                    while (dateRange.EndDate.Value < newStartDate)              // We don't necessarily run every day, so 
                        dateRange.EndDate = dateRange.EndDate.Value.AddDays(1); // make sure to account for multiple days.
                    dateRange.StartDate = newStartDate;
                }
                return this._ActiveDatePartRanges;
            }
            set
            {
                this._ActiveDatePartRanges.Clear();
                this._ActiveDatePartRanges.AddRange(value);
            }
        }

        /// <summary>
        /// Indicates if the current instance is an active member of the experiment.
        /// </summary>
        public override bool Active
        {
            get
            {
                bool baseActive = base.Active;
                if ((!baseActive) || (this.ActiveDatePartRanges.Count == 0))
                    return baseActive;

                // Check date parts
                foreach (DateRange datePart in this.ActiveDatePartRanges)
                    if (datePart.Includes(DateTime.Now))
                        return true;

                return false; // Current time not within date parts
            }
        }
    }
}
