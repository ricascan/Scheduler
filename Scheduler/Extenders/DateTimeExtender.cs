using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Extenders
{
    public static class DateTimeExtender
    {
        public static DateTime GetNextWeekday(this DateTime Start, DayOfWeek Day, int WeeklyFrequency)
        {
            int daysToAdd = ((int)Day - (int)Start.DayOfWeek + (7)) % 7;
            if (daysToAdd <= 0)
            {
                daysToAdd = 7;
            }
            daysToAdd = (WeeklyFrequency - 1) * 7 + daysToAdd;
            return Start.AddDays(daysToAdd);
        }

        public static bool IsInInterval(this DateTime Date, DateTime? StartDate, DateTime? EndDate)
            => (StartDate.HasValue == false || Date >= StartDate)
                && (EndDate.HasValue == false || Date < EndDate);

        public static bool IsWeekendDay(this DateTime Date) => Date.DayOfWeek == DayOfWeek.Sunday || Date.DayOfWeek == DayOfWeek.Saturday;
        public static bool IsWeekDay(this DateTime Date) => Date.DayOfWeek != DayOfWeek.Sunday && Date.DayOfWeek != DayOfWeek.Saturday;
    }
}
