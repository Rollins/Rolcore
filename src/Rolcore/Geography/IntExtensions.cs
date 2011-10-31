using System;
using System.Diagnostics.Contracts;

namespace Rolcore.Geography
{
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

            Contract.Requires<ArgumentOutOfRangeException>(i <= 99999, "i is greater than five digits.");

            //
            // Post-conditions

            Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
            Contract.Ensures(Contract.Result<string>().Length == 5);

            //
            // Pad zeros

            return i.ToString().PadLeft(5, '0');
        }

        public static string ToUsa5DigitPostalCode(this int i)
        {
            Contract.Requires<ArgumentOutOfRangeException>(i >= 0, "i is negative.");

            return Convert.ToUInt32(i).ToUsa5DigitPostalCode();
        }
    }
}
