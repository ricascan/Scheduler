using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Extenders
{
    public static class TimeSpanExtender
    {
        public static bool IsInInterval(this TimeSpan Time, TimeSpan? StartTime, TimeSpan? EndTime)
            => (StartTime.HasValue == false || Time >= StartTime)
                && (EndTime.HasValue == false || Time < EndTime);
    }
}
