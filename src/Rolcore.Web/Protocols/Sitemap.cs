using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.ComponentModel.Composition;
using System;
using System.Linq;

namespace Rolcore.Web.Protocols
{
    [Export(typeof(Sitemap))]
    public class Sitemap : List<SitemapUrl>
    {
        private string _Xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        #region Constructors
        public Sitemap(){ }
        public Sitemap(int capacity) : base(capacity) { }
        public Sitemap(IEnumerable<SitemapUrl> collection): base(collection){ }
        #endregion Constructors

        public string Xmlns
        {
            get { return _Xmlns; }
            set { _Xmlns = value; }
        }

        public void Write(TextWriter output)
        {
            using (XmlTextWriter writer = new XmlTextWriter(output))
            {
                //writer.WriteStartDocument();
                writer.WriteStartElement("urlset");
                writer.WriteAttributeString("xmlns", this.Xmlns);
                writer.WriteAttributeString("xmlns:geo", "http://www.google.com/geo/schemas/sitemap/1.0");

                foreach (SitemapUrl url in this.OrderBy(item => item.Loc))
                {
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", url.Loc);

                    if (!string.IsNullOrEmpty(url.LastMod))
                        writer.WriteElementString("lastmod", url.LastMod);
                    if (!string.IsNullOrEmpty(url.ChangeFreq))
                        writer.WriteElementString("changefreq", url.ChangeFreq);
                    if (!string.IsNullOrEmpty(url.Priority))
                        writer.WriteElementString("priority", url.Priority);
                    if (url.Loc.EndsWith(".kml")) // See http://code.google.com/apis/kml/documentation/kmlSearch.html
                    {
                        writer.WriteStartElement("geo:geo");
                        writer.WriteElementString("geo:format", "kml");
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();

                }

                writer.WriteEndElement();
                //writer.WriteEndDocument();

                writer.Flush();
            }
        }

        public override string ToString()
        {
            using (StringWriter result = new StringWriter())
            {
                this.Write(result);
                return result.ToString();
            }
        }
    }
}
