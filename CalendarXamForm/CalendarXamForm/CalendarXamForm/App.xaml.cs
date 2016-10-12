using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendarXamForm.Model;
using CalendarXamForm.Pages;
using CalendarXamForm.Services;
using Xamarin.Forms;

namespace CalendarXamForm
{
    public partial class Application : Xamarin.Forms.Application
    {
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }

        public Application()
        {
            InitializeComponent();

            if (ScreenWidth == 0 || ScreenHeight == 0)
            {
                ScreenWidth = 600;
                ScreenHeight = 800;
            }

            FakeRepo.InsertItem(new TaskItem()
            {
                DateTimeRenge = new DateTimeRenge(DateTime.Now, DateTime.Now.Add(new TimeSpan(2, 0, 0))),
                Text = "some text ..."
            });
            FakeRepo.InsertItem(new TaskItem()
            {
                DateTimeRenge = new DateTimeRenge(DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).Add(new TimeSpan(2, 0, 0))),
                Text = "some text ..."
            });

            MainPage = new Page1();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
