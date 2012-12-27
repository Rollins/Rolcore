//-----------------------------------------------------------------------
// <copyright file="ReflectionUtilsTest.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.Reflection;
    using Rolcore.Tests.MockObjects;
    
    /// <summary>
    /// Tests for <see cref="ReflectionUtils"/> .
    ///</summary>
    [TestClass]
    public class ReflectionUtilsTest
    {
        /// <summary>
        ///A test for GetPropertyValue, includes "dot syntax" test.
        ///</summary>
        [TestMethod]
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
        ///A test for SetPropertyValue, includes "dot syntax" test.
        ///</summary>
        [TestMethod]
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
