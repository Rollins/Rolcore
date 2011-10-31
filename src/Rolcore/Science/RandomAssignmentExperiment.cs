using System;
using System.Collections.Generic;
using System.Linq;

namespace Rolcore.Science
{
    /// <summary>
    /// Uses the Random assignment experimental technique to assign subjects to a different "item"
    /// randomly. By randomizing assignment each group of participants is roughly equivelant and 
    /// therefore any effects observed between each "item" can be linked to the item's effect and
    /// not a charactaristic of an individual participant.
    /// <seealso cref="http://en.wikipedia.org/wiki/Random_assignment"/>
    /// </summary>
    public class RandomAssignmentExperiment<T> : List<RandomAssignmentExperimentItem<T>>
    {
        private DateRange _ActiveDateRange = new DateRange(null, null);

        #region Constructors
        public RandomAssignmentExperiment() {}
        public RandomAssignmentExperiment(int capacity) : base(capacity){}
        public RandomAssignmentExperiment(IEnumerable<RandomAssignmentExperimentItem<T>> collection): base(collection){}
        #endregion Constructors

        /// <summary>
        /// Gets a value indicating if the experiment is currently active.
        /// </summary>
        public virtual bool Active
        {
            get 
            {
                bool result = this.ActiveDateRange.Includes(DateTime.Now); 
                return result; 
            }
        }

        /// <summary>
        /// Gets the denominator for the overall odds of executing a particular experiment item.
        /// <seealso cref="RandomAssignmentExperimentItem"/>
        /// </summary>
        public int OddsDenominator
        {
            get { return this.Sum(item => item.OddsNumerator); }
        }

        /// <summary>
        /// Gets and sets the active <see cref="DateRange"/> for which the experiment is active.
        /// </summary>
        public DateRange ActiveDateRange
        {
            //TODO: Unit test
            get { return this._ActiveDateRange; }
            set { this._ActiveDateRange = value ?? new DateRange(null, null); }
        }

        //TODO: public Fraction OddsOf(RandomAssignmentExperimentItem<T> item) { }
        //TODO: public Fraction OddsOf(int itemIndex) { }

        public RandomAssignmentExperimentItem<T> Add(T item, int oddsNumerator)
        {
            //TODO: Unit Test
            RandomAssignmentExperimentItem<T> addItem = new RandomAssignmentExperimentItem<T>(item) {  
                OddsNumerator = oddsNumerator  };

            this.Add(addItem);

            return addItem;
        }

        public RandomAssignmentExperimentItem<T>[] Add(IEnumerable<T> items, int oddsNumerator)
        {
            //TODO: Unit Test
            List<RandomAssignmentExperimentItem<T>> result = new List<RandomAssignmentExperimentItem<T>>(items.Count());
            foreach (T item in items)
                result.Add(this.Add(item, oddsNumerator));

            return result.ToArray();
        }

        public int IndexOf(T item)
        {
            //TODO: Unit Test
            RandomAssignmentExperimentItem<T> searchItem = this
                .Where(experimentItem => experimentItem.Item.Equals(item))
                .FirstOrDefault();

            if (searchItem == null)
                return -1;

            return this.IndexOf(searchItem);
        }


        public virtual T PickItem()
        {
            if (!this.Active)
                throw new InvalidOperationException("Cannot pick an item while experiment is inactive.");
            if (this.Count == 0)
                throw new InvalidOperationException("No items have been configured as part of the experiment.");

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
