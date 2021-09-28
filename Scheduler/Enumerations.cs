using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public enum ScheduleTypes
    {
        Once = 1,
        Recurring = 2
    }

    public enum RecurringTypes
    {      
        Hourly = 1,
        Daily = 2,
        Weekly = 3,
        Mounthly = 4,
        Yearly = 5
    }
}
