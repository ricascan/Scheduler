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
        Daily = 2,
        Weekly = 3,
        Monthly = 4
    }

    public enum MonthlyFirstOrderConfiguration
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Last = 31
    }

    public enum MonthlySecondOrdenConfiguration
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Day = 7,
        Weekday = 8,
        WeekendDay = 9
    }
    public static class EnumerationsDescriptionManager
    {
        public static string GetRecurringTypeDescription(RecurringTypes Type)
            => new ResourceManager(typeof(Resources.Global)).GetString("RecurringTypes_" + (int)Type);

        public static string GetRecurringTypeUnitDescription(RecurringTypes Type)
            => new ResourceManager(typeof(Resources.Global)).GetString("RecurringTypesUnits_" + (int)Type);
    }
}
