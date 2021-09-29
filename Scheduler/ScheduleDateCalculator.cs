using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class ScheduleDateCalculator
    {
        public ScheduleOutputData OutputData { get; private set; }
        public void CalculateNextDateTime(DateTime CurrentDate, ScheduleConfiguration Configuration)
        {
            switch (Configuration.ScheduleType)
            {
                case ScheduleTypes.Once:
                    this.OutputData.OutputDateTime = Configuration.DateTime;
                    break;
                case ScheduleTypes.Recurring:
                    this.OutputData = new ScheduleOutputData();
                    DateTime NextDate = this.CalculateNextDateTimeRecurring(CurrentDate, Configuration);
                    this.OutputData.OutputDateTime = NextDate;
                    this.OutputData.OutputDescription = string.Format(Resources.Global.ScheduleDescription, 
                        Configuration.Frequency, EnumerationsDescriptionManager
                            .GetRecurringTypeUnitDescription(Configuration.RecurringType.Value), NextDate, Configuration.StartDate.Value);
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
