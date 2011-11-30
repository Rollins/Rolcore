using System;
using System.ComponentModel;
using System.Web.UI;

namespace Rolcore.Web.UI
{
    [ToolboxData("<{0}:GoogleAnalyticsSnippet runat=\"server\"></{0}:GoogleAnalyticsSnippet>")]
    public class GoogleAnalyticsSnippet : Control
    {
        protected const string PageTrackerIdDefaultValue = "UA-";
        
        protected bool AllowPageLinker
        {
            get { return this.PageDomainName.Equals("none"); }
        }

        protected bool AllowRollUpLinker
        {
            get { return this.RollupDomainName.Equals("none"); }
        }

        protected bool PageTrackerInUse
        {
            get
            {
                return this.PageTrackerId.Length >= 12;
            }
        }

        protected bool RollupTrackerInUse
        {
            get
            {
                return this.RollupTrackerId.Length >= 12;
            }
        }


        /// <summary>
        /// Renders the script tag that includes the Google Analytics javascript files on the webpage.
        /// </summary>
        /// <param name="output">The <see cref="HtmlTextWriter"/> to which the output is rendered.</param>
        protected static void RenderSnippetStartScript(HtmlTextWriter output)
        {
            RenderingUtils.RenderJavaScriptBeginTag(output);

            output.WriteLine(@"var gaJsHost = ((""https:"" == document.location.protocol) ? ""https://ssl."" : ""http://www."");");
            output.WriteLine(@"document.write(unescape(""%3Cscript src='"" + gaJsHost + ""google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E""));");

            output.RenderEndTag();
        }

        /// <summary>
        /// Renders the body of the Google Analytics tracking javascript.
        /// Updated to Asynchronous Tracking 
        /// </summary>
        /// <param name="output">Where the script is to be rendered.</param>
        protected void RenderSnippetBodyScript(HtmlTextWriter output)
        {
            RenderingUtils.RenderJavaScriptBeginTag(output);
            output.WriteLine(@"try {");

            // Page Tracker
            if (this.PageTrackerInUse)
            {
                output.WriteLine(string.Format(@"var _gaq=_gaq||[]; _gaq.push(['_setAccount','{0}']);", this.PageTrackerId));
                if (!string.IsNullOrEmpty(this.RollupDomainName))
                    output.WriteLine(string.Format(@"_gaq.push(['_setDomainName', '{0}']);", this.PageDomainName));
                if (this.AllowPageLinker)
                    output.WriteLine(@"_gaq.push(['_setAllowLinker', true]);");
                output.WriteLine(@"_gaq.push(['_trackPageview']);");
                output.WriteLine(@"_gaq.push(['_trackPageLoadTime']);");
            }

            // RollUp Tracker
            if (this.RollupTrackerInUse)
            {
                output.WriteLine(string.Format(@"var _gaq1=_gaq||[];  _gaq1.push(['_setAccount','{0}']);", this.RollupTrackerId));
                if (!string.IsNullOrEmpty(this.RollupDomainName))
                    output.WriteLine(string.Format(@"_gaq1.push(['_setDomainName', '{0}']);", this.RollupDomainName));
                if (this.AllowRollUpLinker)
                    output.WriteLine(@"_gaq1.push(['_setAllowLinker', true]);");
                output.WriteLine(@"_gaq1.push(['_trackPageview']);");
                output.WriteLine(@"_gaq1.push(['_trackPageLoadTime']);");
            }

            output.WriteLine(@"} catch(err) { }");
            output.RenderEndTag();
        }

        /// <summary>
        /// Renders javascript which automatically tracks PDF link clicks and form field changes.
        /// </summary>
        /// <param name="output">Where the script is to be rendered.</param>
        protected void RenderAdditionalTrackingScripts(HtmlTextWriter output)
        {
            RenderingUtils.RenderJavaScriptBeginTag(output);

            output.WriteLine(@"$(document).ready(function(){");
            {
                output.Indent++;
                // Page Tracker
                if (this.PageTrackerInUse)
                {
                    // Cross domain linking
                    bool renderPageDomains = !string.IsNullOrEmpty(this.PageTrackerLinkedDomainsCsv);
                    if (renderPageDomains)
                    {
                        string pageDomainsScriptVarName = string.Format("{0}_pDomains", this.ClientID);
                        output.WriteLine(string.Format(
                            @"var {0} = [{1}];",
                            pageDomainsScriptVarName,
                            this.PageTrackerLinkedDomainsCsv));

                        output.WriteLine(
                            string.Format(
                                @"jQuery.each({0}, function(index, domain){{ $(""a[href*=""+domain+""]"").click(function(){{ _gaq.push(['_link',this.href]); }}); }});", 
                                pageDomainsScriptVarName));
                    }
                    // PDF Tracking
                    output.WriteLine(@"$(""a[href*='.pdf']"").click(function(){ _gaq.push(['_trackPageview', this.pathname]); }); ");
                    // Form Tracking
                    if (this.EnableFormFieldChangeTracking)
                    {
                        output.WriteLine(@"$(""input"").change(function(){ _gaq.push(['_trackEvent',""Forms"",""Change"",location.pathname+"" - ""+$(this).attr(""name"")]);}); ");
                        output.WriteLine(@"$(""textarea"").change(function(){ _gaq.push(['_trackEvent',""Forms"",""Change"",location.pathname+"" - ""+$(this).attr(""name"")]);}); ");
                        output.WriteLine(@"$(""select"").change(function(){ _gaq.push(['_trackEvent',""Forms"",""Change"",location.pathname+"" - ""+$(this).attr(""name"")]);}); ");
                    }
                }

                // Rollup Tracker
                if (this.RollupTrackerInUse)
                {
                    // Cross domain linking
                    bool renderRollupDomains = !string.IsNullOrEmpty(this.RollupTrackerLinkedDomainsCsv);
                    if (renderRollupDomains)
                    {
                        string rollupDomainsScriptVarName = string.Format("{0}_rDomains", this.ClientID);
                        output.WriteLine(string.Format(
                            @"var {0} = [{1}];",
                            rollupDomainsScriptVarName,
                            this.RollupTrackerLinkedDomainsCsv));
                        output.WriteLine(string.Format(
                            @"jQuery.each({0}, function(index,domain){{ $(""a[href*=""+domain+""]"").click(function(){{ _gaq1.push(['_link',this.href]);}});}});", 
                            rollupDomainsScriptVarName));
                    }

                    // PDF Tracking
                    output.WriteLine(@"$(""a[href*=.pdf]"").click(function(){  _gaq1.push(['_trackPageview', this.pathname]); }); ");

                    // Form Tracking
                    if (this.EnableFormFieldChangeTracking)
                    {
                        output.WriteLine(@"$(""input"").change(function(){  _gaq1.push(['_trackEvent',""Forms"",""Change"",location.pathname+"" - ""+$(this).attr(""name"")]);   }); ");
                        output.WriteLine(@"$(""textarea"").change(function(){ _gaq1.push(['_trackEvent',""Forms"",""Change"",location.pathname+"" - ""+$(this).attr(""name"")]);   }); ");
                        output.WriteLine(@"$(""select"").change(function(){ _gaq1.push(['_trackEvent',""Forms"",""Change"",location.pathname+"" - ""+$(this).attr(""name"")]);   }); ");
                    }
                }
                output.Indent--;
            }
            output.WriteLine(" });");

            output.RenderEndTag();
        }

