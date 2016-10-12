using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalendarXamForm.Model
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Text { get; set; }


        public int RowStart => DateTimeRenge.Start.Hour;
        public int RowEnd => (DateTimeRenge.End.Hour - DateTimeRenge.Start.Hour);

        public Color Color { get; set; } = Xamarin.Forms.Color.FromHex("#A4D7FF");

        public DateTimeRenge DateTimeRenge { get; set; }
    }
}
