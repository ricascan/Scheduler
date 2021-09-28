using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class ScheduleException : ApplicationException
    {
        public ScheduleException(string Message) : base (Message)
        {

        }

        public ScheduleException() : base()
        {

        }
    }
}
