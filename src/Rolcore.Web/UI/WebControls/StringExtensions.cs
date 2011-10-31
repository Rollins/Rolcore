using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Rolcore.Web.UI.WebControls
{
    public static class StringExtensions
    {
        public static Literal ToLiteralWebControl(this string s)
        {
            Literal result = new Literal { Text = s };
            return result;
        }
    }
}
