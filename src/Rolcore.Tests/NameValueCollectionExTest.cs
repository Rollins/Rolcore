//-----------------------------------------------------------------------
// <copyright file="NameValueCollectionExTest.cs" company="Rollins, Inc.">
//     Copyright © 2013 Rollins, Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.Collections.Specialized;

    /// <summary>
    /// This is a test class for NameValueCollectionExTest and is intended
    /// to contain all NameValueCollectionExTest Unit Tests
    /// </summary>
    [TestClass]
    public class NameValueCollectionExTest
    {
        /// <summary>
        /// Tests that ToReadOnly returns a read-only NameValueCollection.
        /// </summary>
        [TestMethod, ExpectedException(typeof(System.NotSupportedException))]
        public void ToReadOnly_ReturnsReadOnlyNameValueCollection()
        {
            NameValueCollectionEx target = new NameValueCollectionEx();
            target.Add("name", "value");
            NameValueCollectionEx actual = target.ToReadOnly();
            actual.Add("exception", "here");
        }
    }
}
