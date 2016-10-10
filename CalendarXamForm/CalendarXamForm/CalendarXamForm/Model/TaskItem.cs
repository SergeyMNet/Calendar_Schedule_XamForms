using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarXamForm.Model
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTimeRenge DateTimeRenge { get; set; }
    }
}
