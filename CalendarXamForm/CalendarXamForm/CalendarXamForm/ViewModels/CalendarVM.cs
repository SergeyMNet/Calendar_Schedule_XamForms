using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendarXamForm.Model;
using CalendarXamForm.Pages;
using CalendarXamForm.Services;
using CalendarXamForm.Utilities;
using CalendarXamForm.ViewModels.ItemsVM;
using Xamarin.Forms;

namespace CalendarXamForm.ViewModels
{
    public class CalendarVM : BaseVM
    {
        int targetMonth = DateTime.Now.Month;
        int targetYear = DateTime.Now.Year;
        int _oldSelItem;


        public ObservableCollection<ItemVM> Items { get; set; } = new ObservableCollection<ItemVM>();
        

        private string _curentMonthName;
        public string CurentMonthName
        {
            get { return _curentMonthName; }
            set
            {
                _curentMonthName = value;
                OnPropertyChanged();
            }
        }

        private DateTime _selDate;
        public DateTime SelDate
        {
            get { return _selDate; }
            set
            {
                _selDate = value;
                OnPropertyChanged();
            }
        }


        public Command TapBack { get; set; }
        public Command TapForward { get; set; }
        

        public CalendarVM()
        {
            TapBack = new Command(TapBackDo);
            TapForward = new Command(TapForwardDo);
            
            StartPrepare();
        }

        

        /// <summary>
        /// start rendering vm
        /// </summary>
        private async void StartPrepare()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                await PrepareCalendarGrid();
                IsBusy = false;
            }
        }

        /// <summary>
        /// Tap previus month
        /// </summary>
        private async void TapForwardDo()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                
                if (targetMonth < 12)
                {
                    targetMonth++;
                }
                else
                {
                    targetMonth = 1;
                    targetYear++;
                }

                await PrepareCalendarGrid(targetMonth, targetYear);
                
                IsBusy = false; 
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Tap next month
        /// </summary>
        private async void TapBackDo()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                
                if (targetMonth > 1)
                {
                    targetMonth--;
                }
                else
                {
                    targetMonth = 12;
                    targetYear--;
                }

                await PrepareCalendarGrid(targetMonth, targetYear);
                IsBusy = false; 
            }
            else
            {
                return;
            }
        }
        

        #region Events

        // start change items
        public event EventHandler MonthStartChange;
        public void DoItemsStartChange()
        {
            EventHandler eh = MonthStartChange;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }

        // end change items
        public event EventHandler MonthChanged;
        public void DoItemsChanged()
        {
            EventHandler eh = MonthChanged;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }


        // change sel date
        public event EventHandler DayChanged;
        public void DoDateChanged()
        {
            EventHandler eh = DayChanged;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }

        #endregion

        
        /// <summary>
        /// Prepare calendar grid
        /// </summary>
        /// <param name="targetM"></param>
        /// <param name="targetY"></param>
        /// <returns></returns>
        private async Task PrepareCalendarGrid(int targetM = 0, int targetY = 0)
        {
            DoItemsStartChange();
            await Task.Delay(500);

            #region Prepare target month and Year

            if (targetM != 0)
                targetMonth = targetM;

            if (targetY != 0)
                targetYear = targetY;

            CurentMonthName = new DateTime(targetYear, targetMonth, 1).ToString("MMMM") + " " + targetYear; // DateTime.Parse(String.Format("{0}/{1}/{2}", targetMonth, 1, targetYear)).ToString("MMMM") + " " + targetYear;

            #endregion

            Items.Clear();

            bool IsStartDate = false;
            int day = 1;
            int dayNext = 1;

            double maxDay = DateTime.DaysInMonth(targetYear, targetMonth);
            int rows = 6;


            var startDay = new DateTime(targetYear, targetMonth, 1);// DateTime.Parse(string.Format("{0}/{1}/{2}", targetMonth, 1, targetYear));
            var startDayDayOfWeek = (int)startDay.DayOfWeek;
            if (startDayDayOfWeek == 0)
            {
                startDayDayOfWeek = 1;
            }

            #region Prepare days before

            int dayBef;
            int yearBef;
            if (targetMonth > 1)
            {
                dayBef = targetMonth - 1;
                yearBef = targetYear;
            }
            else
            {
                dayBef = 12;
                yearBef = targetYear - 1;
            }

            double monthBefore_MaxDay = DateTime.DaysInMonth(yearBef, dayBef);
            //int daysBefore = (int)(monthBefore_MaxDay - (7 - startDayDayOfWeek)) + 1;
            int daysBefore = 7;
            if (startDayDayOfWeek != 1)
            {
                daysBefore = (int)(monthBefore_MaxDay - (startDayDayOfWeek - 1)) + 1;
            }

            #endregion

            for (int j = 0; j < rows; j++)
            {
                for (int i = 1; i <= 7; i++)
                {
                    if ((startDayDayOfWeek == i || IsStartDate) && day <= (int)maxDay)
                    {
                        var newDay = new ItemVM(day.ToString(), i.ToString(), (i - 1), j);
                        newDay.Date = new DateTime(targetYear, targetMonth, day); // DateTime.Parse(string.Format("{0}/{1}/{2}", targetMonth, day, targetYear));
                        newDay.IsReal = true;
                        newDay.DateSel += NewDay_DateSel;
                        newDay.Id = day;
                        if (DateTime.Now.Date == newDay.Date)
                        {
                            newDay.IsSelected = true;
                            _oldSelItem = day;
                        }

                        Items.Add(newDay);
                        day++;
                        IsStartDate = true;
                    }
                    else
                    {
                        if (IsStartDate)
                        {
                            Items.Add(new ItemVM(dayNext.ToString(), i.ToString(), (i - 1), j));
                            dayNext++;
                        }
                        else
                        {
                            Items.Add(new ItemVM(daysBefore.ToString(), i.ToString(), (i - 1), j));
                            daysBefore++;
                        }


                    }
                }
            }

            DoItemsChanged();
        }
        
        /// <summary>
        /// Select item changed - listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NewDay_DateSel(object sender, EventArgs e)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                
                ItemVM item = sender as ItemVM;
                
                if (item != null)
                {
                    DoDateChanged();
                    await ChangeItemSelected(item);
                    DoDateChanged();
                }
                IsBusy = false;
            }
        }

        /// <summary>
        /// Sel item changed - Async
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private async Task ChangeItemSelected(ItemVM item)
        {
            var selItem = Items.FirstOrDefault(it => it.Id == item.Id);
            selItem.IsSelected = true;
            if (_oldSelItem != 0)
            {
                var olditem = Items.FirstOrDefault(it => it.Id == _oldSelItem);
                olditem.IsSelected = false;
            }
            _oldSelItem = item.Id;

            SelDate = selItem.Date;
        }
    }
}
