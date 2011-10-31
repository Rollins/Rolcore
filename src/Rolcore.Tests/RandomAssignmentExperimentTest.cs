using Rolcore.Science;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
namespace Rolcore.Tests
{
    
    
    /// <summary>
    ///This is a test class for RandomAssignmentExperimentTest and is intended
    ///to contain all RandomAssignmentExperimentTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RandomAssignmentExperimentTest
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
        public void OddsDenominatorTest()
        {
            RandomAssignmentExperiment<string> target = new RandomAssignmentExperiment<string>();
            target.Add(new RandomAssignmentExperimentItem<string>("First Item")
            {
                OddsNumerator = 0
            });
            target.Add(new RandomAssignmentExperimentItem<string>("Second Item")
            {
                OddsNumerator = 1
            });
            target.Add(new RandomAssignmentExperimentItem<string>("Third Item")
            {
                OddsNumerator = 2
            });
            int actual = target.OddsDenominator;
            Assert.AreEqual(actual, 3);
        }

        [TestMethod()]
        public void PickItemWith100PercentProbabilityTest()
        {
            string expected = "Second Item";
            RandomAssignmentExperiment<string> target = new RandomAssignmentExperiment<string>();
            target.Add(new RandomAssignmentExperimentItem<string>("First Item")
            {
                OddsNumerator = 0
            });
            target.Add(new RandomAssignmentExperimentItem<string>(expected)
            {
                OddsNumerator = 1
            });
            target.Add(new RandomAssignmentExperimentItem<string>("Third Item")
            {
                OddsNumerator = 0
            });

            for (int i = 0; i < 100; i++)
            {
                string actual = target.PickItem();
                Assert.AreEqual(actual, expected);
            }
        }

        [TestMethod()]
        public void PickItemWithUnevenProbabilityTest()
        {
            string notExpected = "First Item";
            string sometimesExpectedItem = "Second Item";
            string mostlyExpected = "Third Item";
            RandomAssignmentExperiment<string> target = new RandomAssignmentExperiment<string>();
            target.Add(new RandomAssignmentExperimentItem<string>(notExpected)
            {
                OddsNumerator = 0 // 0 chance
            });
            target.Add(new RandomAssignmentExperimentItem<string>(sometimesExpectedItem)
            {
                OddsNumerator = 2 // 2/5 chance, or 40%
            });
            target.Add(new RandomAssignmentExperimentItem<string>(mostlyExpected)
            {
                OddsNumerator = 3 // 3/5 chance, or 60%
            });

            int pickCount = 1000;

            List<string> actuals = new List<string>(pickCount);

            for (int i = 0; i < pickCount; i++) //TODO: Use Parallel.For when we upgrade to .NET 4.0.
            {
                string actual = target.PickItem();
                actuals.Add(actual);
                Assert.AreNotEqual(actual, notExpected);
            }

            Assert.AreEqual(pickCount, actuals.Count);

            int sometimesExpectedItemCount = actuals.Count(item => item.Equals(sometimesExpectedItem));
            int mostlyExpectedItemCount = actuals.Count(item => item.Equals(mostlyExpected));

            Assert.AreEqual(pickCount, sometimesExpectedItemCount + mostlyExpectedItemCount);

            Assert.IsTrue(sometimesExpectedItemCount < mostlyExpectedItemCount, "Sometimes: " + sometimesExpectedItemCount + ". Mostly: " + mostlyExpectedItemCount);

            //
            // Verify split was within 50 picks, or 5% of a "perfect" outcome (400:600)

            Assert.IsTrue(sometimesExpectedItemCount >= 375, "Anomolous result (sometimesExpectedItemCount): " + sometimesExpectedItemCount);
            Assert.IsTrue(sometimesExpectedItemCount <= 425, "Anomolous result (sometimesExpectedItemCount): " + sometimesExpectedItemCount);

            Assert.IsTrue(mostlyExpectedItemCount >= 575, "Anomolous result (mostlyExpectedItemCount): " + mostlyExpectedItemCount);
            Assert.IsTrue(mostlyExpectedItemCount <= 625, "Anomolous result (mostlyExpectedItemCount): " + mostlyExpectedItemCount);
        }
    }
}
