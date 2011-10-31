using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rolcore.Text.RegularExpressions
{
    public sealed class CommonExpressions
    {
        public const string HtmlTag = @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>";
    }
}
