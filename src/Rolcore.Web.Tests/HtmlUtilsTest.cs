using Rolcore.Web.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

namespace Rolcore.Web.Tests
{
    
    
    /// <summary>
    ///This is a test class for HtmlUtilsTest and is intended
    ///to contain all HtmlUtilsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HtmlUtilsTest
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
    }
}
namespace Rollins.Web.Tests
{
    
    
    /// <summary>
    ///This is a test class for HtmlUtilsTest and is intended
    ///to contain all HtmlUtilsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HtmlUtilsTest
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
        ///A test for CreateAttributeRegEx
        ///</summary>
        [TestMethod()]
        public void CreateAttributeRegExTest()
        {
            string attributeName = "href"; // TODO: Initialize to an appropriate value
            var actual = HtmlUtils.CreateAttributeRegEx(attributeName);

            const string anchorHrefStandardDblQuote = "<p><a href=\"https://github.com/jamestharpe/Rolcore\">Go to Rolcore</a></p>";
            var matchValue = actual.Match(anchorHrefStandardDblQuote).Value;
            Assert.AreEqual("href=\"https://github.com/jamestharpe/Rolcore\"", matchValue);

            const string anchorHrefStandardSnglQuote = "<p><a href='https://github.com/jamestharpe/Rolcore'>Go to Rolcore</a></p>";
            matchValue = actual.Match(anchorHrefStandardSnglQuote).Value;
            Assert.AreEqual("href='https://github.com/jamestharpe/Rolcore'", matchValue);

            const string anchorHrefStandardNoQuote = "<p><a href=https://github.com/jamestharpe/Rolcore>Go to Rolcore</a></p>";
            matchValue = actual.Match(anchorHrefStandardNoQuote).Value;
            Assert.AreEqual("href=https://github.com/jamestharpe/Rolcore", matchValue);

            const string anchorHrefMismatchDblSnglQuote = "<p><a href=\"https://github.com/jamestharpe/Rolcore'>Go to Rolcore</a></p>";
            matchValue = actual.Match(anchorHrefMismatchDblSnglQuote).Value;
            Assert.AreEqual("href=\"https://github.com/jamestharpe/Rolcore'", matchValue);

            const string anchorHrefMismatchSnglDblQuote = "<p><a href='https://github.com/jamestharpe/Rolcore\">Go to Rolcore</a></p>";
            matchValue = actual.Match(anchorHrefMismatchSnglDblQuote).Value;
            Assert.AreEqual("href='https://github.com/jamestharpe/Rolcore\"", matchValue);

            const string anchorHrefOpenDblQuote = "<p><a href=\"https://github.com/jamestharpe/Rolcore>Go to Rolcore</a></p>";
            matchValue = actual.Match(anchorHrefOpenDblQuote).Value;
            Assert.AreEqual("href=\"https://github.com/jamestharpe/Rolcore", matchValue);

            const string anchorHrefOpenSnglQuote = "<p><a href='https://github.com/jamestharpe/Rolcore>Go to Rolcore</a></p>";
            matchValue = actual.Match(anchorHrefOpenSnglQuote).Value;
            Assert.AreEqual("href='https://github.com/jamestharpe/Rolcore", matchValue);
        }
    }
}
