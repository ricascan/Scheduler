using Scheduler.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Extenders
{
    public static class SchedulerExtender
    {
        private static ScheduleDateCalculator _calculator;
        public static ScheduleOutputData GetNextExecutionDate(this Scheduler Configuration)
        {
            InitializeResourcesManager(Configuration);
            Validate(Configuration);
            ScheduleOutputData OutputData = calculator.CalculateNextDateTime(Configuration);
            if (OutputData.OutputDateTime.Value.IsInInterval(
                Configuration.StartDate, Configuration.EndDate) == false)
            {
                throw new ScheduleException(SchedulerResourcesManager.GetResource("NotPossibleToGenerateExecDate"));
            }
            return OutputData;
        }

        private static void InitializeResourcesManager(Scheduler Configuration)
        {
            if (string.IsNullOrWhiteSpace(Configuration.Language) == false)
            {
                SchedulerResourcesManager.InitializeResourcesManager(Configuration.Language);
            }
            else
            {
                SchedulerResourcesManager.InitializeResourcesManager("en-GB");
            }
        }

        public static ScheduleOutputData[] GetNextExecutionDateSeries(this Scheduler Configuration, int NumberOfSeries)
        {
            var OutputData = new List<ScheduleOutputData>();
            for(int i = 0; i < NumberOfSeries; i++)
            {
                OutputData.Add(Configuration.GetNextExecutionDate());
                Configuration.CurrentDate = OutputData[^1].OutputDateTime.Value;
            }
            return OutputData.ToArray();
        }

        public static void OrderDaysOfWeek(this Scheduler Configuration)
        {
            if (Configuration.DaysOfWeek != null)
            {
                Configuration.DaysOfWeek = Configuration.DaysOfWeek.OrderBy(D => (int)D).ToArray();
            }
        }

        private static void Validate(Scheduler Configuration)
        {
            if (Configuration.EndDate < Configuration.StartDate)
            {
                throw new ScheduleException(SchedulerResourcesManager.GetResource("StartDateGreaterThanEndDate"));
            }
            if (Configuration.CurrentDate.IsInInterval(Configuration.StartDate, Configuration.EndDate) == false)
            {
                throw new ScheduleException(SchedulerResourcesManager.GetResource("CurrentDateOutLimits"));
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

        private static void ValidateRecurring(Scheduler Configuration)
        {
            if ((Configuration.Frequency < 0 || Configuration.HourlyFrequency < 0))
            {
                throw new ScheduleException(SchedulerResourcesManager.GetResource("FrequencyMustBeGraterThanZero"));
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

        private static void ValidateWeekly(Scheduler Configuration)
        {
            if ((Configuration.DaysOfWeek == null || Configuration.DaysOfWeek.Length < 1))
            {
                throw new ScheduleException(SchedulerResourcesManager.GetResource("MustSetAtLeastOneDayWeek"));
            }

            if (Configuration.DaysOfWeek != null
                && Configuration.DaysOfWeek.Distinct().Count() != Configuration.DaysOfWeek.Length)
            {
                throw new ScheduleException(SchedulerResourcesManager.GetResource("DaysOfWeekCanNotBeRepeated"));
            }
        }

        private static void ValidateHourly(Scheduler Configuration)
        {
            if (Configuration.HourlyFrequency.HasValue && (Configuration.StartTime.HasValue == false || Configuration.EndTime.HasValue == false))
            {
                throw new ScheduleException(SchedulerResourcesManager.GetResource("MustSetStartEndTimesWhenFrequency"));
            }
            if ((Configuration.StartTime.HasValue || Configuration.EndTime.HasValue) && Configuration.HourlyFrequency.HasValue == false)
            {
                throw new ScheduleException(SchedulerResourcesManager.GetResource("MustSetHourlyFrequencyWhenStartEndTimes"));
            }
            if ((Configuration.StartTime.HasValue && Configuration.EndTime.HasValue && Configuration.EndTime.Value < Configuration.StartTime.Value))
            {
                throw new ScheduleException(SchedulerResourcesManager.GetResource("EndTimeCanNotBeLessStartTime"));
            }
            if ((Configuration.StartTime.HasValue && Configuration.EndTime.HasValue && Configuration.HourlyFrequency.HasValue
                && Configuration.StartTime.Value.Add(new TimeSpan(Configuration.HourlyFrequency.Value, 0, 0)) > Configuration.EndTime.Value))
            {
                throw new ScheduleException(SchedulerResourcesManager.GetResource("TimeFrequencyConfigurationIsNotValid"));
            }
        }

        private static void ValidateOnce(Scheduler Configuration)
        {
            if ((Configuration.DateTime < Configuration.CurrentDate))
            {
                throw new ScheduleException(SchedulerResourcesManager.GetResource("DateTimeCanNotBeLessThanCurrentDate"));
            }
        }

        private static void ValidateMonthly(Scheduler Configuration)
        {
            if(Configuration.DayOfMonth.HasValue == false && (Configuration.MonthlyFirstOrderConfiguration.HasValue == false
                || Configuration.MonthlySecondOrdenConfiguration.HasValue == false))
            {
                throw new ScheduleException(SchedulerResourcesManager.GetResource("MustSetMonthlyConfiguration"));
            }
            if(Configuration.DayOfMonth.HasValue && (Configuration.DayOfMonth < 1 || Configuration.DayOfMonth > 31))
            {
                throw new ScheduleException(SchedulerResourcesManager.GetResource("InvalidDayOfMounth"));
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

        public static bool CurrentDayIsValid(this Scheduler Configuration)
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

        private static bool CurrentDayIsValidMonthlyConfiguration(Scheduler Configuration)
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
                    if (Configuration.MonthlySecondOrdenConfiguration == MonthlySecondOrderConfiguration.Weekday
                        && Configuration.CurrentDate.IsWeekendDay())
                    {
                        return false;
                    }
                    else if (Configuration.MonthlySecondOrdenConfiguration == MonthlySecondOrderConfiguration.WeekendDay
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
