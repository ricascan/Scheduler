using System;

namespace Scheduler
{
    public class Schedule
    {
        private ScheduleConfiguration _configuration;
        private ScheduleDateCalculator _calculator;
        public DateTime CurrentDate { get; set; }
            


        public Schedule()
        {
            this.Configuration.StartDate = new DateTime(DateTime.Now.Year, 1, 1);
            this.Configuration.ScheduleType = ScheduleTypes.Recurring;
            this.Configuration.RecurringType = RecurringTypes.Daily;
            this.CurrentDate = DateTime.Now;
        }

        public Schedule(ScheduleConfiguration Configuration)
        {
            this._configuration = Configuration;
        }

        public ScheduleOutputData GetNextExecutionDate()
        {
            if(this.DateInInterval(this.CurrentDate, this.Configuration.StartDate, this.Configuration.EndDate) == false)
            {
                throw new ScheduleException("Current Date is out of the limits set");
            }
            this.calculator.CalculateNextDateTime(this.CurrentDate, this.Configuration);
            if(this.DateInInterval(this.calculator.OutputData.OutputDateTime.Value, this.Configuration.StartDate, this.Configuration.EndDate) == false)
            {
                throw new ScheduleException("It is not possible to generate a execution date within the current date and limits set");
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
       

        private bool DateInInterval(DateTime Date, DateTime? StartDate, DateTime? EndDate)
            => (StartDate.HasValue == false || Date >= StartDate)
                && (EndDate.HasValue == false || Date < EndDate);
    }
}
