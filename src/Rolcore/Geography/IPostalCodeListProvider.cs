using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rolcore.Geography
{
    public interface IPostalCodeListProvider
    {
        IEnumerable<string> GetPostalCodes();
    }
}
