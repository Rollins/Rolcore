//-----------------------------------------------------------------------
// <copyright file="ThreadSafeRandom.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore
{
    using System;

    /// <summary>
    /// Provides a thread-safe, statistically random operations. Use instead of 
    /// <see cref="Random"/>.
    /// </summary>
    public static class ThreadSafeRandom
    {
        /// <summary>
        /// Provides random seed for thread-safe <see cref="Random"/> instances.
        /// </summary>
        private readonly static Random _Global = new Random();

        /// <summary>
        /// The thread-local <see cref="Random"/> instance used to perform random functions.
        /// </summary>
        [ThreadStatic]
        private static Random _local;

        /// <summary>
        /// Ensures a thread-local <see cref="Random"/> instance is available.
        /// </summary>
        private static void EnsureLocal()
        {
            if (_local == null)
            {
                int seed;
                lock (_Global) seed = _Global.Next();
                _local = new Random(seed);
            }
        }

        /// <summary>
        /// Returns a non-negative random number.
        /// </summary>
        public static int Next()
        {
            EnsureLocal();

            return _local.Next();
        }

        /// <summary>
        /// Returns a non-negative random number less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. 
        /// Must be greater than or equal to zero.</param>
        public static int Next(int maxValue)
        {
            EnsureLocal();

            return _local.Next(maxValue);
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. 
        /// Must be greater than or equal to zero.</param>
        public static int Next(int minValue, int maxValue)
        {
            EnsureLocal();

            return _local.Next(minValue, maxValue);
        }
    }
}
