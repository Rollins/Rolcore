using Rolcore.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Rolcore.Web.Tests
{
    
    
    /// <summary>
    ///This is a test class for StringExtensionMethodsTest and is intended
    ///to contain all StringExtensionMethodsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StringExtensionsTest
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
        ///A test for ContainsHtml
        ///</summary>
        [TestMethod()]
        public void ContainsHtmlPositiveTest()
        {
            string s = "<a>";
            bool actual = s.ContainsHtml();
            Assert.AreEqual(true, actual);

            s = "<a href='foobar'>";
            actual = s.ContainsHtml();
            Assert.AreEqual(true, actual);

            s = "<a href=\"foobar\">";
            actual = s.ContainsHtml();
            Assert.AreEqual(true, actual);

            s = "<a href=\"foobar\">biz baz";
            actual = s.ContainsHtml();
            Assert.AreEqual(true, actual);

            s = "<a href=\"foobar\">biz baz</a> bang!";
            actual = s.ContainsHtml();
            Assert.AreEqual(true, actual);
        }

        /// <summary>
        ///A test for ToUri
        ///</summary>
        [TestMethod()]
        public void ToUriTest()
        {
            string expected = "http://www.rolcore.com/";
            string actual = expected.ToUri().ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToUri
        ///</summary>
        [TestMethod(), ExpectedException(typeof(ArgumentException))]
        public void ToUriEmptyStringTest()
        {
            string expected = string.Empty;
            string actual = expected.ToUri().ToString();
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContainsHtml
        ///</summary>
        //TODO: negative tests
        //[TestMethod()]
        //public void ContainsHtmlNegativeTest()
        //{
        //}
    }
}
