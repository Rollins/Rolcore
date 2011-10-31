using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Rolcore.Web.UI
{
    public class RenderingUtils
    {
        public static void RenderJavaScriptBeginTag(HtmlTextWriter output)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Type, ContentTypes.TextJavaScript);
            output.RenderBeginTag(HtmlTextWriterTag.Script);
        }
    }
}
