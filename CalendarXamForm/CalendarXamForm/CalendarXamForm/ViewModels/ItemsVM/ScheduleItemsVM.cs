using System;
using Xamarin.Forms;

namespace CalendarXamForm.ViewModels.ItemsVM
{
    public class ScheduleItemsVM : BaseVM
    {
        public int SmallFont => (Application.ScreenHeight / 50);
        
        
        public int RowNumber { get; set; }
        public Command TapCommand { get; set; }



        public ScheduleItemsVM()
        {
            TapCommand = new Command<object>(Open_AddPage);
            
        }

        private void Open_AddPage(object obj)
        {
            DoRowSel();
        }

        


        public event EventHandler RowSel;
        private void DoRowSel()
        {
            EventHandler eh = RowSel;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }




        
    }
}
