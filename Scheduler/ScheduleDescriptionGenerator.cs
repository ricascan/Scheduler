using System;
using System.Text;

namespace Scheduler
{
    public class ScheduleDescriptionGenerator
    {
        private string outputDescription;
        private ScheduleConfiguration configuration;
        private DateTime outputDateTime;

        public string GetScheduleDescription(ScheduleConfiguration Configuration, DateTime OutputDateTime)
        {
            this.configuration = Configuration;
            this.outputDateTime = OutputDateTime;
            switch (this.configuration.ScheduleType)
            {
                case ScheduleTypes.Once:
                    this.GenerateOutputDescriptionOnce();
                    break;
                case ScheduleTypes.Recurring:
                    this.GenerateOutputDescriptionRecurring();
                    break;
            }
            return this.outputDescription;
        }

        private void GenerateOutputDescriptionRecurring()
        {
            this.outputDescription = new StringBuilder(string.Format(Resources.Global.ScheduleDescriptionOccursRecurring,
                                    this.configuration.Frequency, EnumerationsDescriptionManager
                                        .GetRecurringTypeUnitDescription(this.configuration.RecurringType)))
                                        .Append(((this.configuration.RecurringType == RecurringTypes.Weekly) ? $"on the following days: {string.Join(", ", this.configuration.DaysOfWeek)}, " : ""))
                                        .Append((this.configuration.RecurringType == RecurringTypes.Monthly && this.configuration.DayOfMonth != null) ? $"on day {this.configuration.DayOfMonth}, " : "")
                                        .Append((this.configuration.RecurringType == RecurringTypes.Monthly && this.configuration.DayOfMonth == null) ? 
                                        $"on the {this.configuration.MonthlyFirstOrderConfiguration} {this.configuration.MonthlySecondOrdenConfiguration}, " : "")
                                        .Append((this.configuration.HourlyFrequency.HasValue ?
                                        string.Format(Resources.Global.ScheduleDescriptionHourly, configuration.HourlyFrequency,
                                        configuration.StartTime.Value.ToString("t"), configuration.EndTime.Value.ToString("t")) : ""))
                                        .Append(this.GetGeneralRecurringDescription()).ToString();
        }

        private string GetGeneralRecurringDescription() => string.Format(Resources.Global.ScheduleDescription, outputDateTime, this.configuration.StartDate);

        private void GenerateOutputDescriptionOnce()
        {
            this.outputDescription = Resources.Global.ScheduleDescriptionOccursOnce
                         + this.GetGeneralRecurringDescription();
        }


    }
}
