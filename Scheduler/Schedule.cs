using System;

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
            this.Configuration.Validate();          
            this.calculator.CalculateNextDateTime(this.Configuration.CurrentDate, this.Configuration);
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
       

        
    }
}