        protected override void Render(HtmlTextWriter output)
        {
            GoogleAnalyticsSnippet.RenderSnippetStartScript(output);
            this.RenderSnippetBodyScript(output);
            this.RenderAdditionalTrackingScripts(output);
        }

        [Bindable(true), Category("Behavior"), DefaultValue(true),
         Description("Enables tracking of user interaction with Forms through the _trackEvent method.")]
        public bool EnableFormFieldChangeTracking
        {
            get
            {
                object result = ViewState["EnableFormEventTracking"] ?? true;
                return (bool)result;
            }
            set { ViewState["EnableFormEventTracking"] = value; }
        }

        // TODO: Figure out a way to use a string list editor in the component design view. See Orkin.com ticket:771
        [Bindable(true), Category("Google Analytics Account"),
         Description("Gets and sets the list of domains to track in comma seperated values (CSV) format.")]
        public string RollupTrackerLinkedDomainsCsv
        {
            get
            {
                string result = (string)ViewState["RollupTrackerLinkedDomainsCsv"];

                return result;
                // The list of linked domains was changing to frequently to keep on having to add the
                // linked domains in on the control. So for now just hardcode it until the list stops
                // getting longer.
                //return result ?? string.Empty;
            }
            set { ViewState["RollupTrackerLinkedDomainsCsv"] = value; }
        }

        // TODO: Figure out a way to use a string list editor in the component design view. See Orkin.com ticket:771
        [Bindable(true), Category("Google Analytics Account"), DefaultValue(""),
        Description("Gets and sets the list of domains to track in comma seperated values (CSV) format.")]
        public string PageTrackerLinkedDomainsCsv
        {
            get
            {
                string result = (string)ViewState["PageTrackerLinkedDomainsCsv"] ?? string.Empty;
                return result;
            }
            set { ViewState["PageTrackerLinkedDomainsCsv"] = value; }
        }

        [Bindable(true), Category("Google Analytics Account"), DefaultValue(PageTrackerIdDefaultValue),
         Description("Gets and sets the the ID to use in the _getTracker portion of the tracking snippet.")]
        public string PageTrackerId
        {
            get
            {
                string result = (string)ViewState["PageTrackerId"] ?? PageTrackerIdDefaultValue;
                return result;
            }
            set { ViewState["PageTrackerId"] = value; }
        }

        [Bindable(true), Category("Google Analytics Account"),
         Description("Gets and sets the the ID to use as a roll-up of multiple Google Aanalytics accounts in the _getTracker portion of the tracking snippet.")]
        public string RollupTrackerId
        {
            get
            {
                string result = (string)ViewState["RollupTrackerId"];
                return result;
            }
            set { ViewState["RollupTrackerId"] = value; }
        }

        [Bindable(true), Category("Google Analytics Account"), DefaultValue(""),
         Description("Gets and sets the the value to use in the _setDomainName portion of the tracking snippet. If blank, _setDomainName is not rendered.")]
        public string PageDomainName
        {
            get
            {
                string result = (string)ViewState["PageDomainName"] ?? string.Empty;
                return result;
            }
            set { ViewState["PageDomainName"] = value; }
        }

        [Bindable(true), Category("Google Analytics Account"), DefaultValue(""),
         Description("Gets and sets the the value to use in the _setDomainName portion of the tracking snippet for the roll-up account. If blank, _setDomainName is not rendered.")]
        public string RollupDomainName
        {
            get
            {
                string result = (string)ViewState["RollUpDomainName"] ?? string.Empty;
                return result;
            }
            set { ViewState["RollUpDomainName"] = value; }
        }
    }
}
