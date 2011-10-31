using Rollins.Integration.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Rollins.Integration.Tests
{
    
    
    /// <summary>
    ///This is a test class for WebUtilsTest and is intended
    ///to contain all WebUtilsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WebUtilsTest
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
        ///A test for GetUrlSubdomain
        ///</summary>
        [TestMethod()]
        public void GetUrlSubdomainTest()
        {
            // Random tests
            Assert.AreEqual("foo.bar", WebUtils.GetUrlSubdomain("http://foo.bar.bizbaz.com"));
            Assert.AreEqual("foo.bar.biz", WebUtils.GetUrlSubdomain("http://foo.bar.biz.baz.com"));

            // Just the domain, no SSL
            Assert.AreEqual("nets", WebUtils.GetUrlSubdomain("http://nets.orkin.com"));
            Assert.AreEqual("www.nets", WebUtils.GetUrlSubdomain("http://www.nets.orkin.com"));
            Assert.AreEqual("nets", WebUtils.GetUrlSubdomain("http://nets.orkin.com/"));
            Assert.AreEqual("www.nets", WebUtils.GetUrlSubdomain("http://www.nets.orkin.com/"));

            // Just the domain, with SSL
            Assert.AreEqual("nets", WebUtils.GetUrlSubdomain("https://nets.orkin.com"));
            Assert.AreEqual("www.nets", WebUtils.GetUrlSubdomain("https://www.nets.orkin.com"));
            Assert.AreEqual("nets", WebUtils.GetUrlSubdomain("https://nets.orkin.com/"));
            Assert.AreEqual("www.nets", WebUtils.GetUrlSubdomain("https://www.nets.orkin.com/"));
            Assert.AreEqual("www.nets", WebUtils.GetUrlSubdomain("https://www.nets.orkin.com/justforthehellofit"));
            Assert.AreEqual("www.nets", WebUtils.GetUrlSubdomain("https://www.nets.orkin.com/justforthehellofit/"));
            Assert.AreEqual("www.nets", WebUtils.GetUrlSubdomain("https://www.nets.orkin.com/justforthehellofit.aspx"));
            Assert.AreEqual("www.nets", WebUtils.GetUrlSubdomain("https://www.nets.orkin.com/justforthehellofit.aspx/"));
            Assert.AreEqual("www.nets", WebUtils.GetUrlSubdomain("https://www.nets.orkin.com/justforthehellofit.aspx?please=work&how=well"));

            // Domain and path, no SSL
            Assert.AreEqual("www.nets", WebUtils.GetUrlSubdomain("http://www.nets.orkin.com/pest-control/ohio-oh"));
            Assert.AreEqual("nets", WebUtils.GetUrlSubdomain("http://nets.orkin.com/pest-control/ohio-oh"));
            Assert.AreEqual("nets", WebUtils.GetUrlSubdomain("http://nets.orkin.com/customercare/index.aspx"));
            Assert.AreEqual("www.nets", WebUtils.GetUrlSubdomain("http://www.nets.orkin.com/customercare/index.aspx"));

            // Domain and path, with SSL
            Assert.AreEqual("www.nets", WebUtils.GetUrlSubdomain("https://www.nets.orkin.com/pest-control/ohio-oh"));
            Assert.AreEqual("nets", Rollins.Web.UI.WebUtils.GetUrlSubdomain("https://nets.orkin.com/pest-control/ohio-oh"));
            Assert.AreEqual("nets", WebUtils.GetUrlSubdomain("https://nets.orkin.com/customercare/index.aspx"));
            Assert.AreEqual("www.nets", WebUtils.GetUrlSubdomain("https://www.nets.orkin.com/customercare/index.aspx"));
        }

        /// <summary>
        ///A test for GetUrlDoman
        ///</summary>
        [TestMethod()]
        public void GetUrlDomanTest()
        {
            // Random test
            Assert.AreEqual("foo.bar.bizbaz.com", WebUtils.GetUrlDomain("http://foo.bar.bizbaz.com"));
            Assert.AreEqual("foo.bar.biz.baz.com", WebUtils.GetUrlDomain("http://foo.bar.biz.baz.com"));
            Assert.AreEqual("foo.bar.bizbaz.net", WebUtils.GetUrlDomain("http://foo.bar.bizbaz.net"));
            Assert.AreEqual("foo.bar.biz.baz.org", WebUtils.GetUrlDomain("http://foo.bar.biz.baz.org"));

            // Just the domain, no SSL
            Assert.AreEqual("nets.orkin.com", WebUtils.GetUrlDomain("http://nets.orkin.com"));
            Assert.AreEqual("www.nets.orkin.com", WebUtils.GetUrlDomain("http://www.nets.orkin.com"));
            Assert.AreEqual("nets.orkin.com", WebUtils.GetUrlDomain("http://nets.orkin.com/"));
            Assert.AreEqual("www.nets.orkin.com", WebUtils.GetUrlDomain("http://www.nets.orkin.com/"));

            // Just the domain, with SSL
            Assert.AreEqual("nets.orkin.com", WebUtils.GetUrlDomain("https://nets.orkin.com"));
            Assert.AreEqual("www.nets.orkin.com", WebUtils.GetUrlDomain("https://www.nets.orkin.com"));
            Assert.AreEqual("nets.orkin.com", WebUtils.GetUrlDomain("https://nets.orkin.com/"));
            Assert.AreEqual("www.nets.orkin.com", WebUtils.GetUrlDomain("https://www.nets.orkin.com/"));
            Assert.AreEqual("www.nets.orkin.com", WebUtils.GetUrlDomain("https://www.nets.orkin.com/justforthehellofit"));
            Assert.AreEqual("www.nets.orkin.com", WebUtils.GetUrlDomain("https://www.nets.orkin.com/justforthehellofit/"));
            Assert.AreEqual("www.nets.orkin.com", WebUtils.GetUrlDomain("https://www.nets.orkin.com/justforthehellofit.aspx"));
            Assert.AreEqual("www.nets.orkin.com", WebUtils.GetUrlDomain("https://www.nets.orkin.com/justforthehellofit.aspx/"));
            Assert.AreEqual("www.nets.orkin.com", WebUtils.GetUrlDomain("https://www.nets.orkin.com/justforthehellofit.aspx?please=work&how=well"));

            // Domain and path, no SSL
            Assert.AreEqual("www.nets.orkin.com", WebUtils.GetUrlDomain("http://www.nets.orkin.com/pest-control/ohio-oh"));
            Assert.AreEqual("nets.orkin.com", WebUtils.GetUrlDomain("http://nets.orkin.com/pest-control/ohio-oh"));
            Assert.AreEqual("nets.orkin.com", WebUtils.GetUrlDomain("http://nets.orkin.com/customercare/index.aspx"));
            Assert.AreEqual("www.nets.orkin.com", WebUtils.GetUrlDomain("http://www.nets.orkin.com/customercare/index.aspx"));

            // Domain and path, with SSL
            Assert.AreEqual("www.nets.orkin.com", WebUtils.GetUrlDomain("https://www.nets.orkin.com/pest-control/ohio-oh"));
            Assert.AreEqual("nets.orkin.com", WebUtils.GetUrlDomain("https://nets.orkin.com/pest-control/ohio-oh"));
            Assert.AreEqual("nets.orkin.com", WebUtils.GetUrlDomain("https://nets.orkin.com/customercare/index.aspx"));
            Assert.AreEqual("www.nets.orkin.com", WebUtils.GetUrlDomain("https://www.nets.orkin.com/customercare/index.aspx"));
        }
    }
}
