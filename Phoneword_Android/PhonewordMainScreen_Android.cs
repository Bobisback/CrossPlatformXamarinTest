using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Phoneword_Android {
    [Activity(Label = "Phoneword", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity {

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
			Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);
			TextView errorLabel = FindViewById<TextView> (Resource.Id.ErrorLabel);

			phoneNumberText.Click += (object sender, EventArgs e) => {
				phoneNumberText.RequestFocus();
				phoneNumberText.SelectAll();
			};

            // Disable the "Call" button
            callButton.Enabled = false;

            // Add code to translate number
            string translatedNumber = string.Empty;

            translateButton.Click += (object sender, EventArgs e) => {
				//translateButton.RequestFocus();

                // Translate user’s alphanumeric phone number to numeric
                translatedNumber = PhonewordSharedCode.PhoneTranslator.ToNumber(phoneNumberText.Text);
                
				if (translatedNumber.Contains("Error: ")) {
                    callButton.Text = "Call";
                    callButton.Enabled = false;

					if (translatedNumber.Contains("Error: Validation Failed")) {
						errorLabel.Visibility = ViewStates.Visible;
						errorLabel.Text = "Validation Failed: Please check Phone Format.";
					}
                } else {
                    callButton.Text = "Call " + translatedNumber;
                    callButton.Enabled = true;
					errorLabel.Visibility = ViewStates.Invisible;
					errorLabel.Text = string.Empty;
                }
            };

            callButton.Click += (object sender, EventArgs e) => {
				//callButton.RequestFocus();

                // On "Call" button click, try to dial phone number.
                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage("Call " + translatedNumber + "?");
                callDialog.SetNeutralButton("Call", delegate {
                    // Create intent to dial phone
                    var callIntent = new Intent(Intent.ActionCall);
                    callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));
                    StartActivity(callIntent);
                });
                callDialog.SetNegativeButton("Cancel", delegate { });

                // Show the alert dialog to the user and wait for response.
                callDialog.Show();
            };
		}
    }
}
