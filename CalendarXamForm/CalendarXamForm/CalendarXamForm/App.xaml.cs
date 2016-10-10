using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendarXamForm.Pages;
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
