using NUnit.Framework;
using Xamarin.UITest;
using System.Reflection;
using System.IO;
using Xamarin.UITest.Queries;
using System.Linq;

[TestFixture]
public class ValidatePhonewordTests {
    IApp app;

    public string PathToIPA { get; private set; }
    public string PathToAPK { get; private set; }

    [TestFixtureSetUp]
    public void TestFixtureSetup() {
		PathToIPA = "../../../Phoneword_iOS/bin/iPhoneSimulator/Debug/TestProject.app";
		PathToAPK = "../../../Phoneword_Android/bin/Debug/Phoneword_Android.Phoneword_Android.apk";
    }

	[TestCase(Platform.iOS)]
	[TestCase(Platform.Android)]
	public void PhonewondNumber_TooLong_DisplayErrorMessage(Platform platform) {
		/* Arrange - set up our queries for the views */
		ConfigureTest(platform);

		app.EnterText(c=>c.Marked("PhoneNumberText"), "1-855-XAMARIN2");

		/* Act - enter the credit card number and tap the button */
		app.Tap(c=>c.Marked("TranslateButton"));

		/* Assert - make sure that the error message is displayed. */
		AppResult[] result = app.Query (c => c.Marked ("ErrorLabel").Text ("Validation Failed: Please check Phone Format."));
		Assert.IsTrue(result.Any(), "The 'Validation Failed' error message is not being displayed.");
	}

	[TestCase(Platform.iOS)]
	[TestCase(Platform.Android)]
	public void PhonewondNumber_TooShort_DisplayErrorMessage(Platform platform) {
		/* Arrange - set up our queries for the views */
		ConfigureTest(platform);

		app.EnterText(c=>c.Marked("PhoneNumberText"), "866-USA");

		/* Act - enter the credit card number and tap the button */
		app.Tap(c=>c.Marked("TranslateButton"));

		/* Assert - make sure that the error message is displayed. */
		AppResult[] result = app.Query (c => c.Marked ("ErrorLabel").Text ("Validation Failed: Please check Phone Format."));
		Assert.IsTrue(result.Any(), "The 'Validation Failed' error message is not being displayed.");
	}

	[TestCase(Platform.iOS)]
	[TestCase(Platform.Android)]
	public void PhonewondNumber_BadFormat1_DisplayErrorMessage(Platform platform) {
		/* Arrange - set up our queries for the views */
		ConfigureTest(platform);

		app.EnterText(c=>c.Marked("PhoneNumberText"), "855-XAMARI");

		/* Act - enter the credit card number and tap the button */
		app.Tap(c=>c.Marked("TranslateButton"));

		/* Assert - make sure that the error message is displayed. */
		AppResult[] result = app.Query (c => c.Marked ("ErrorLabel").Text ("Validation Failed: Please check Phone Format."));
		Assert.IsTrue(result.Any(), "The 'Validation Failed' error message is not being displayed.");
	}

	[TestCase(Platform.iOS)]
	[TestCase(Platform.Android)]
	public void PhonewondNumber_BadFormat2_DisplayErrorMessage(Platform platform) {
		/* Arrange - set up our queries for the views */
		ConfigureTest(platform);

		app.EnterText(c=>c.Marked("PhoneNumberText"), "855-XAMA-RIN");

		/* Act - enter the credit card number and tap the button */
		app.Tap(c=>c.Marked("TranslateButton"));

		/* Assert - make sure that the error message is displayed. */
		AppResult[] result = app.Query (c => c.Marked ("ErrorLabel").Text ("Validation Failed: Please check Phone Format."));
		Assert.IsTrue(result.Any(), "The 'Validation Failed' error message is not being displayed.");
	}

	[TestCase(Platform.iOS)]
	[TestCase(Platform.Android)]
	public void PhonewondNumber_BadFormat3_DisplayErrorMessage(Platform platform) {
		/* Arrange - set up our queries for the views */
		ConfigureTest(platform);

		app.EnterText(c=>c.Marked("PhoneNumberText"), "85-5XAMA.RIN");

		/* Act - enter the credit card number and tap the button */
		app.Tap(c=>c.Marked("TranslateButton"));

		/* Assert - make sure that the error message is displayed. */
		AppResult[] result = app.Query (c => c.Marked ("ErrorLabel").Text ("Validation Failed: Please check Phone Format."));
		Assert.IsTrue(result.Any(), "The 'Validation Failed' error message is not being displayed.");
	}

	[TestCase(Platform.iOS)]
	[TestCase(Platform.Android)]
	public void PhonewondNumber_TelNotSupported_DisplayErrorMessage(Platform platform) {
		/* Arrange - set up our queries for the views */
		ConfigureTest(platform);

		app.EnterText(c=>c.Marked("PhoneNumberText"), "1-855-XAMARIN");

		/* Act - enter the credit card number and tap the button */
		app.Tap(c=>c.Marked("TranslateButton"));
		app.Tap(c=>c.Marked("CallButton"));

		/* Assert - make sure that the error message is displayed. */
		AppResult[] result = app.Query (c => c.Text("Scheme 'tel:' is not supported on this device"));
		Assert.IsTrue(result.Any(), "The 'tel: not supported' error message is not being displayed.");
		app.Tap(c=>c.Text("Ok"));
	}

	public enum Platform
	{
		Android,
		iOS
	}

	void ConfigureTest(Platform platform)
	{
		switch (platform)
		{
		case Platform.iOS:
			ConfigureiOSApp ();
			break;
		case Platform.Android:
			ConfigureAndroidApp ();
			break;
		}
	}

	void ConfigureAndroidApp()
	{
		app = ConfigureApp.Android
			.ApkFile(PathToAPK)
			.StartApp();
	}

	void ConfigureiOSApp()
	{
		app = ConfigureApp.iOS
			.AppBundle(PathToIPA)
			.StartApp();
	}
}