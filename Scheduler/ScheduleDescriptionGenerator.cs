using Scheduler.Resources;
using System;
using System.Text;

namespace Scheduler
{
    public class ScheduleDescriptionGenerator
    {
        private string outputDescription;
        private Scheduler configuration;
        private DateTime outputDateTime;

        public string GetScheduleDescription(Scheduler Configuration, DateTime OutputDateTime)
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
            this.outputDescription = new StringBuilder(string.Format(SchedulerResourcesManager.GetResource("ScheduleDescriptionOccursRecurring"),
                                    this.configuration.Frequency, SchedulerResourcesManager
                                        .GetRecurringTypeUnitDescription(this.configuration.RecurringType)))
                                        .Append((this.configuration.RecurringType == RecurringTypes.Weekly) ? string.Format(SchedulerResourcesManager.GetResource("OnFollowingDays"), 
                                            string.Join(", ",SchedulerResourcesManager.GetDaysOfWeekResources(this.configuration.DaysOfWeek))) : "")
                                        .Append((this.configuration.RecurringType == RecurringTypes.Monthly && this.configuration.DayOfMonth != null) ? string.Format(SchedulerResourcesManager.GetResource("OnDay"),
                                            configuration.DayOfMonth) : "")
                                        .Append((this.configuration.RecurringType == RecurringTypes.Monthly && this.configuration.DayOfMonth == null
                                        ) ? 
                                        string.Format(SchedulerResourcesManager.GetResource("OnThe"), SchedulerResourcesManager.GetFirstOrderConfigurationResource(this.configuration.MonthlyFirstOrderConfiguration.Value),
                                            SchedulerResourcesManager.GetSecondOrderConfigurationResource(this.configuration.MonthlySecondOrdenConfiguration.Value)) : "")
                                        .Append((this.configuration.HourlyFrequency.HasValue ?
                                        string.Format(SchedulerResourcesManager.GetResource("ScheduleDescriptionHourly"), configuration.HourlyFrequency,
                                        SchedulerResourcesManager.FormatTime(configuration.StartTime.Value), SchedulerResourcesManager.FormatTime(configuration.EndTime.Value)) : ""))
                                        .Append(this.GetGeneralRecurringDescription()).ToString();
        }

        private string GetGeneralRecurringDescription() => string.Format(SchedulerResourcesManager.GetResource("ScheduleDescription"), SchedulerResourcesManager.FormatDateTime(outputDateTime), 
            SchedulerResourcesManager.FormatDateTime(this.configuration.StartDate)); 

        private void GenerateOutputDescriptionOnce()
        {
            this.outputDescription = SchedulerResourcesManager.GetResource("ScheduleDescriptionOccursOnce")
                         + this.GetGeneralRecurringDescription();
        }


    }
}
