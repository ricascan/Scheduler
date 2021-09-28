﻿using System;

namespace Scheduler
{
    public class Schedule
    {
        private ScheduleConfiguration _configuration;
        private ScheduleDateCalculator _calculator;
        public DateTime CurrentDate { get; set; }


        public Schedule()
        {

        }

        public Schedule(ScheduleConfiguration Configuration)
        {
            this._configuration = Configuration;
        }

        public DateTime GetNextExecutionDate()
        {
            if(this.DateInInterval(this.CurrentDate, this.configuration.StartDate, this.configuration.EndDate) == false)
            {
                throw new ScheduleException("Current Date is out of the limits set");
            }
            this.calculator.CalculateNextDateTime(this.CurrentDate, this.configuration);
            if(this.DateInInterval(this.calculator.OutputDateTime.Value, this.configuration.StartDate, this.configuration.EndDate) == false)
            {
                throw new ScheduleException("It is not possible to generate a execution date within the current date and limits set");
            }
            return calculator.OutputDateTime.Value;
        }

        

        private ScheduleConfiguration configuration
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
       

        private bool DateInInterval(DateTime Date, DateTime? StartDate, DateTime? EndDate)
            => (StartDate.HasValue == false || Date >= StartDate)
                && (EndDate.HasValue == false || Date < EndDate);
    }
}
