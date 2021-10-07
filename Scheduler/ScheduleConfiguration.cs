using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class ScheduleConfiguration
    {
        private int frequency = 1;
        private DateTime dateTime = DateTime.Now;

        public ScheduleTypes ScheduleType { get; set; } = ScheduleTypes.Recurring;
        public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);
        public DateTime? EndDate { get; set; } = null;
        public RecurringTypes RecurringType { get; set; } = RecurringTypes.Daily;

        public DateTime CurrentDate { get; set; } = DateTime.Now;

        public DateTime DateTime
        {
            get => this.dateTime;
            set
            {
                if(value < this.CurrentDate)
                {
                    throw new ScheduleException(Resources.Global.DateTimeCanNotBeLessThanCurrentDate);
                }
                this.dateTime = value;
            }
        }

        public int Frequency
        {
            get => this.frequency;
            set
            {
                if(value < 0)
                {
                    throw new ScheduleException(Resources.Global.FrequencyMustBeGraterThanZero);
                }
                this.frequency = value;
            }
        } 

        public void Validate()
        {
            if(this.EndDate < this.StartDate)
            {
                throw new ScheduleException(Resources.Global.StartDateGreaterThanEndDate);
            }
            if (ScheduleDateCalculator.DateInInterval(this.CurrentDate, this.StartDate, this.EndDate) == false)
            {
                throw new ScheduleException(Resources.Global.CurrentDateOutLimits);
            }
            if(this.ScheduleType == ScheduleTypes.Once && (this.DateTime < this.CurrentDate))
            {
                throw new ScheduleException(Resources.Global.DateTimeCanNotBeLessThanCurrentDate);
            }
        }

        
    }
}
