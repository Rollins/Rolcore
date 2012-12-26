//-----------------------------------------------------------------------
// <copyright file="IntExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Geography
{
    using System;

    /// <summary>
    /// Extension methods for <see cref="int"/>.
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Converts the given <see cref="int"/> to a 5-digit USA zip code.
        /// </summary>
        /// <param name="i">Specifies the number to convert.</param>
        /// <returns>A zero-padded, 5-digit zip code.</returns>
        public static string ToUsa5DigitPostalCode(this uint i)
        {
            //
            // Pre-conditions

            if (i > 99999)
                throw new ArgumentOutOfRangeException("i is greater than five digits.");

            //
            // Pad zeros

            return i.ToString().PadLeft(5, '0');
        }

        public static string ToUsa5DigitPostalCode(this int i)
        {
            if (i < 0)
                throw new ArgumentOutOfRangeException("i is negative.");

            return Convert.ToUInt32(i).ToUsa5DigitPostalCode();
        }
    }
}
