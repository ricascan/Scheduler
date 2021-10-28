using System;
using System.Linq;

namespace Scheduler
{
    public class Schedule
    {
        private ScheduleConfiguration _configuration;
        private ScheduleDateCalculator _calculator;
        



        public Schedule()
        {
        }

        public Schedule(ScheduleConfiguration Configuration)
        {
            this._configuration = Configuration;
        }

        public ScheduleOutputData GetNextExecutionDate()
        {
            this.Validate();          
            this.calculator.CalculateNextDateTime(this.Configuration);
            if(ScheduleDateCalculator.DateInInterval(this.calculator.OutputData.OutputDateTime.Value, 
                this.Configuration.StartDate, this.Configuration.EndDate) == false)
            {
                throw new ScheduleException(Resources.Global.NotPossibleToGenerateExecDate);
            }
            return this.calculator.OutputData;
        }

        

        public ScheduleConfiguration Configuration
        {
            get
            {
                if(this._configuration == null)
                {
                    this._configuration = new ScheduleConfiguration();
                }
                return this._configuration;
            }
        }

        private ScheduleDateCalculator calculator
        {
            get
            {
                if(this._calculator == null)
                {
                    this._calculator = new ScheduleDateCalculator();
                }
                return this._calculator;
            }
        }

        private void Validate()
        {
            if (this.Configuration.EndDate < this.Configuration.StartDate)
            {
                throw new ScheduleException(Resources.Global.StartDateGreaterThanEndDate);
            }
            if (ScheduleDateCalculator.DateInInterval(this.Configuration.CurrentDate, this.Configuration.StartDate, this.Configuration.EndDate) == false)
            {
                throw new ScheduleException(Resources.Global.CurrentDateOutLimits);
            }
            if (this.Configuration.ScheduleType == ScheduleTypes.Once && (this.Configuration.DateTime < this.Configuration.CurrentDate))
            {
                throw new ScheduleException(Resources.Global.DateTimeCanNotBeLessThanCurrentDate);
            }
            if(this.Configuration.ScheduleType == ScheduleTypes.Recurring && (this.Configuration.DailyFrequency < 0 || this.Configuration.HourlyFrequency < 0))
            {
                throw new ScheduleException(Resources.Global.FrequencyMustBeGraterThanZero);
            }
            if(this.Configuration.ScheduleType == ScheduleTypes.Recurring && this.Configuration.RecurringType == RecurringTypes.Weekly 
                && (this.Configuration.DaysOfWeek == null || this.Configuration.DaysOfWeek.Length < 1))
            {
                throw new ScheduleException(Resources.Global.MustSetAtLeastOneDayWeek);
            }
            if(this.Configuration.HourlyFrequency.HasValue && (this.Configuration.StartTime.HasValue == false || this.Configuration.EndTime.HasValue == false))
            {
                throw new ScheduleException(Resources.Global.MustSetStartEndTimesWhenFrequency);
            }
            if((this.Configuration.StartTime.HasValue || this.Configuration.EndTime.HasValue) && this.Configuration.HourlyFrequency.HasValue == false)
            {
                throw new ScheduleException(Resources.Global.MustSetHourlyFrequencyWhenStartEndTimes);
            }
            if((this.Configuration.StartTime.HasValue && this.Configuration.EndTime.HasValue && this.Configuration.EndTime.Value < this.Configuration.StartTime.Value))
            {
                throw new ScheduleException(Resources.Global.EndTimeCanNotBeLessStartTime);
            }
            if ((this.Configuration.StartTime.HasValue && this.Configuration.EndTime.HasValue && this.Configuration.HourlyFrequency.HasValue 
                && this.Configuration.StartTime.Value.Add(new TimeSpan(this.Configuration.HourlyFrequency.Value, 0, 0)) > this.Configuration.EndTime.Value))
            {
                throw new ScheduleException(Resources.Global.TimeFrequencyConfigurationIsNotValid);
            }
            if(this.Configuration.DaysOfWeek != null 
                && this.Configuration.DaysOfWeek.Distinct().Count() != this.Configuration.DaysOfWeek.Length)
            {
                throw new ScheduleException(Resources.Global.DaysOfWeekCanNotBeRepeated);
            }
        }

    }
}
