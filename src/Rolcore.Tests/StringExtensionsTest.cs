//-----------------------------------------------------------------------
// <copyright file="StringExtensionsTest.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Tests
{
    using System;
    using System.Text.RegularExpressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.Geography;
    using Rolcore.Text.RegularExpressions;

    /// <summary>
    ///This is a test class for StringExtensionMethodsTest and is intended
    ///to contain all StringExtensionMethodsTest Unit Tests
    ///</summary>
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void First_ReturnsFirstNCharacters()
        {
            int numberOfCharacters = 10;
            string inString = "12345678901";
            string expected = "1234567890";
            string actual = inString.First(numberOfCharacters);
            Assert.AreEqual(expected, actual);

            inString = "12345";
            expected = "12345";
            actual = inString.First(numberOfCharacters);
            Assert.AreEqual(expected, actual);
        }

        #region Repeat tests

        /// <summary>
        ///A test for Repeat
        ///</summary>
        [TestMethod]
        public void Repeat_ReturnsEmptyStringForZeroRepetitions()
        {
            string expected = string.Empty;
            string actual = "Hello world!".Repeat(0);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Repeat
        ///</summary>
        [TestMethod]
        public void Repeat1Test()
        {
            string expected = "Hello world!";
            string actual = expected.Repeat(1);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Repeat
        ///</summary>
        [TestMethod]
        public void Repeat2Test()
        {
            string s = "Hello world!";
            string expected = s + s;
            string actual = s.Repeat(2);
            Assert.AreEqual(expected, actual);
            s = "Hello world!";
            expected = s + s + s;
            actual = s.Repeat(3);
            Assert.AreEqual(expected, actual);
        }
        #endregion Repeat tests

        [TestMethod]
        public void Reverse_Reverses()
        {
            var forward = "0123456789";
            var expected = "9876543210";
            var actual = forward.Reverse();

            Assert.AreEqual(expected, actual);
        }

        #region IsPalindrome Tests
        [TestMethod]
        public void IsPalindrome_ReturnsTrueWhenGivenOneWordPalindrome()
        {
            var mom = "Mom";
            Assert.IsTrue(mom.IsPalindrome());
        }

        [TestMethod]
        public void IsPalindrome_ReturnsTrueWhenGivenMultiWordPalindrome()
        {
            var redruM = "Red rum, sir, is murder";
            Assert.IsTrue(redruM.IsPalindrome());
        }

        [TestMethod]
        public void IsPalindrome_ReturnsFalseWhenNotGivenPalindrome()
        {
            var redrum = "Red rum is murder";
            Assert.IsFalse(redrum.IsPalindrome());
        }
        #endregion IsPalindrome Tests

        /// <summary>
        ///A test for IsInUsa5DigitPostalCodeFormat
        ///</summary>
        [TestMethod]
        public void IsInUsaPostalCodeFormatTest()
        {
            Assert.IsFalse("aaaaa".IsInUsaPostalCodeFormat());
            Assert.IsFalse("aaaaa-aaaa".IsInUsaPostalCodeFormat());

            Assert.IsFalse("12345-".IsInUsaPostalCodeFormat());
            Assert.IsFalse("-1234".IsInUsaPostalCodeFormat());

            Assert.IsFalse("aaaaa-1234".IsInUsaPostalCodeFormat());
            Assert.IsFalse("12345-aaaa".IsInUsaPostalCodeFormat());

            Assert.IsTrue("12345".IsInUsaPostalCodeFormat());
            Assert.IsTrue("12345-1234".IsInUsaPostalCodeFormat());
        }

        /// <summary>
        ///A test for ToRegEx
        ///</summary>
        [TestMethod]
        public void HtmlTag_ToRegExTest()
        {
            Regex actual = CommonExpressions.HtmlTag.ToRegEx(RegexOptions.IgnoreCase);

            bool isValidRegex = actual.IsMatch("<html>");
            Assert.IsTrue(isValidRegex);

            isValidRegex = actual.IsMatch("<>");
            Assert.IsFalse(isValidRegex);

            isValidRegex = actual.IsMatch("<\"\">");
            Assert.IsFalse(isValidRegex);

            isValidRegex = actual.IsMatch("<br \\>");
            Assert.IsFalse(isValidRegex);
        }

        [TestMethod]
        public void ToSHA1String_ConvertsStringToSHA1Hash()
        {
            const string Expected = "17fb5cfe493fdf1fd66cf93348609c7937c036ed";
            var actual = "Bacon ipsum dolor sit amet biltong tail sirloin bacon pig pork loin spare ribs venison pancetta kielbasa shoulder beef. Ham sausage beef ribs, spare ribs boudin ribeye pork. Flank drumstick boudin pig. Tenderloin venison fatback, spare ribs ham hock doner pork loin. Short loin corned beef bresaola ball tip. Pancetta jowl pig, meatloaf hamburger drumstick short ribs salami brisket beef flank."
                .ToSHA1String();

            Assert.AreEqual(Expected, actual);
        }
    }
}
