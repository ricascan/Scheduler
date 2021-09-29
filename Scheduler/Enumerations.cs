using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
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
    public static class EnumerationsDescriptionManager
    {
        public static string GetRecurringTypeDescription(RecurringTypes Type)
            => new ResourceManager(typeof(Resources.Global)).GetString("RecurringTypes_" + (int)Type);

        public static string GetRecurringTypeUnitDescription(RecurringTypes Type)
            => new ResourceManager(typeof(Resources.Global)).GetString("RecurringTypesUnits_" + (int)Type);
    }
}
