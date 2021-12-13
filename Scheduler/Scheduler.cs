using System;

namespace Scheduler
{
    public class Scheduler
    {
        public ScheduleTypes ScheduleType { get; set; } = ScheduleTypes.Recurring;
        public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);
        public DateTime? EndDate { get; set; } = null;
        public RecurringTypes RecurringType { get; set; } = RecurringTypes.Daily;
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int Frequency { get; set; } = 1;
        public DateTime? EndTime { get; set; }
        public DateTime? StartTime { get; set; }
        public int? HourlyFrequency { get; set; }
        public DayOfWeek[] DaysOfWeek { get; set; }
        public int? DayOfMonth { get; set; }
        public MonthlyFirstOrderConfiguration? MonthlyFirstOrderConfiguration { get; set; } = null;
        public MonthlySecondOrderConfiguration? MonthlySecondOrdenConfiguration { get; set; } = null;
        public string Language { get; set; }
    }
}
