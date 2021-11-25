using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Extenders
{
    public static class ConfigurationExtender
    {
        private static ScheduleDateCalculator _calculator;
        public static ScheduleOutputData GetNextExecutionDate(this ScheduleConfiguration Configuration)
        {
            Validate(Configuration);
            ScheduleOutputData OutputData = calculator.CalculateNextDateTime(Configuration);
            if (OutputData.OutputDateTime.Value.IsInInterval(
                Configuration.StartDate, Configuration.EndDate) == false)
            {
                throw new ScheduleException(Resources.Global.NotPossibleToGenerateExecDate);
            }
            return OutputData;
        }

        public static ScheduleOutputData GetNextExecutionDateSeries(this ScheduleConfiguration Configuration, int NumberOfSeries)
        {
            ScheduleOutputData OutputData = null;
            for(int i = 0; i < NumberOfSeries; i++)
            {
                OutputData = Configuration.GetNextExecutionDate();
                Configuration.CurrentDate = OutputData.OutputDateTime.Value;
            }
            return OutputData;
        }

        public static void OrderDaysOfWeek(this ScheduleConfiguration Configuration)
        {
            if (Configuration.DaysOfWeek != null)
            {
                Configuration.DaysOfWeek = Configuration.DaysOfWeek.OrderBy(D => (int)D).ToArray();
            }
        }

        private static void Validate(ScheduleConfiguration Configuration)
        {
            if (Configuration.EndDate < Configuration.StartDate)
            {
                throw new ScheduleException(Resources.Global.StartDateGreaterThanEndDate);
            }
            if (Configuration.CurrentDate.IsInInterval(Configuration.StartDate, Configuration.EndDate) == false)
            {
                throw new ScheduleException(Resources.Global.CurrentDateOutLimits);
            }
            switch (Configuration.ScheduleType)
            {
                case ScheduleTypes.Once:
                    ValidateOnce(Configuration);
                    break;
                case ScheduleTypes.Recurring:
                    ValidateRecurring(Configuration);
                    break;
            }
        }

        private static void ValidateRecurring(ScheduleConfiguration Configuration)
        {
            if ((Configuration.Frequency < 0 || Configuration.HourlyFrequency < 0))
            {
                throw new ScheduleException(Resources.Global.FrequencyMustBeGraterThanZero);
            }
            ValidateHourly(Configuration);
            if (Configuration.RecurringType == RecurringTypes.Weekly)
            {
                ValidateWeekly(Configuration);
            }
            if(Configuration.RecurringType == RecurringTypes.Monthly)
            {
                ValidateMonthly(Configuration);
            }
        }

        private static void ValidateWeekly(ScheduleConfiguration Configuration)
        {
            if ((Configuration.DaysOfWeek == null || Configuration.DaysOfWeek.Length < 1))
            {
                throw new ScheduleException(Resources.Global.MustSetAtLeastOneDayWeek);
            }

            if (Configuration.DaysOfWeek != null
                && Configuration.DaysOfWeek.Distinct().Count() != Configuration.DaysOfWeek.Length)
            {
                throw new ScheduleException(Resources.Global.DaysOfWeekCanNotBeRepeated);
            }
        }

        private static void ValidateHourly(ScheduleConfiguration Configuration)
        {
            if (Configuration.HourlyFrequency.HasValue && (Configuration.StartTime.HasValue == false || Configuration.EndTime.HasValue == false))
            {
                throw new ScheduleException(Resources.Global.MustSetStartEndTimesWhenFrequency);
            }
            if ((Configuration.StartTime.HasValue || Configuration.EndTime.HasValue) && Configuration.HourlyFrequency.HasValue == false)
            {
                throw new ScheduleException(Resources.Global.MustSetHourlyFrequencyWhenStartEndTimes);
            }
            if ((Configuration.StartTime.HasValue && Configuration.EndTime.HasValue && Configuration.EndTime.Value < Configuration.StartTime.Value))
            {
                throw new ScheduleException(Resources.Global.EndTimeCanNotBeLessStartTime);
            }
            if ((Configuration.StartTime.HasValue && Configuration.EndTime.HasValue && Configuration.HourlyFrequency.HasValue
                && Configuration.StartTime.Value.Add(new TimeSpan(Configuration.HourlyFrequency.Value, 0, 0)) > Configuration.EndTime.Value))
            {
                throw new ScheduleException(Resources.Global.TimeFrequencyConfigurationIsNotValid);
            }
        }

        private static void ValidateOnce(ScheduleConfiguration Configuration)
        {
            if ((Configuration.DateTime < Configuration.CurrentDate))
            {
                throw new ScheduleException(Resources.Global.DateTimeCanNotBeLessThanCurrentDate);
            }
        }

        private static void ValidateMonthly(ScheduleConfiguration Configuration)
        {
            if(Configuration.DayOfMonth.HasValue == false && (Configuration.MonthlyFirstOrderConfiguration.HasValue == false
                || Configuration.MonthlySecondOrdenConfiguration.HasValue == false))
            {
                throw new ScheduleException("You must set a monthly configuration when recurring type is set to monthly");
            }
            if(Configuration.DayOfMonth.HasValue && (Configuration.DayOfMonth < 1 || Configuration.DayOfMonth > 31))
            {
                throw new ScheduleException("The day of mounth must be between 1 and 31");
            }
        }

        

        private static ScheduleDateCalculator calculator
        {
            get
            {
                if (_calculator == null)
                {
                    _calculator = new ScheduleDateCalculator();
                }
                return _calculator;
            }
        }

        public static bool CurrentDayIsValid(this ScheduleConfiguration Configuration)
        {
            if (Configuration.DaysOfWeek != null && Configuration.DaysOfWeek.Length > 0 
                && (Configuration.DaysOfWeek.Contains(Configuration.CurrentDate.DayOfWeek) == false))
            {
                return false;
            }
            if (Configuration.RecurringType == RecurringTypes.Monthly)
            {
                return CurrentDayIsValidMonthlyConfiguration(Configuration);
            }
            return true;
        }

        private static bool CurrentDayIsValidMonthlyConfiguration(ScheduleConfiguration Configuration)
        {
            if (Configuration.DayOfMonth != null)
            {
                if (Configuration.CurrentDate.Day != Configuration.DayOfMonth)
                {
                    return false;
                }
            }
            else
            {
                if (((int)Configuration.MonthlySecondOrdenConfiguration) < 7 && ((int)Configuration.CurrentDate.DayOfWeek) != ((int)Configuration.MonthlySecondOrdenConfiguration))
                {
                    return false;
                }
                else
                {
                    if (Configuration.MonthlySecondOrdenConfiguration == MonthlySecondOrdenConfiguration.Weekday
                        && Configuration.CurrentDate.IsWeekendDay())
                    {
                        return false;
                    }
                    else if (Configuration.MonthlySecondOrdenConfiguration == MonthlySecondOrdenConfiguration.WeekendDay
                       && Configuration.CurrentDate.IsWeekDay())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
