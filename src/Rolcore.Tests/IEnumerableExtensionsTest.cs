//-----------------------------------------------------------------------
// <copyright file="IEnumerableExtensionsTest.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.Collections.Generic;

    /// <summary>
    /// Test class for <see cref="IEnumerableExtensions"/>.
    /// </summary>
    [TestClass]
    public class IEnumerableExtensionsTest
    {
        /// <summary>
        /// Tests that <see cref="IEnumerableExtensions.Combine"/> combines all the items given 
        /// with the proper appended and prepended values.
        /// </summary>
        [TestMethod]
        public void Combine_CombinesAllArgs()
        {
            string[] target = 
            { 
                "item 1",
                "item 2",
                "item 3" 
            };

            const string 
                Prepend = "<ul>", 
                Append = "</ul>",
                ItemPrepend = "<li>",
                ItemAppend = "</li>",
                Expected = "<ul><li>item 1</li><li>item 2</li><li>item 3</li></ul>";

            string actual = target.Combine(Prepend, Append, ItemPrepend, ItemAppend);
            
            Assert.AreEqual(Expected, actual);
        }

        /// <summary>
        /// Tests that <see cref="IEnumerableExtensions.Combine"/> combines all the items given 
        /// even though no appends or prepends were specified.
        /// </summary>
        [TestMethod]
        public void Combine_AcceptsNullAppendAndPrependArgs()
        {
            string[] target = 
            { 
                "item 1",
                "item 2",
                "item 3" 
            };

            const string Expected = "item 1item 2item 3";

            string actual = target.Combine(null, null, null, null);

            Assert.AreEqual(Expected, actual);
        }
    }
}
