using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Rolcore.IO
{
    public static class StringExtensions
    {
        public static TextReader ToTextReader(this string s)
        {
            return new StringReader(s);
        }
    }
}
