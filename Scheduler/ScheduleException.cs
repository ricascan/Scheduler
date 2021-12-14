using System;

namespace Scheduler
{
    public class ScheduleException : ApplicationException
    {
        public ScheduleException(string Message) : base (Message)
        {

        }
    }
}
