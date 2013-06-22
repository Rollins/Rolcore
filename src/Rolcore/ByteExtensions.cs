using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rolcore
{
    /// <summary>
    /// Extension methods for <see cref="byte"/>.
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="byte"/> to a hexadecimal string notation.
        /// </summary>
        /// <param name="b">The byte to convert.</param>
        /// <returns>A hexadecimal string representation of the byte.</returns>
        public static string ToHexString(this byte b)
        {
            return string.Format("{0:x2}", b);
        } // Tested

        /// <summary>
        /// Converts the specified <see cref="byte"/> array to a hexadecimal string notation.
        /// </summary>
        /// <param name="bytes">The bytes to convert.</param>
        /// <returns>A hexadecimal string representation of the byte array.</returns>
        public static string ToHexString(this byte[] bytes)
        {
            var result = new StringBuilder();
            foreach (var b in bytes)
            {
                result.Append(b.ToHexString());
            }

            return result.ToString();
        } // Tested
    }
}
