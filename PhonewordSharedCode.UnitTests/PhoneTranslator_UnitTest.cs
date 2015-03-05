using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhonewordSharedCode.UnitTests {
    [TestClass]
    public class PhoneTranslator_UnitTest {

        [TestMethod]
        public void PhoneTranslator_ToNumber_Tests() {
            Assert.AreEqual("8503463228", PhoneTranslator.ToNumber("850346dabu"));
            Assert.AreEqual("850.345.7585", PhoneTranslator.ToNumber("850.EHL.7585"));
            Assert.AreEqual("864.457-5623", PhoneTranslator.ToNumber("864.457-JOCF"));
            Assert.AreEqual("1-245.465.5758", PhoneTranslator.ToNumber("1-245.IMJ.5758"));
            Assert.AreEqual("1-452-475-8685", PhoneTranslator.ToNumber("1-452-GQL-VMVJ"));
            Assert.AreEqual("(850)523-6523", PhoneTranslator.ToNumber("(850)523-6523"));
            Assert.AreEqual("(850) 563.5242", PhoneTranslator.ToNumber("(850) 563.5242"));
            Assert.AreEqual("(863)5623295", PhoneTranslator.ToNumber("(863)562dcWk"));
            Assert.AreEqual("Error: Validation Failed", PhoneTranslator.ToNumber("588-65212"));
            Assert.AreEqual("Error: Validation Failed", PhoneTranslator.ToNumber("8563-BJNN"));
            Assert.AreEqual("Error: Validation Failed", PhoneTranslator.ToNumber("1-850.LA.52145"));
            Assert.AreEqual("Error: Validation Failed", PhoneTranslator.ToNumber("8850.263.LTGN"));
            Assert.AreEqual("Error: Validation Failed", PhoneTranslator.ToNumber("1-850-YJU-56453"));
            Assert.AreEqual("Error: Validation Failed", PhoneTranslator.ToNumber("2635YUHJ"));
            Assert.AreEqual("Error: Validation Failed", PhoneTranslator.ToNumber("850346ERT"));
            Assert.AreEqual("Error: Validation Failed", PhoneTranslator.ToNumber("5582.GFG.GHYU"));
        }

        [TestMethod]
        public void PhoneTranslator_ValidatePhoneword_Tests() {
            Assert.AreEqual(true, PhoneTranslator.ValidatePhoneword("8503463228"));
            Assert.AreEqual(true, PhoneTranslator.ValidatePhoneword("850.345.7585"));
            Assert.AreEqual(true, PhoneTranslator.ValidatePhoneword("864.457-5623"));
            Assert.AreEqual(true, PhoneTranslator.ValidatePhoneword("1-245.465.5758"));
            Assert.AreEqual(true, PhoneTranslator.ValidatePhoneword("1-452-475-8685"));
            Assert.AreEqual(true, PhoneTranslator.ValidatePhoneword("(850)523-6523"));
            Assert.AreEqual(true, PhoneTranslator.ValidatePhoneword("(850) 563.5242"));
            Assert.AreEqual(true, PhoneTranslator.ValidatePhoneword("(863)5623215"));
            Assert.AreEqual(false, PhoneTranslator.ValidatePhoneword("588-65212"));
            Assert.AreEqual(false, PhoneTranslator.ValidatePhoneword("8563-2566"));
            Assert.AreEqual(false, PhoneTranslator.ValidatePhoneword("1-850.52.52145"));
            Assert.AreEqual(false, PhoneTranslator.ValidatePhoneword("7850.263.5864"));
            Assert.AreEqual(false, PhoneTranslator.ValidatePhoneword("1-850-356-56453"));
            Assert.AreEqual(false, PhoneTranslator.ValidatePhoneword("26356235"));
            Assert.AreEqual(false, PhoneTranslator.ValidatePhoneword("850346233"));
            Assert.AreEqual(false, PhoneTranslator.ValidatePhoneword("5-582.365.2452"));
        }

        [TestMethod]
        public void PhoneTranslator_TranslateToNumber_Tests() {
            Assert.AreEqual(2, PhoneTranslator.TranslateToNumber('A'));
            Assert.AreEqual(2, PhoneTranslator.TranslateToNumber('C'));
            Assert.AreEqual(3, PhoneTranslator.TranslateToNumber('E'));
            Assert.AreEqual(5, PhoneTranslator.TranslateToNumber('J'));
            Assert.AreEqual(4, PhoneTranslator.TranslateToNumber('I'));
            Assert.AreEqual(4, PhoneTranslator.TranslateToNumber('G'));
            Assert.AreEqual(6, PhoneTranslator.TranslateToNumber('M'));
            Assert.AreEqual(7, PhoneTranslator.TranslateToNumber('S'));
            Assert.AreEqual(7, PhoneTranslator.TranslateToNumber('Q'));
            Assert.AreEqual(9, PhoneTranslator.TranslateToNumber('X'));
            Assert.AreEqual(null, PhoneTranslator.TranslateToNumber('/'));
            Assert.AreEqual(null, PhoneTranslator.TranslateToNumber('\''));
            Assert.AreEqual(null, PhoneTranslator.TranslateToNumber('}'));
            Assert.AreEqual(null, PhoneTranslator.TranslateToNumber('!'));
        }
    }
}
