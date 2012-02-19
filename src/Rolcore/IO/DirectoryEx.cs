using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace Rolcore.IO
{
    public static class DirectoryEx 
    {
        public static IEnumerable<string> EnumerateFilesFormattedSearchPattern(string path, string formattedSearchPattern, SearchOption searchOption, params IFormattable[] formatters)
        {
            if (formatters == null || formatters.Length == 0)
            {
                foreach (string result in Directory.EnumerateFiles(path, formattedSearchPattern, searchOption))
                    yield return result;

            }else foreach (IFormattable formatter in formatters)
            {
                string searchPattern = formatter.ToString(formattedSearchPattern, null);
                Debug.WriteLine("SearchPattern: " + searchPattern);
                foreach (string result in Directory.EnumerateFiles(path, searchPattern, searchOption))
                    yield return result;
            }
        }
    }
}
