using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class ScheduleDateCalculator
    {
        public DateTime? OutputDateTime { get; private set; }
        public string OutputDescription { get; private set; }
        public void CalculateNextDateTime(DateTime CurrentDate, ScheduleConfiguration Configuration)
        {
            switch (Configuration.ScheduleType)
            {
                case ScheduleTypes.Once:
                    this.OutputDateTime = Configuration.DateTime;
                    break;
                case ScheduleTypes.Recurring:
                        DateTime NextDate = this.CalculateNextDateTimeRecurring(CurrentDate, Configuration);
                        this.OutputDateTime = NextDate;
                        this.OutputDescription = string.Format("Occurs every {0} {1}. Schedule will be used on {2} starting on {3}", 
                            Configuration.Frequency, "Interval", NextDate, Configuration.StartDate.Value);
                    break;
                default:
                    break;
            }
        }

        

        private DateTime CalculateNextDateTimeRecurring(DateTime CurrentDate, ScheduleConfiguration Configuration)
            => Configuration.RecurringType switch
            {
                RecurringTypes.Hourly   => CurrentDate.AddHours(Configuration.Frequency.Value),
                RecurringTypes.Daily    => CurrentDate.AddDays(Configuration.Frequency.Value),
                RecurringTypes.Weekly   => CurrentDate.AddDays(7 * Configuration.Frequency.Value),
                RecurringTypes.Mounthly => CurrentDate.AddMonths(Configuration.Frequency.Value),
                RecurringTypes.Yearly   => CurrentDate.AddYears(Configuration.Frequency.Value),
                _ => throw new ScheduleException("The recurring type is invalid"),
            };
    }
}
