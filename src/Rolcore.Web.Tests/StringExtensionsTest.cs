//-----------------------------------------------------------------------
// <copyright file="StringExtensionsTest.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------

namespace Rolcore.Web.Tests
{
    using Rolcore.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    
    /// <summary>
    /// Tests for <see cref="StringExtensions"/>.
    /// </summary>
    [TestClass]
    public class StringExtensionsTest
    {
        /// <summary>
        /// A test for ContainsHtml
        /// </summary>
        [TestMethod]
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
        [TestMethod]
        public void ToUriTest()
        {
            string expected = "http://www.rolcore.com/";
            string actual = expected.ToUri().ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToUri
        ///</summary>
        [TestMethod, ExpectedException(typeof(ArgumentException))]
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
        //[TestMethod]
        //public void ContainsHtmlNegativeTest()
        //{
        //}
    }
}
