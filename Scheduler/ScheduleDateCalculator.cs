using Scheduler.Extenders;
using System;
using System.Linq;

namespace Scheduler
{
    public class ScheduleDateCalculator
    {
        public ScheduleOutputData CalculateNextDateTime(ScheduleConfiguration Configuration)
        {
            Configuration.OrderDaysOfWeek();
            ScheduleOutputData OutputData = new();
            switch (Configuration.ScheduleType)
            {
                case ScheduleTypes.Once:
                    OutputData.OutputDateTime = Configuration.DateTime;
                    break;
                case ScheduleTypes.Recurring:
                    try
                    {
                        OutputData.OutputDateTime = CalculateNextDateTimeRecurring(Configuration);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw new ScheduleException(Resources.Global.GeneratedDateTimeNotRepresentable);
                    }
                    break;
            }
            OutputData.OutputDescription = GenerateOutputDescription(Configuration,
                OutputData.OutputDateTime.Value);
            return OutputData;
        }

        private static string GenerateOutputDescription(ScheduleConfiguration Configuration, DateTime OutputDateTime) =>
            new ScheduleDescriptionGenerator().GetScheduleDescription(Configuration, OutputDateTime);

        private static DateTime CalculateNextDateTimeRecurring(ScheduleConfiguration Configuration)
        {
            DateTime? OutputDateTime = TryGetNextDateHourly(Configuration);
            if (OutputDateTime.HasValue == false)
            {
                OutputDateTime = Configuration.RecurringType switch
                {
                    RecurringTypes.Daily => CalculateNextDateTimeDaily(Configuration),
                    RecurringTypes.Weekly => CalculateNextDateTimeWeekly(Configuration),
                    RecurringTypes.Monthly => CalculateNextDateTimeMonthly(Configuration),
                    _ => throw new ScheduleException(Resources.Global.RecurringTypes_Invalid),
                };
                OutputDateTime = OutputDateTime.Value.Date.AddHours(Configuration.StartTime?.TotalHours ?? 0);
            }
            return OutputDateTime.Value;
        }
        private static DateTime CalculateNextDateTimeDaily(ScheduleConfiguration Configuration)
        {
            DateTime? OutputDateTime;

            OutputDateTime = Configuration.CurrentDate.Date
                .AddDays(Configuration.Frequency);

            return OutputDateTime.Value;
        }

        private static DateTime CalculateNextDateTimeWeekly(ScheduleConfiguration Configuration)
        {
            DateTime? OutputDateTime;
            DayOfWeek? NextDayOfWeek = Configuration.DaysOfWeek.FirstOrDefault(D => (int)D > (int)Configuration.CurrentDate.DayOfWeek);
            if (NextDayOfWeek.Value == DayOfWeek.Sunday)
            {
                OutputDateTime = Configuration.CurrentDate.GetNextWeekday(Configuration.DaysOfWeek[0], Configuration.Frequency);
            }
            else
            {
                OutputDateTime = Configuration.CurrentDate.GetNextWeekday(NextDayOfWeek.Value, 1);
            }
            return OutputDateTime.Value;
        }

        private static DateTime CalculateNextDateTimeMonthly(ScheduleConfiguration Configuration)
        {
            DateTime? OutputDateTime;
            if (Configuration.DayOfMonth != null)
            {
                OutputDateTime = CalculateDateTimeDayOfMonth(Configuration);
            }
            else
            {   
                OutputDateTime = CalculateDateTimeMonthlyDays(Configuration);               
            }
            return OutputDateTime.Value;
        }

        private static DateTime? CalculateDateTimeDayOfMonth(ScheduleConfiguration Configuration)
        {
            int DayInMonth;
            DateTime? OutputDateTime;
            if (Configuration.CurrentDate.Day < Configuration.DayOfMonth)
            {
                DayInMonth = GetNormalizedDayInMonth(Configuration, Configuration.CurrentDate.Year, Configuration.CurrentDate.Month);
                OutputDateTime = new DateTime(Configuration.CurrentDate.Year,
                    Configuration.CurrentDate.Month, DayInMonth);
            }
            else
            {

                OutputDateTime = Configuration.CurrentDate.AddMonths(Configuration.Frequency);
                DayInMonth = GetNormalizedDayInMonth(Configuration, OutputDateTime.Value.Year, OutputDateTime.Value.Month);
                OutputDateTime = new DateTime(OutputDateTime.Value.Year, OutputDateTime.Value.Month, DayInMonth);
            }

            return OutputDateTime;
        }

