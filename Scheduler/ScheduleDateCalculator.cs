using System;
using System.Linq;

namespace Scheduler
{
    public class ScheduleDateCalculator
    {
        public ScheduleOutputData OutputData { get; private set; }
        private ScheduleConfiguration configuration;
        private ScheduleDescriptionGenerator descriptionGenerator;
        public void CalculateNextDateTime(ScheduleConfiguration Configuration)
        {
            this.configuration = Configuration;
            this.OrderDaysOfWeek();
            this.OutputData = new ScheduleOutputData();
            switch (this.configuration.ScheduleType)
            {
                case ScheduleTypes.Once:
                    this.OutputData.OutputDateTime = this.configuration.DateTime;
                    break;
                case ScheduleTypes.Recurring:
                    try
                    {
                        this.OutputData.OutputDateTime = this.CalculateNextDateTimeRecurring();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw new ScheduleException(Resources.Global.GeneratedDateTimeNotRepresentable);
                    }
                    break;
            }
            this.GenerateOutputDescription();
        }

        private void GenerateOutputDescription()
        {
            this.descriptionGenerator = new ScheduleDescriptionGenerator();
            this.OutputData.OutputDescription = this.descriptionGenerator
                .GetScheduleDescription(this.configuration, this.OutputData.OutputDateTime.Value);
        }

        private void OrderDaysOfWeek()
        {
            if (this.configuration.DaysOfWeek != null)
            {
                this.configuration.DaysOfWeek = this.configuration.DaysOfWeek.OrderBy(D => (int)D).ToArray();
            }
        }
        
        private DateTime CalculateNextDateTimeRecurring()
        {
            DateTime? OutputDateTime = this.TryGetNextDateHourly();
            if (OutputDateTime.HasValue == false)
            {
                OutputDateTime = this.configuration.RecurringType switch
                {
                    RecurringTypes.Daily => this.CalculateNextDateTimeDaily(),
                    RecurringTypes.Weekly => this.CalculateNextDateTimeWeekly(),
                    _ => throw new ScheduleException(Resources.Global.RecurringTypes_Invalid),
                };
            }
            return OutputDateTime.Value;
        }
        private DateTime CalculateNextDateTimeDaily()
        {
            DateTime? OutputDateTime;

            OutputDateTime = this.configuration.CurrentDate.Date
                .AddDays(this.configuration.Frequency)
                .AddHours(this.configuration.StartTime?.TotalHours?? 0);

            return OutputDateTime.Value;
        }

        private DateTime CalculateNextDateTimeWeekly()
        {
            DateTime? OutputDateTime;
            DayOfWeek? NextDayOfWeek = this.configuration.DaysOfWeek.FirstOrDefault(D => (int)D > (int)this.configuration.CurrentDate.DayOfWeek);
            if (NextDayOfWeek.Value == DayOfWeek.Sunday)
            {
                OutputDateTime = this.GetNextWeekday(this.configuration.CurrentDate, this.configuration.DaysOfWeek[0], this.configuration.Frequency)
                    .Date.AddHours(this.configuration.StartTime?.TotalHours ?? 0);
            }
            else
            {
                OutputDateTime = GetNextWeekday(this.configuration.CurrentDate, NextDayOfWeek.Value, 1).Date.AddHours(this.configuration.StartTime?.TotalHours ?? 0);
            }         
            return OutputDateTime.Value;
        }


        private DateTime? TryGetNextDateHourly()
        {
            DateTime OutputDateTime = this.configuration.CurrentDate;
            if (this.configuration.HourlyFrequency.HasValue
                && OutputDateTime.TimeOfDay < this.configuration.EndTime)
            {
                OutputDateTime = OutputDateTime.Date.AddHours(this.configuration.StartTime.Value.TotalHours);
                while (OutputDateTime.TimeOfDay <= this.configuration.CurrentDate.TimeOfDay)
                {
                    OutputDateTime = OutputDateTime.AddHours(this.configuration.HourlyFrequency.Value);
                }
                if (OutputDateTime.Date == this.configuration.CurrentDate.Date
                    && this.TimeInInterval(OutputDateTime.TimeOfDay, this.configuration.StartTime, this.configuration.EndTime))
                {
                    return OutputDateTime;
                }               
            }
            return null;
        }
        private DateTime GetNextWeekday(DateTime Start, DayOfWeek Day, int WeeklyFrequency)
        {
            int daysToAdd = ((int)Day - (int)Start.DayOfWeek + (7)) % 7;
            if(daysToAdd <= 0)
            {
                daysToAdd = 7;
            }
            daysToAdd = (WeeklyFrequency - 1) * 7 + daysToAdd;
            return Start.AddDays(daysToAdd);
        }
        

        private bool TimeInInterval(TimeSpan Time, TimeSpan? StartTime, TimeSpan? EndTime)
            => (StartTime.HasValue == false || Time >= StartTime)
                && (EndTime.HasValue == false || Time < EndTime);
    }
}
