//-----------------------------------------------------------------------
// <copyright file="IntExtensionsTest.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.Geography;

    /// <summary>
    /// This is a test class for IntExtensionsTest and is intended to contain all IntExtensionsTest
    /// Unit Tests.
    /// </summary>
    [TestClass]
    public class IntExtensionsTest
    {
        /// <summary>
        /// A test for ToUsa5DigitPostalCode
        /// </summary>
        [TestMethod]
        public void ToUsa5DigitPostalCode_ZeroPadsOnLeftForIntsLessThan5Digits()
        {
            Assert.AreEqual("00001", 1.ToUsa5DigitPostalCode());
            Assert.AreEqual("00011", 11.ToUsa5DigitPostalCode());
            Assert.AreEqual("00111", 111.ToUsa5DigitPostalCode());
            Assert.AreEqual("01111", 1111.ToUsa5DigitPostalCode());
            Assert.AreEqual("11111", 11111.ToUsa5DigitPostalCode());
        }
    }
}