        private static int GetNormalizedDayInMonth(ScheduleConfiguration Configuration, int Year, int Mounth)
        {
            int DayInMonth = Configuration.DayOfMonth.Value;
            int DaysInMonth = DateTime.DaysInMonth(Configuration.CurrentDate.Year, Configuration.CurrentDate.Month);
            if (DaysInMonth < DayInMonth)
            {
                DayInMonth = DaysInMonth;
            }

            return DayInMonth;
        }

        private static DateTime? CalculateDateTimeMonthlyDays(ScheduleConfiguration Configuration)
        {
            int Offset = 0;
            int ExecutionFrequency = (int)Configuration.MonthlyFirstOrderConfiguration;
            DateTime? OutputDateTime = null;
            DateTime StartDate = Configuration.CurrentDate;
            while (OutputDateTime.HasValue == false || OutputDateTime.Value <= Configuration.CurrentDate)
            {
                AddMonthIfNeeded();
                OutputDateTime = FindMonthWeekDay(StartDate.Year, StartDate.Month,
                    ExecutionFrequency - Offset, Configuration.MonthlySecondOrdenConfiguration.Value);
                if (OutputDateTime.HasValue == false)
                {
                    Offset++;
                }
            }
            return OutputDateTime;

            void AddMonthIfNeeded()
            {
                if (OutputDateTime.HasValue && OutputDateTime.Value <= Configuration.CurrentDate)
                {
                    StartDate = StartDate.AddMonths(Configuration.Frequency);
                    Offset = 0;
                }
            }
        }
      
        private static DateTime? FindMonthWeekDay(int Year, int Month, int Offset, MonthlySecondOrdenConfiguration Configuration)
        {
            if(Offset > DateTime.DaysInMonth(Year, Month)) { return null; }
            if (Offset < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(Offset));
            }
            Func<DayOfWeek, bool> Condition = Configuration switch
            {
                MonthlySecondOrdenConfiguration.Day => _ => true,
                MonthlySecondOrdenConfiguration.Weekday => Day => Day != DayOfWeek.Saturday && Day != DayOfWeek.Sunday,
                MonthlySecondOrdenConfiguration.WeekendDay => Day => Day == DayOfWeek.Saturday || Day == DayOfWeek.Sunday,
                _ => Day => Day == (DayOfWeek)Configuration
            };
            DateTime Moment = new(Year, Month, 1);
            while (Moment.Month == Month)
            {
                DayOfWeek DayOfWeek = Moment.DayOfWeek;
                if (Condition.Invoke(DayOfWeek))
                {
                    Offset--;
                }
                if (Offset == 0)
                {
                    return Moment;
                }
                Moment = Moment.AddDays(1);
            }
            return null;
        }


        private static DateTime? TryGetNextDateHourly(ScheduleConfiguration Configuration)
        {
            if(Configuration.CurrentDayIsValid() == false)
            {
                return null;
            }
            DateTime OutputDateTime = Configuration.CurrentDate;
            
            if (Configuration.HourlyFrequency.HasValue
                && OutputDateTime.TimeOfDay < Configuration.EndTime)
            {
                OutputDateTime = OutputDateTime.Date.AddHours(Configuration.StartTime.Value.TotalHours);
                while (OutputDateTime.TimeOfDay <= Configuration.CurrentDate.TimeOfDay)
                {
                    OutputDateTime = OutputDateTime.AddHours(Configuration.HourlyFrequency.Value);
                }
                if (OutputDateTime.Date == Configuration.CurrentDate.Date
                    && OutputDateTime.TimeOfDay.IsInInterval(Configuration.StartTime, Configuration.EndTime))
                {
                    return OutputDateTime;
                }
            }
            return null;
        }    
    }
}
