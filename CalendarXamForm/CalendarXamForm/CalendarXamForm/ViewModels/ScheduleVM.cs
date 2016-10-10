using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendarXamForm.ViewModels.ItemsVM;

namespace CalendarXamForm.ViewModels
{
    public class ScheduleVM : BaseVM
    {
        public int RowHeight => 35;
        
        public List<ScheduleItemsVM> ScheduleRows
        {
            get
            {
                var result = new List<ScheduleItemsVM>();

                for (int i = 0; i < 24; i++)
                {
                    var item = new ScheduleItemsVM();
                    item.RowNumber = i;
                    result.Add(item);
                }

                return result;
            }
        }


        public bool IsShowFrame { get; set; }
        public int SelectedRow { get; set; }
        public int EndRow { get; set; }
        
        public TimeSpan StartTime { get; set; }

        public ScheduleVM()
        {
            IsShowFrame = false;
            SelectedRow = 0;
            EndRow = 1;
            StartTime = new TimeSpan();
        }



        public void SelRow(object sender, EventArgs e)
        {
            var selItem = sender as ScheduleItemsVM;
            var h = selItem.RowNumber.ToString();
            var hh = Int32.Parse(h);

            StartTime = new TimeSpan(hh, 0, 0);
            SelectedRow = hh;
            IsShowFrame = true;
            OnPropertyChanged("StartTime");
            OnPropertyChanged("SelectedRow");
            OnPropertyChanged("IsShowFrame");
            DoTimeChanged();
        }


        public void DayChanged(object sender, EventArgs e)
        {
            var calendar = sender as CalendarVM;
            Debug.WriteLine("-----sel date = " + calendar.SelDate);

            // TODO изменился день - изменить расписание
        }



        // change sel row
        public event EventHandler TimeChanged;
        private void DoTimeChanged()
        {
            EventHandler eh = TimeChanged;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }


        public void TimeStartChanged(object sender, EventArgs e)
        {
            var t = sender as AddTaskVM;
            
            SelectedRow = t.StartTime.Hours;
            OnPropertyChanged("SelectedRow");

        }

        public void TimeEndChanged(object sender, EventArgs e)
        {
            var t = sender as AddTaskVM;

            EndRow = t.EndTime.Hours - t.StartTime.Hours;

            OnPropertyChanged("EndRow");

            ReloadFrame();
        }

        private async void ReloadFrame()
        {
            IsShowFrame = false;
            OnPropertyChanged("IsShowFrame");

            await Task.Delay(200);

            IsShowFrame = true;
            OnPropertyChanged("IsShowFrame");
        }

        public void ClearFrame(object sender, EventArgs e)
        {
            IsShowFrame = false;
            OnPropertyChanged("IsShowFrame");
        }
    }
}
