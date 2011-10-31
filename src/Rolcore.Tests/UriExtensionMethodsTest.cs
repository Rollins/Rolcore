using Rolcore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Rolcore.Tests
{
    
    
    /// <summary>
    ///This is a test class for UriExtensionMethodsTest and is intended
    ///to contain all UriExtensionMethodsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UriExtensionMethodsTest
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
        ///A test for GetSubDomain
        ///</summary>
        [TestMethod()]
        public void GetSubDomainTest()
        {
            Uri uri = new Uri("http://signup.orkin.com/orkin/res/pc-Standard-ph_cb.aspx?cm_mmc=National-_-Image7-_-D72WEB-_-_&utm_source=National&utm_medium=Direct%2BMail&utm_term=Image7&utm_content=Standard&utm_campaign=D72WEB"); // Randomly taken from a landing page vanity URL
            string expected = "signup";
            string actual = UriExtensions.GetSubDomain(uri);
            Assert.AreEqual(expected, actual);

            uri = new Uri("http://signup.foo.orkin.com/orkin/res/pc-Standard-ph_cb.aspx?cm_mmc=National-_-Image7-_-D72WEB-_-_&utm_source=National&utm_medium=Direct%2BMail&utm_term=Image7&utm_content=Standard&utm_campaign=D72WEB"); // Randomly taken from a landing page vanity URL
            expected = "signup.foo";
            actual = UriExtensions.GetSubDomain(uri);
            Assert.AreEqual(expected, actual);

            uri = new Uri("http://signup.foo.bar.orkin.com/orkin/res/pc-Standard-ph_cb.aspx?cm_mmc=National-_-Image7-_-D72WEB-_-_&utm_source=National&utm_medium=Direct%2BMail&utm_term=Image7&utm_content=Standard&utm_campaign=D72WEB"); // Randomly taken from a landing page vanity URL
            expected = "signup.foo.bar";
            actual = UriExtensions.GetSubDomain(uri);
            Assert.AreEqual(expected, actual);

            uri = new Uri("http://bar.biz.baz.signup.foo.orkin.com/orkin/res/pc-Standard-ph_cb.aspx?cm_mmc=National-_-Image7-_-D72WEB-_-_&utm_source=National&utm_medium=Direct%2BMail&utm_term=Image7&utm_content=Standard&utm_campaign=D72WEB"); // Randomly taken from a landing page vanity URL
            expected = "bar.biz.baz.signup.foo";
            actual = UriExtensions.GetSubDomain(uri);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetBaseUri
        ///</summary>
        [TestMethod()]
        public void GetBaseUriTest()
        {
            Uri uri = new Uri("http://signup.orkin.com/orkin/res/pc-Standard-ph_cb.aspx?cm_mmc=National-_-Image7-_-D72WEB-_-_&utm_source=National&utm_medium=Direct%2BMail&utm_term=Image7&utm_content=Standard&utm_campaign=D72WEB"); // Randomly taken from a landing page vanity URL
            string expected = "http://signup.orkin.com/";
            string actual = UriExtensions.GetBaseUri(uri).ToString();
            Assert.AreEqual(expected, actual);

            uri = new Uri("http://signup.foo.orkin.com/orkin/res/pc-Standard-ph_cb.aspx?cm_mmc=National-_-Image7-_-D72WEB-_-_&utm_source=National&utm_medium=Direct%2BMail&utm_term=Image7&utm_content=Standard&utm_campaign=D72WEB"); // Randomly taken from a landing page vanity URL
            expected = "http://signup.foo.orkin.com/";
            actual = UriExtensions.GetBaseUri(uri).ToString();
            Assert.AreEqual(expected, actual);

            uri = new Uri("https://signup.foo.bar.orkin.com/orkin/res/pc-Standard-ph_cb.aspx?cm_mmc=National-_-Image7-_-D72WEB-_-_&utm_source=National&utm_medium=Direct%2BMail&utm_term=Image7&utm_content=Standard&utm_campaign=D72WEB"); // Randomly taken from a landing page vanity URL
            expected = "https://signup.foo.bar.orkin.com/";
            actual = UriExtensions.GetBaseUri(uri).ToString();
            Assert.AreEqual(expected, actual);

            uri = new Uri("http://bar.biz.baz.signup.foo.orkin.com/orkin/res/pc-Standard-ph_cb.aspx?cm_mmc=National-_-Image7-_-D72WEB-_-_&utm_source=National&utm_medium=Direct%2BMail&utm_term=Image7&utm_content=Standard&utm_campaign=D72WEB"); // Randomly taken from a landing page vanity URL
            expected = "http://bar.biz.baz.signup.foo.orkin.com/";
            actual = UriExtensions.GetBaseUri(uri).ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
