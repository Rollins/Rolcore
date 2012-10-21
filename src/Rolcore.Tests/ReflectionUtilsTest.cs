using Rolcore.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using Rolcore.Tests.MockObjects;

namespace Rolcore.Tests
{
    
    
    /// <summary>
    ///This is a test class for ReflectionUtilsTest and is intended
    ///to contain all ReflectionUtilsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ReflectionUtilsTest
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
        ///A test for GetPropertyValue, includes "dot syntax" test.
        ///</summary>
        [TestMethod()]
        public void GetPropertyValueTest()
        {
            string propertyName = "IntPropNonNullable";
            int expected = (new Random()).Next(int.MaxValue);
            object obj = new ReflectionUtilsMockObject() { IntPropNonNullable = expected };
            object actual = obj.GetPropertyValue(propertyName);
            Assert.AreEqual(expected, actual);

            propertyName = "SubObject.IntPropNonNullable";
            actual = obj.GetPropertyValue(propertyName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsNullableType
        ///</summary>
        [TestMethod(), Ignore]
        public void IsNullableTypeTest()
        {
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReplaceVars
        ///</summary>
        [TestMethod(), Ignore]
        public void ReplaceVarsTest()
        {
            string s = string.Empty; // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual = ReflectionUtils.ReplaceVars(s, obj);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SetPropertyValue, includes "dot syntax" test.
        ///</summary>
        [TestMethod()]
        public void SetPropertyValueTest()
        {
            string propertyName = "IntPropNonNullable";
            int random = (new Random()).Next(int.MaxValue);
            int expected = random - (new Random()).Next(int.MaxValue);
            object obj = new ReflectionUtilsMockObject() { IntPropNonNullable = random };
            obj.SetPropertyValue(propertyName, expected);
            object actual = ((ReflectionUtilsMockObject)obj).IntPropNonNullable;
            Assert.AreEqual(expected, actual);

            propertyName = "SubObject.IntPropNonNullable";
            obj.SetPropertyValue(propertyName, expected);
            actual = ((ReflectionUtilsMockObject)obj).SubObject.IntPropNonNullable;
            Assert.AreEqual(expected, actual);
        }
    }
}
