using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendarXamForm.Model;
using CalendarXamForm.Services;
using CalendarXamForm.ViewModels.ItemsVM;
using Xamarin.Forms;

namespace CalendarXamForm.ViewModels
{
    public class AddTaskVM : BaseVM
    {
        public ObservableCollection<TaskItemVM> TaskItems { get; set; } = new ObservableCollection<TaskItemVM>();

        public DateTime CurentDateTime { get; set; } = DateTime.Now;

        private string _textMessage = "Work hours";
        public string TextMessage
        {
            get { return _textMessage; }
            set
            {
                _textMessage = value;
                OnPropertyChanged();
            }
        }


        private TimeSpan _startTime = new TimeSpan();
        public TimeSpan StartTime
        {
            get { return _startTime; }
            set
            {
                if (value >= EndTime)
                {
                    _startTime = value;
                    EndTime = _startTime.Add(new TimeSpan(1, 0, 0));
                    OnPropertyChanged();
                    DoTimeStartChanged();
                }
                else
                {
                    _startTime = value;
                    EndTime = EndTime;
                    OnPropertyChanged();
                    DoTimeStartChanged();
                }
            }
        }

        private TimeSpan _endTime = new TimeSpan();
        public TimeSpan EndTime
        {
            get { return _endTime; }
            set
            {
                if (value <= StartTime)
                {
                    _endTime = StartTime.Add(new TimeSpan(1, 0, 0));
                    OnPropertyChanged();
                    DoTimeEndChanged();
                }
                else
                {
                    _endTime = value;
                    OnPropertyChanged();
                    DoTimeEndChanged();
                }
            }
        }


        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }


        public AddTaskVM()
        {
            CancelCommand = new Command(Cancel);
            SaveCommand = new Command(Save);
        }

        private void Save()
        {
            var item = new TaskItem();
            item.DateTimeRenge = new DateTimeRenge(CurentDateTime.Date.Add(StartTime), CurentDateTime.Date.Add(EndTime));
            item.Text = TextMessage;
            var result = FakeRepo.InsertItem(item);
            Application.Current.MainPage.DisplayAlert("Result", result.ToString(), "OK");

            DoCloseWindow();
        }

        private void Cancel()
        {
            DoCloseWindow();
        }


        public void StartTimeChange(object sender, EventArgs e)
        {
            var schedule = sender as ScheduleVM;
            StartTime = schedule.StartTime;
            CurentDateTime = schedule.CurentDateDay;
        }


        // change start time
        public event EventHandler TimeStartChanged;
        private void DoTimeStartChanged()
        {
            EventHandler eh = TimeStartChanged;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }

        // change end time
        public event EventHandler TimeEndChanged;
        private void DoTimeEndChanged()
        {
            EventHandler eh = TimeEndChanged;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }


        // CloseWindow
        public event EventHandler CloseWindow;
        private void DoCloseWindow()
        {
            EventHandler eh = CloseWindow;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }
    }
}
