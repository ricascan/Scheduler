using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class ScheduleDateCalculator
    {
        public DateTime? OutputDateTime { get; private set; }
        public string OutputDescription { get; private set; }
        public void CalculateDateTime(DateTime CurrentDate, ScheduleConfiguration Configuration)
        {
            switch (Configuration.ScheduleType)
            {
                case ScheduleTypes.Once:
                    this.OutputDateTime = Configuration.DateTime;
                    break;
                case ScheduleTypes.Recurring:
                    break;
                default:
                    break;
            }
        }
    }
}
