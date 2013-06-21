//-----------------------------------------------------------------------
// <copyright file="RandomAssignmentExperiment.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
using System.Diagnostics.Contracts;
namespace Rolcore.Science
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Uses the Random assignment experimental technique to assign subjects to a different "item"
    /// randomly. By randomizing assignment each group of participants is roughly equivelant and 
    /// therefore any effects observed between each "item" can be linked to the item's effect and
    /// not a characteristic of an individual participant.
    /// <seealso cref="http://en.wikipedia.org/wiki/Random_assignment"/>
    /// </summary>
    public class RandomAssignmentExperiment<T> : List<RandomAssignmentExperimentItem<T>>
    {

        #region Constructors
        public RandomAssignmentExperiment() { }
        public RandomAssignmentExperiment(int capacity) : base(capacity) { }
        public RandomAssignmentExperiment(IEnumerable<RandomAssignmentExperimentItem<T>> collection) : base(collection) { }
        #endregion Constructors

        /// <summary>
        /// Gets the denominator for the overall odds of executing a particular experiment item.
        /// <seealso cref="RandomAssignmentExperimentItem"/>
        /// </summary>
        public int OddsDenominator
        {
            get { return this.Sum(item => item.OddsNumerator); }
        }

        public RandomAssignmentExperimentItem<T> Add(T item, int oddsNumerator)
        {
            //TODO: Unit Test
            RandomAssignmentExperimentItem<T> addItem = new RandomAssignmentExperimentItem<T>(item){
                OddsNumerator = oddsNumerator };

            this.Add(addItem);

            return addItem;
        }

        public RandomAssignmentExperimentItem<T>[] Add(IEnumerable<T> items, int oddsNumerator)
        {
            Contract.Requires<ArgumentNullException>(items != null, "items is null.");

            var result = new List<RandomAssignmentExperimentItem<T>>(items.Count());
            foreach (T item in items)
            {
                result.Add(this.Add(item, oddsNumerator));
            }

            return result.ToArray();

            //TODO: Unit Test
        }

        public int IndexOf(T item)
        {
            RandomAssignmentExperimentItem<T> searchItem = this
                .Where(experimentItem => experimentItem.Item.Equals(item))
                .FirstOrDefault();

            if (searchItem == null)
                return -1;

            return this.IndexOf(searchItem);

            //TODO: Unit Test
        }


        public virtual T PickItem()
        {
            Contract.Requires<InvalidOperationException>(this.OddsDenominator >= 0, "OddsDenominator is less than zero.");
            Contract.Requires<InvalidOperationException>(this.Count > 0, "No items have been configured as part of the experiment.");

            int maxRandomNumber = this.OddsDenominator + 1;
            int randomNumber = ThreadSafeRandom.Next(1, maxRandomNumber);

            int runningTotal = 0;
            foreach (RandomAssignmentExperimentItem<T> experimentItem in this)
            {
                runningTotal += experimentItem.OddsNumerator;
                if (runningTotal >= randomNumber)
                    return experimentItem.Item;
            }

            throw new InvalidOperationException("Failed to adhere to the laws of probability.");
        }
    }
}
