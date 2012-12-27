//-----------------------------------------------------------------------
// <copyright file="ExperimentItem.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Science
{
    /// <summary>
    /// Base class for an item that is part of an <see cref="Experiment"/>.
    /// </summary>
    /// <typeparam name="T">Specifies the type of item taking part in the experiment.</typeparam>
    public abstract class ExperimentItem<T>
    {
        /// <summary>
        /// The item instance taking part in the experiment.
        /// </summary>
        private readonly T item;

        /// <summary>
        /// Initializes a new instance of ExperimentItem.
        /// </summary>
        /// <param name="item">Specifies the item taking part in the experiment.</param>
        public ExperimentItem(T item)
        {
            this.item = item;
        }

        /// <summary>
        /// Gets the item taking part in the experiment.
        /// </summary>
        public T Item
        {
            get { return this.item; }
        }
    }
}
