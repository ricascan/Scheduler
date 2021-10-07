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
            this.OutputData = new ScheduleOutputData();
            switch (Configuration.ScheduleType)
            {
                case ScheduleTypes.Once:
                    this.OutputData.OutputDateTime = Configuration.DateTime;
                    this.OutputData.OutputDescription = Resources.Global.ScheduleDescriptionOccursOnce
                         + string.Format(Resources.Global.ScheduleDescription, this.OutputData.OutputDateTime, Configuration.StartDate);
                    break;
                case ScheduleTypes.Recurring:
                    DateTime NextDate;
                    try
                    {
                        NextDate = this.CalculateNextDateTimeRecurring(CurrentDate, Configuration);
                    }
                    catch (ArgumentOutOfRangeException Ex)
                    {
                        throw new ScheduleException(Ex.Message);
                    }
                    this.OutputData.OutputDateTime = NextDate;
                    this.OutputData.OutputDescription = string.Format(Resources.Global.ScheduleDescriptionOccursRecurring,
                        Configuration.Frequency, EnumerationsDescriptionManager
                            .GetRecurringTypeUnitDescription(Configuration.RecurringType))
                            + string.Format(Resources.Global.ScheduleDescription, NextDate, Configuration.StartDate);
                    break;
                default:
                    break;
            }
        }



        private DateTime CalculateNextDateTimeRecurring(DateTime CurrentDate, ScheduleConfiguration Configuration)
            => Configuration.RecurringType switch
            {
                RecurringTypes.Hourly => CurrentDate.AddHours(Configuration.Frequency),
                RecurringTypes.Daily => CurrentDate.AddDays(Configuration.Frequency),
                RecurringTypes.Weekly => CurrentDate.AddDays(7 * Configuration.Frequency),
                RecurringTypes.Mounthly => CurrentDate.AddMonths(Configuration.Frequency),
                RecurringTypes.Yearly => CurrentDate.AddYears(Configuration.Frequency),
                _ => throw new ScheduleException(Resources.Global.RecurringTypes_Invalid),
            };

        public static bool DateInInterval(DateTime Date, DateTime? StartDate, DateTime? EndDate)
            => (StartDate.HasValue == false || Date >= StartDate)
                && (EndDate.HasValue == false || Date < EndDate);
    }
}
