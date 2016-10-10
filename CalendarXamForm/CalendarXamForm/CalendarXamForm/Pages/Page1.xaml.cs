using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CalendarXamForm.ViewModels;
using Xamarin.Forms;

namespace CalendarXamForm.Pages
{
    public partial class Page1 : ContentPage
    {
        public MainScreenVM MainScreenVm { get; set; }
        public Page1()
        {
            InitializeComponent();
            MainScreenVm = new MainScreenVM();
            Content.BindingContext = MainScreenVm;

            MainScreenVm.CalendarVm.MonthStartChange += MonthStartChanging;
            MainScreenVm.CalendarVm.MonthChanged += MonthChanged;
            MainScreenVm.CalendarVm.DayChanged += MainScreenVm.ScheduleVm.DayChanged;

            MainScreenVm.ScheduleVm.TimeChanged += MainScreenVm.CloseCalendar;
            MainScreenVm.ScheduleVm.TimeChanged += MainScreenVm.OpenAddPage;
            MainScreenVm.ScheduleVm.TimeChanged += MainScreenVm.AddTaskVm.StartTimeChange;

            MainScreenVm.AddTaskVm.TimeStartChanged += MainScreenVm.ScheduleVm.TimeStartChanged;
            MainScreenVm.AddTaskVm.TimeEndChanged += MainScreenVm.ScheduleVm.TimeEndChanged;

            MainScreenVm.AddTaskVm.CloseWindow += MainScreenVm.ScheduleVm.ClearFrame;
            MainScreenVm.AddTaskVm.CloseWindow += MainScreenVm.CloseAddPage;
            MainScreenVm.AddTaskVm.CloseWindow += MainScreenVm.OpenCalendar;

            PrepareGrid();
            PrepareTaskGrig();
            
        }
        

        private async void MonthStartChanging(object sender, EventArgs e)
        {
            await MainGrid.FadeTo(0, 500, Easing.Linear);
        }
        private async void MonthChanged(object sender, EventArgs e)
        {
            await PrepareGrid();
            await MainGrid.FadeTo(1, 500, Easing.Linear);
        }


        /// <summary>
        /// Подготовить календарь
        /// </summary>
        /// <returns></returns>
        private async Task PrepareGrid()
        {
            MainGrid.Children.Clear();
            Debug.WriteLine("----------CalendarVm.Items.Count = " + MainScreenVm.CalendarVm.Items.Count);
            foreach (var item in MainScreenVm.CalendarVm.Items)
            {
                var mod = (DataTemplate)Xamarin.Forms.Application.Current.Resources["ItemTemplate"];
                var content = mod.CreateContent() as StackLayout;
                content.BindingContext = item;
                MainGrid.Children.Add(content);
            }
        }

        /// <summary>
        /// Подготовить таблицу задания на день
        /// </summary>
        /// <returns></returns>
        private async Task PrepareTaskGrig()
        {
            Schedule.Children.Clear();

            foreach (var scheduleItem in MainScreenVm.ScheduleVm.ScheduleRows)
            {
                scheduleItem.RowSel += MainScreenVm.ScheduleVm.SelRow;
                var mod = (DataTemplate)Xamarin.Forms.Application.Current.Resources["ItemTaskTemplate"];
                var content = mod.CreateContent() as Grid;
                content.BindingContext = scheduleItem;
                Schedule.Children.Add(content);
            }

            AddSelectFrame();
        }


        private void AddSelectFrame()
        {
            var mod = (DataTemplate) Xamarin.Forms.Application.Current.Resources["ItemBoxTemplate"];
            var content = mod.CreateContent() as StackLayout;
            content.BindingContext = MainScreenVm.ScheduleVm;
            Schedule.Children.Add(content);
        }
        
        
    }
}
