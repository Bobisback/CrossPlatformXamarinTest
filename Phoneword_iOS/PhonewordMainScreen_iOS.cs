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

			PhoneNumberText.WeakDelegate = this;
			PhoneNumberText.AccessibilityIdentifier = "PhoneNumberText";

			ErrorLabel.AccessibilityIdentifier = "ErrorLabel";

			string translatedNumber = string.Empty;

            TranslateButton.TouchUpInside += (object sender, EventArgs e) => {
                translatedNumber = PhonewordSharedCode.PhoneTranslator.ToNumber(PhoneNumberText.Text);

                PhoneNumberText.ResignFirstResponder();

				if (translatedNumber.Contains("Error: ")) {
                    CallButton.SetTitle("Call", UIControlState.Normal);
                    CallButton.Enabled = false;

					if (translatedNumber.Contains("Error: Validation Failed")) {
						ErrorLabel.Hidden = false;
						ErrorLabel.Text = "Validation Failed: Please check Phone Format.";
					}
                } else {
                    CallButton.SetTitle("Call " + translatedNumber, UIControlState.Normal);
					CallButton.Enabled = true;
					ErrorLabel.Hidden = true;
					ErrorLabel.Text = string.Empty;
                }
            };
			TranslateButton.AccessibilityIdentifier = "TranslateButton";

            CallButton.TouchUpInside += (object sender, EventArgs e) => {
                var url = new NSUrl("tel:" + translatedNumber);

                if (!UIApplication.SharedApplication.OpenUrl(url)) {
                    var alertView = new UIAlertView("Not Supported",
                        "Scheme 'tel:' is not supported on this device",
                        null,
                        "Ok",
                        null);
					alertView.AccessibilityIdentifier = "AlertViewTelSchemeNotSupported";
                    alertView.Show();
                }
            };
			CallButton.AccessibilityIdentifier = "CallButton";
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

		[Export("textFieldDidBeginEditing:")]
		public void EditingStarted (UITextField textField) {
			textField.SelectAll (this);
		}
    }
}
