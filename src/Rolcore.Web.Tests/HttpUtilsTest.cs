//-----------------------------------------------------------------------
// <copyright file="HttpUtilsTest.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Web.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.Web;
    
    /// <summary>
    /// This is a test class for HttpUtilsTest and is intended
    /// to contain all HttpUtilsTest Unit Tests
    /// </summary>
    [TestClass]
    public class HttpUtilsTest
    {
        /// <summary>
        /// A test for ReplaceQSValues
        /// </summary>
        [TestMethod]
        public void ReplaceQSValuesTest()
        {
            string queryString = "?utm_source=national&utm_content=phonepromo1-20&utm_medium=drtv&utm_campaign=DTV038";
            string replacementRules = "utm_campaign=hello{utm_campaign}world";
            char ruleSeperator = ',';
            string expected = "?utm_source=national&utm_content=phonepromo1-20&utm_medium=drtv&utm_campaign=helloDTV038world";
            string actual = HttpUtils.ReplaceQSValues(queryString, replacementRules, ruleSeperator);
            Assert.AreEqual(expected, actual);

            queryString = "?utm_source=google&utm_content=code-control&utm_medium=ppc&utm_campaign=GO178WEB&utm_term=pest-control&a=ABC&b=BCD";
            replacementRules = "utm_campaign={a}";
            expected = "?utm_source=google&utm_content=code-control&utm_medium=ppc&utm_campaign=ABC&utm_term=pest-control&a=ABC&b=BCD";
            actual = HttpUtils.ReplaceQSValues(queryString, replacementRules, ruleSeperator);
            Assert.AreEqual(expected, actual);

            queryString = "?utm_source=google&utm_content=code-control&utm_medium=ppc&utm_campaign=GO178WEB&a=ABC&utm_term=pest-control&b=BCD";
            replacementRules = "utm_campaign={b}";
            expected = "?utm_source=google&utm_content=code-control&utm_medium=ppc&utm_campaign=BCD&a=ABC&utm_term=pest-control&b=BCD";
            actual = HttpUtils.ReplaceQSValues(queryString, replacementRules, ruleSeperator);
            Assert.AreEqual(expected, actual);

            // Case 3004
            queryString = "?utm_source=google&utm_content=code-control&utm_medium=ppc&a=ABC&utm_term=pest-control&b=BCD";
            replacementRules = "utm_campaign={b}";
            expected = "?utm_source=google&utm_content=code-control&utm_medium=ppc&a=ABC&utm_term=pest-control&b=BCD";
            actual = HttpUtils.ReplaceQSValues(queryString, replacementRules, ruleSeperator);
            Assert.AreEqual(expected, actual);
        }
    }
}
