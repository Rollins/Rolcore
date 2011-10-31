using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Threading;
using System.Globalization;

namespace Rolcore.Web.UI
{
    [ToolboxData("<{0}:MetaContentLanguage runat=\"server\"></{0}:MetaContentLanguage>")]
    public class MetaContentLanguage : Control
    {
        protected override void Render(HtmlTextWriter output)
        {
            string currentCultureName = Thread.CurrentThread.CurrentCulture.Name;
            if(!string.IsNullOrWhiteSpace(currentCultureName))
                output.Write("<meta http-equiv=\"content-language\" content=\"{0}\"/>", currentCultureName);
        }
    }
}
