//-----------------------------------------------------------------------
// <copyright file="ListExtensionMethodsTest.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.Collections;

    /// <summary>
    ///This is a test class for ListExtensionMethodsTest and is intended
    ///to contain all ListExtensionMethodsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ListExtensionMethodsTest
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

        [TestMethod()]
        public void DuplicatesTest()
        {
            // A little bit of Hamlet

            List<string> list1 = new List<string>();
            list1.Add("To be, or not to be: that is the question.");
            list1.Add("Neither a borrower nor a lender be; For loan oft loses both itself and friend, and borrowing dulls the edge of husbandry.");
            list1.Add("That it should come to this!");
            list1.Add("his above all: to thine own self be true.");

            List<string> list2 = new List<string>();
            list2.Add("To be, or not to be: that is the question.");
            list2.Add("Neither a borrower nor a lender be; For loan oft loses both itself and friend, and borrowing dulls the edge of husbandry.");
            list2.Add("his above all: to thine own self be true.");
            list2.Add("Though this be madness, yet there is method in 't.");

            List<string> expected = new List<string>();
            expected.Add("To be, or not to be: that is the question.");
            expected.Add("Neither a borrower nor a lender be; For loan oft loses both itself and friend, and borrowing dulls the edge of husbandry.");
            expected.Add("his above all: to thine own self be true.");

            List<string> unexpected = new List<string>();
            unexpected.Add("That it should come to this!");
            unexpected.Add("Though this be madness, yet there is method in 't.");

            List<string> actual = list1.Duplicates(list2).ToList();
            Assert.AreEqual(expected.Count, actual.Count);
            foreach (string actualItem in actual)
            {
                Assert.IsTrue(expected.Contains(actualItem));
                Assert.IsFalse(unexpected.Contains(actualItem));
            }
        }

        /// <summary>
        ///A test for Shuffle
        ///</summary>
        [TestMethod()]
        public void ShuffleTest()
        {
            object[] list = new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }; // TODO: Initialize to an appropriate value
            Assert.IsTrue(
                list[0].Equals(1) &&
                list[1].Equals(2) &&
                list[2].Equals(3) &&
                list[3].Equals(4) &&
                list[4].Equals(5) &&
                list[5].Equals(6) &&
                list[6].Equals(7) &&
                list[7].Equals(8) &&
                list[8].Equals(9) &&
                list[9].Equals(0)
                );
            list.Shuffle();
            Assert.IsFalse(
                list[0].Equals(1) &&
                list[1].Equals(2) &&
                list[2].Equals(3) &&
                list[3].Equals(4) &&
                list[4].Equals(5) &&
                list[5].Equals(6) &&
                list[6].Equals(7) &&
                list[7].Equals(8) &&
                list[8].Equals(9) &&
                list[9].Equals(0)
                );
        }
    }
}
