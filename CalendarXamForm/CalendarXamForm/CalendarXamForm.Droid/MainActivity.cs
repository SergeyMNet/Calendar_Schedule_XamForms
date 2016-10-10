using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.Res;

namespace CalendarXamForm.Droid
{
    [Activity(Label = "CalendarXamForm", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            CalendarXamForm.Application.ScreenWidth = (int)((int)Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density); // real pixels
            CalendarXamForm.Application.ScreenHeight = (int)((int)Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density); // real pixels

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new Application());
        }
    }
}

