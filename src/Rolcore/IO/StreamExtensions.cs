

namespace Rolcore.IO
{
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;

    /// <summary>
    /// Extension methods for <see cref="Stream"/>.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads the specified string from the current position to the end as a string.
        /// </summary>
        /// <param name="stream">Specifies the stream to read.</param>
        /// <returns>The <see cref="String"/> contents of the stream.</returns>
        public static string ReadToEndAsString(this Stream stream)
        {
            Contract.Requires<ArgumentNullException>(stream != null, "stream is null");

            using (var reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
                return result;
            }
        } // TODO: Test
    }
}
