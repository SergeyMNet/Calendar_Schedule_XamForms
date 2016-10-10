using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarXamForm.Model
{
    public class DateTimeRenge
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public DateTimeRenge(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTimeRenge(DateTime start, TimeSpan duration)
        {
            Start = start;
            End = start.Add(duration);
        }

        public int DurationInHours()
        {
            return (End - Start).Hours;
        }


        public DateTimeRenge NewStart(DateTime d)
        {
            return new DateTimeRenge(d, End);
        }
        public DateTimeRenge NewEnd(DateTime d)
        {
            return new DateTimeRenge(Start, d);
        }
        public DateTimeRenge NewDuration(TimeSpan t)
        {
            return new DateTimeRenge(Start, t);
        }
    }
}
