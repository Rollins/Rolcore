//-----------------------------------------------------------------------
// <copyright file="RandomAssignmentExperimentTest.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.Science;

    /// <summary>
    /// This is a test class for RandomAssignmentExperimentTest and is intended
    /// to contain all RandomAssignmentExperimentTest Unit Tests
    /// </summary>
    [TestClass]
    public class RandomAssignmentExperimentTest
    {
        /// <summary>
        /// Tests that the denominator is the sum of all enumerators.
        /// </summary>
        [TestMethod]
        public void OddsDenominator_IsSumOfNumerators()
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

        /// <summary>
        /// Tests that an experiment item with 100% probability of being selected is always 
        /// selected.
        /// </summary>
        [TestMethod]
        public void OddsNumerator_IsAlwaysSelectedWhenProbabilityIs100Percent()
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

        /// <summary>
        /// Tests that <see cref="RandomAssignmentExperiment{}.PickItem"/> does not produce 
        /// anomalous results
        /// </summary>
        [TestMethod]
        public void PickItem_RandomSelectionReflectsDefinedProbability()
        {
            const string NotExpected = "First Item";
            const string SometimesExpectedItem = "Second Item";
            const string MostlyExpected = "Third Item";

            RandomAssignmentExperiment<string> target = new RandomAssignmentExperiment<string>();
            target.Add(new RandomAssignmentExperimentItem<string>(NotExpected)
            {
                OddsNumerator = 0 // 0 chance
            });
            target.Add(new RandomAssignmentExperimentItem<string>(SometimesExpectedItem)
            {
                OddsNumerator = 2 // 2/5 chance, or 40%
            });
            target.Add(new RandomAssignmentExperimentItem<string>(MostlyExpected)
            {
                OddsNumerator = 3 // 3/5 chance, or 60%
            });

            const int PickCount = 10000;

            List<string> actuals = new List<string>(PickCount);

            // TODO: Use Parallel.For when we upgrade to .NET 4.0.
            for (int i = 0; i < PickCount; i++) 
            {
                string actual = target.PickItem();
                actuals.Add(actual);
                Assert.AreNotEqual(actual, NotExpected);
            }

            Assert.AreEqual(PickCount, actuals.Count);

            int sometimesExpectedItemCount = actuals.Count(item => item.Equals(SometimesExpectedItem));
            int mostlyExpectedItemCount = actuals.Count(item => item.Equals(MostlyExpected));

            Assert.AreEqual(PickCount, sometimesExpectedItemCount + mostlyExpectedItemCount);

            Assert.IsTrue(
                sometimesExpectedItemCount < mostlyExpectedItemCount, 
                string.Format("Sometimes: {0}. Mostly: {1}", sometimesExpectedItemCount, mostlyExpectedItemCount));

            //// Verify split was within 50 picks, or 5% of a "perfect" outcome (400:600)

            Assert.IsTrue(sometimesExpectedItemCount >= 3750, "Anomalous result (sometimesExpectedItemCount): " + sometimesExpectedItemCount);
            Assert.IsTrue(sometimesExpectedItemCount <= 4250, "Anomalous result (sometimesExpectedItemCount): " + sometimesExpectedItemCount);

            Assert.IsTrue(mostlyExpectedItemCount >= 5750, "Anomalous result (mostlyExpectedItemCount): " + mostlyExpectedItemCount);
            Assert.IsTrue(mostlyExpectedItemCount <= 6250, "Anomalous result (mostlyExpectedItemCount): " + mostlyExpectedItemCount);
        }
    }
}
