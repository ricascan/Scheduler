using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class ScheduleConfiguration
    {       
        private RecurringTypes? recurringType;
        private DateTime? dateTime;
        private int? frequency;

        public ScheduleTypes ScheduleType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public RecurringTypes? RecurringType
        {
            get => this.recurringType;
            set
            {
                if (this.ScheduleType != ScheduleTypes.Recurring)
                {
                    throw new ScheduleException("You can not set a Recurring Type if Schedule Type is not set to Recurring");
                }
                this.recurringType = value;
            }
        }

        public DateTime? DateTime
        {
            get => this.dateTime;
            set
            {
                if(this.ScheduleType != ScheduleTypes.Once)
                {
                    throw new ScheduleException("You can not set a date if Schedule Type is not set to once");
                }
                this.dateTime = value;
            }
        }

        public int? Frequency
        {
            get => this.frequency;
            set
            {
                if(this.ScheduleType != ScheduleTypes.Recurring)
                {
                    throw new ScheduleException("You can not set a frequency if Schedule Type is not set to recurring");
                }
                this.frequency = value;
            }
        }
    }
}
