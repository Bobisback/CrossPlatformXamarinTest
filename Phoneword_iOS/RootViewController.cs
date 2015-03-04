using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Phoneword_iOS
{
    public partial class RootViewController : UIViewController {
        static bool UserInterfaceIdiomIsPhone {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public RootViewController(IntPtr handle)
            : base(handle) {
        }

        public override void DidReceiveMemoryWarning() {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad() {
            base.ViewDidLoad();

            string translatedNumber = "";

            TranslateButton.TouchUpInside += (object sender, EventArgs e) => {
                translatedNumber = PhonewordSharedCode.PhoneTranslator.ToNumber(PhoneNumberText.Text);

                PhoneNumberText.ResignFirstResponder();

                if (translatedNumber == "") {
                    CallButton.SetTitle("Call", UIControlState.Normal);
                    CallButton.Enabled = false;
                } else {
                    CallButton.SetTitle("Call " + translatedNumber, UIControlState.Normal);
                    CallButton.Enabled = true;
                }
            };

            CallButton.TouchUpInside += (object sender, EventArgs e) => {
                var url = new NSUrl("tel:" + translatedNumber);

                if (!UIApplication.SharedApplication.OpenUrl(url)) {
                    var alertView = new UIAlertView("Not Supported",
                        "Scheme 'tel:' is not supported on this device",
                        null,
                        "Ok",
                        null);
                    alertView.Show();
                }
            };
        }

        public override void ViewWillAppear(bool animated) {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated) {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated) {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated) {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}
