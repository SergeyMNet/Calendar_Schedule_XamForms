using System;
using System.Collections.Generic;
using System.Text;
using CalendarXamForm.iOS.Renders;
using CalendarXamForm.Pages;
//using CalendarXamForm.Pages.Teplate;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BackgroundPage), typeof(BackgroundPageRenderer))]
namespace CalendarXamForm.iOS.Renders
{
    public class BackgroundPageRenderer : PageRenderer
    {
        public override void WillMoveToParentViewController(UIViewController parent)
        {
            base.WillMoveToParentViewController(parent);
            if (parent != null)
            {
                parent.ModalPresentationStyle = ModalPresentationStyle;
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            //View.Superview.BackgroundColor = UIColor.Clear;
            View.Superview.BackgroundColor = Color.Transparent.ToUIColor();
            //View.Superview.BackgroundColor = UIColor.FromWhiteAlpha(1, (nint)0.5);

            //this.View.BackgroundColor = UIColor.Clear;
            //this.View.BackgroundColor = Color.Transparent.ToUIColor();
            //this.View.Alpha = (nint)0.5;
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (Element is BackgroundPage)
            {
                // UI settings
                this.View.BackgroundColor = UIColor.Clear;
                this.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
                this.ModalTransitionStyle = UIModalTransitionStyle.CoverVertical;
            }

            //if (e.OldElement != null || Element == null)
            //{
            //    return;
            //}

            //try
            //{
            //    //SetupUserInterface();
            //    //SetupEventHandlers();
            //    //SetupLiveCameraStream();
            //    //AuthorizeCameraUse();
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(@"			ERROR: ", ex.Message);
            //}
        }
    }
}
