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
        PathToIPA = "../../../CreditCardValidation.iOS/bin/iPhoneSimulator/Debug/CreditCardValidationiOS.app";
        PathToAPK = "../../../CreditCardValidation.Droid/bin/Debug/CreditCardValidation.Droid.APK";
    }
}
