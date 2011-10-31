using Rolcore.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Rolcore.Web.Tests
{
    
    
    /// <summary>
    ///This is a test class for HttpUtilsTest and is intended
    ///to contain all HttpUtilsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HttpUtilsTest
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
        ///A test for ReplaceQSValues
        ///</summary>
        [TestMethod()]
        public void ReplaceQSValuesTest()
        {
            string queryString = "?utm_source=national&utm_content=phonepromo1-20&utm_medium=drtv&utm_campaign=DTV038";
            string replacementRules = "utm_campaign=hello{utm_campaign}world";
            char ruleSeperator = ',';
            string expected = "?utm_source=national&utm_content=phonepromo1-20&utm_medium=drtv&utm_campaign=helloDTV038world";
            string actual = HttpUtils.ReplaceQSValues(queryString, replacementRules, ruleSeperator);
            Assert.AreEqual(expected, actual);


            queryString = "?utm_source=google&utm_content=pest-control&utm_medium=ppc&utm_campaign=GO178WEB&utm_term=pest-control&a=ABC&b=BCD";
            replacementRules = "utm_campaign={a}";
            expected = "?utm_source=google&utm_content=pest-control&utm_medium=ppc&utm_campaign=ABC&utm_term=pest-control&a=ABC&b=BCD";
            actual = HttpUtils.ReplaceQSValues(queryString, replacementRules, ruleSeperator);
            Assert.AreEqual(expected, actual);

            queryString = "?utm_source=google&utm_content=pest-control&utm_medium=ppc&utm_campaign=GO178WEB&a=ABC&utm_term=pest-control&b=BCD";
            replacementRules = "utm_campaign={b}";
            expected = "?utm_source=google&utm_content=pest-control&utm_medium=ppc&utm_campaign=BCD&a=ABC&utm_term=pest-control&b=BCD";
            actual = HttpUtils.ReplaceQSValues(queryString, replacementRules, ruleSeperator);
            Assert.AreEqual(expected, actual);

            //
            // Case 3004
            queryString = "?utm_source=google&utm_content=pest-control&utm_medium=ppc&a=ABC&utm_term=pest-control&b=BCD";
            replacementRules = "utm_campaign={b}";
            expected = "?utm_source=google&utm_content=pest-control&utm_medium=ppc&a=ABC&utm_term=pest-control&b=BCD";
            actual = HttpUtils.ReplaceQSValues(queryString, replacementRules, ruleSeperator);
            Assert.AreEqual(expected, actual);
        }
    }
}
