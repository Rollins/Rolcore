//-----------------------------------------------------------------------
// <copyright file="StringExtensionsTest.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Tests.IO
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.IO;

    /// <summary>
    /// Test methods for <see cref="StringExtensions"/>.
    /// </summary>
    [TestClass]
    public class StringExtensionsTest
    {
        /// <summary>
        /// Tests that <see cref="StringExtensions.ToStream"/> successfully converts a string to a
        /// stream.
        /// </summary>
        [TestMethod]
        public void ToStream_ConvertsStringToStream()
        {
            const string expected = "Hello World! שלום עולם 你好世界";
            using (var stream = expected.ToStream())
            {
                var actual = stream.ReadToEndAsString();
                Assert.AreEqual(expected, actual, "Stream did not convert back to expected string.");
            }
        }
    }
}
