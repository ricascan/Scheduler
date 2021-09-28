using System;

namespace Scheduler
{
    public class Schedule
    {
        private ScheduleConfiguration _configuration;
        private ScheduleDateCalculator _calculator;
        private DateTime currentDate;

        public Schedule(ScheduleConfiguration Configuration, ScheduleDateCalculator Calculator)
        {
            this._configuration = Configuration;
            this._calculator = Calculator;
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
    }
}
