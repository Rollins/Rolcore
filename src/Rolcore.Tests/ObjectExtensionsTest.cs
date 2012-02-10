using Rolcore.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using Rolcore.Tests.MockObjects;

namespace Rolcore.Tests
{
    
    
    /// <summary>
    ///This is a test class for ObjectExtensionsTest and is intended
    ///to contain all ObjectExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ObjectExtensionsTest
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
        ///A test for GetMethodsWithAttribute
        ///</summary>
        [TestMethod(), ExpectedException(typeof(ArgumentException))]
        public void GetMethodsWithAttributeInvalidAttributeTest()
        {
            object instance = new ReflectionUtilsMockObject();
            Type attributeType = typeof(object);
            ObjectExtensions.GetMethodsWithAttribute(instance, attributeType, false);
        }

        //TODO: [TestMethod()] public void GetMethodsWithAttributeInheritedTest() { }

        /// <summary>
        ///A test for GetMethodsWithAttribute
        ///</summary>
        [TestMethod()]
        public void GetMethodsWithAttributeNotIneritedTest()
        {
            object instance = new ReflectionUtilsMockObject();
            Type attributeType = typeof(ReflectionUtilsMockAttribute);
            MethodInfo[] actual = ObjectExtensions.GetMethodsWithAttribute(instance, attributeType, false);
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("VoidMethodWithAttribute", actual[0].Name);
        }

        /// <summary>
        ///A test for CopyMatchingObjectPropertiesTo
        ///</summary>
        [TestMethod()]
        public void CopyMatchingObjectPropertiesToTest()
        {
            var source = new
            {
                IntPropNonNullable = 1,
                IntPropNullable = (int?)null,
                StringProp = "Source String",
                ExtraProperty = "foobar",
                SubObject = new
                {
                    IntPropNonNullable = 1,
                    IntPropNullable = (int?)null,
                    StringProp = "Source String",
                }
            };
            var dest = new ReflectionUtilsMockObject()
            {
                IntPropNonNullable = 10,
                IntPropNullable = 20,
                StringProp = "Destination String"
            };
            Assert.AreNotEqual(source.IntPropNonNullable, dest.IntPropNonNullable, "IntPropNonNullable");
            Assert.AreNotEqual(source.IntPropNullable, dest.IntPropNullable, "IntPropNullable");
            Assert.AreNotEqual(source.StringProp, dest.StringProp, "StringProp");
            Assert.AreNotEqual(source.SubObject.IntPropNonNullable, dest.IntPropNonNullable, "SubObject.IntPropNonNullable");
            Assert.AreNotEqual(source.SubObject.IntPropNullable, dest.IntPropNullable, "SubObject.IntPropNullable");
            Assert.AreNotEqual(source.SubObject.StringProp, dest.StringProp, "SubObject.StringProp");

            ObjectExtensions.CopyMatchingObjectPropertiesTo(source, dest);

            Assert.AreEqual(source.IntPropNonNullable, dest.IntPropNonNullable, "IntPropNonNullable");
            Assert.AreEqual(source.IntPropNullable, dest.IntPropNullable, "IntPropNullable");
            Assert.AreEqual(source.StringProp, dest.StringProp, "StringProp");
            Assert.AreEqual(source.SubObject.IntPropNonNullable, dest.IntPropNonNullable, "SubObject.IntPropNonNullable");
            Assert.AreEqual(source.SubObject.IntPropNullable, dest.IntPropNullable, "SubObject.IntPropNullable");
            Assert.AreEqual(source.SubObject.StringProp, dest.StringProp, "SubObject.StringProp");
        }
    }
}
