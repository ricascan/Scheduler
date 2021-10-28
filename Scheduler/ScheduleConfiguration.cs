using System;

namespace Scheduler
{
    public class ScheduleConfiguration
    {
        public ScheduleTypes ScheduleType { get; set; } = ScheduleTypes.Recurring;
        public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);
        public DateTime? EndDate { get; set; } = null;
        public RecurringTypes RecurringType { get; set; } = RecurringTypes.Daily;
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int DailyFrequency { get; set; } = 1;
        public TimeSpan? EndTime { get; set; }
        public TimeSpan? StartTime { get; set; }
        public int? HourlyFrequency { get; set; }
        public DayOfWeek[] DaysOfWeek { get; set; }
    }
}
