//-----------------------------------------------------------------------
// <copyright file="RandomAssignmentExperimentItem.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Science
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents an item that is part of a <see cref="RandomAssignmentExperiment"/>.
    /// </summary>
    /// <typeparam name="T">The type of item being experimented on.</typeparam>
    public class RandomAssignmentExperimentItem<T> : ExperimentItem<T>
    {
        private int _OddsNumerator = 1;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="item">The item to be randomly selected as part of the experiment.</param>
        public RandomAssignmentExperimentItem(T item) : base(item) { }

        /// <summary>
        /// Gets and sets the numerator part of the odds of selecting the current item during a 
        /// <see cref="RandomAssignmentExperiment"/>.
        /// </summary>
        public int OddsNumerator
        {
            get
            {
                Debug.Assert(this._OddsNumerator >= 0);
                return _OddsNumerator;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value", "OddsNumerator must be zero or greater");

                this._OddsNumerator = value;
            }
        }
    }
}
