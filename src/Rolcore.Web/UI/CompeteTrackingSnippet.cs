using System;
using System.ComponentModel;
using System.Web.UI;

namespace Rolcore.Web.UI
{
    [ToolboxData("<{0}:CompeteTrackingSnippet  runat=\"server\"></{0}:CompeteTrackingSnippet >")]
    public class CompeteTrackingSnippet : Control
    {
        protected const string CompeteTrackingCodeDefaultValue = "e8581ab8df2c8c9f60b9a0f1ae8701f4";

        [Bindable(true), Category("Compete Tracking"), DefaultValue(CompeteTrackingCodeDefaultValue),
         Description("Gets and sets the the __compete_code.")]
        public string CompeteTrackingCode
        {
            get
            {
                string result = (string)ViewState["CompeteTrackingCode"] ?? CompeteTrackingCodeDefaultValue;
                return result;
            }
            set { ViewState["CompeteTrackingCode"] = value; }
        }

        protected override void Render(HtmlTextWriter output)
        {
            RenderingUtils.RenderJavaScriptBeginTag(output);

            output.Write(@"
                __compete_code = '" + CompeteTrackingCode + @"';
                (function () {
                    var s = document.createElement('script'),
                        e = document.getElementsByTagName('script')[0],
                        t = document.location.protocol.toLowerCase() === 'https:' ?
                            'https://c.compete.com/bootstrap/' :
                            'http://c.compete.com/bootstrap/';
                        s.src = t + __compete_code + '/bootstrap.js';
                        s.type = 'text/javascript';
                        s.async = true;
                        if (e) { e.parentNode.insertBefore(s, e); }
                    }());");

            output.RenderEndTag();
        }
    }
}
