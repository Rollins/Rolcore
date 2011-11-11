﻿using Rolcore.Web.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rollins.Web.Tests
{
    
    
    /// <summary>
    ///This is a test class for ControlExtensionsTest and is intended
    ///to contain all ControlExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ControlExtensionsTest
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
        ///A test for RenderControlToString
        ///</summary>
        [TestMethod()]
        public void RenderControlToStringTest()
        {
            using (Control control = new Button { ID = "TestButton", Text = "Click me!" })
            {
                const string expected = "<input type=\"submit\" name=\"TestButton\" value=\"Click me!\" id=\"TestButton\" />";
                string actual = ControlExtensions.RenderControlToString(control);
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
