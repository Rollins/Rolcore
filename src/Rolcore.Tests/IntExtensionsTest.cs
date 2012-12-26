//-----------------------------------------------------------------------
// <copyright file="IntExtensionsTest.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.Geography;

    /// <summary>
    ///This is a test class for IntExtensionsTest and is intended
    ///to contain all IntExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IntExtensionsTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ToUsa5DigitPostalCode
        ///</summary>
        [TestMethod()]
        public void ToUsa5DigitPostalCodeTest()
        {
            Assert.AreEqual("00001", 1.ToUsa5DigitPostalCode());
            Assert.AreEqual("00011", 11.ToUsa5DigitPostalCode());
            Assert.AreEqual("00111", 111.ToUsa5DigitPostalCode());
            Assert.AreEqual("01111", 1111.ToUsa5DigitPostalCode());
            Assert.AreEqual("11111", 11111.ToUsa5DigitPostalCode());
        }
    }
}
