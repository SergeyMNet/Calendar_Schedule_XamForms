using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CalendarXamForm.ViewModels
{
    public class MainScreenVM : BaseVM
    {
        public int LargeFont => (Application.ScreenHeight / 40);
        public int MediumFont => (Application.ScreenHeight / 45);
        public int SmallFont => (Application.ScreenHeight / 50);

        

        public CalendarVM CalendarVm { get; set; } = new CalendarVM();
        public ScheduleVM ScheduleVm { get; set; } = new ScheduleVM();
        public AddTaskVM AddTaskVm { get; set; } = new AddTaskVM();


        private bool _isCalendar = true;
        public bool IsCalendar
        {
            get { return _isCalendar; }
            set
            {
                _isCalendar = value;
                OnPropertyChanged();
            }
        }

        private bool _isTaskEdit = false;
        public bool IsTaskEdit
        {
            get { return _isTaskEdit; }
            set
            {
                _isTaskEdit = value;
                OnPropertyChanged();
            }
        }


        public MainScreenVM()
        {
            IsCalendar = true;
        }

        public void CloseCalendar(object sender, EventArgs e)
        {
            if(IsCalendar)
                IsCalendar = false;
        }

        public void OpenAddPage(object sender, EventArgs e)
        {
            if(!IsTaskEdit)
                IsTaskEdit = true;
        }

        public void OpenCalendar(object sender, EventArgs e)
        {
            if (!IsCalendar)
                IsCalendar = true;
        }

        public void CloseAddPage(object sender, EventArgs e)
        {
            if (IsTaskEdit)
                IsTaskEdit = false;
        }
    }
}
