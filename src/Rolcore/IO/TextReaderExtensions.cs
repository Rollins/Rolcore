using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Rolcore.IO
{
    /// <summary>
    /// Extension methods for <see cref="TextReader"/>.
    /// </summary>
    public static class TextReaderExtensions
    {
        /// <summary>
        /// Reads lines from the specified stream and returns the data as an enumerable. If a
        /// <see cref="Regex"/> is specified, only lines matching the expression are yielded.
        /// </summary>
        /// <param name="textReader">Specifies the <see cref="TextReader"/>.</param>
        /// <param name="regex">Specifies the <see cref="Regex"/>. Passing null results in all 
        /// lines being returned.</param>
        /// <returns></returns>
        public static IEnumerable<string> ReadLines(this TextReader textReader, Regex regex = null)
        {
            string result;
            while ((result = textReader.ReadLine()) != null)
                if (regex == null || regex.IsMatch(result))
                    yield return result;
        }
    }
}
