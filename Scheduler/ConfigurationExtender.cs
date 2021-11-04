using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public static class ConfigurationExtender
    {
        private static ScheduleDateCalculator _calculator;
        public static ScheduleOutputData GetNextExecutionDate(this ScheduleConfiguration Configuration)
        {
            Configuration.Validate();
            calculator.CalculateNextDateTime(Configuration);
            if (DateInInterval(calculator.OutputData.OutputDateTime.Value,
                Configuration.StartDate, Configuration.EndDate) == false)
            {
                throw new ScheduleException(Resources.Global.NotPossibleToGenerateExecDate);
            }
            return calculator.OutputData;
        }

        private static void Validate(this ScheduleConfiguration Configuration)
        {
            if (Configuration.EndDate < Configuration.StartDate)
            {
                throw new ScheduleException(Resources.Global.StartDateGreaterThanEndDate);
            }
            if (DateInInterval(Configuration.CurrentDate,Configuration.StartDate, Configuration.EndDate) == false)
            {
                throw new ScheduleException(Resources.Global.CurrentDateOutLimits);
            }
            switch (Configuration.ScheduleType)
            {
                case ScheduleTypes.Once:
                    Configuration.ValidateOnce();
                    break;
                case ScheduleTypes.Recurring:
                    Configuration.ValidateRecurring();
                    break;
            }
        }

        private static void ValidateRecurring(this ScheduleConfiguration Configuration)
        {
            if ((Configuration.Frequency < 0 || Configuration.HourlyFrequency < 0))
            {
                throw new ScheduleException(Resources.Global.FrequencyMustBeGraterThanZero);
            }
            Configuration.ValidateHourly();
            if (Configuration.RecurringType == RecurringTypes.Weekly)
            {
                Configuration.ValidateWeekly();
            }
        }

        private static void ValidateWeekly(this ScheduleConfiguration Configuration)
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

        private static void ValidateHourly(this ScheduleConfiguration Configuration)
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

        private static void ValidateOnce(this ScheduleConfiguration Configuration)
        {
            if ((Configuration.DateTime < Configuration.CurrentDate))
            {
                throw new ScheduleException(Resources.Global.DateTimeCanNotBeLessThanCurrentDate);
            }
        }

        private static bool DateInInterval(DateTime Date, DateTime? StartDate, DateTime? EndDate)
            => (StartDate.HasValue == false || Date >= StartDate)
                && (EndDate.HasValue == false || Date < EndDate);

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
    }
}
