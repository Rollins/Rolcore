using System;
using System.Xml;
using System.Xml.Linq;

namespace Rolcore.Xml.Linq
{
    public static class XElementExtensionMethods
    {
        /// <summary>
        /// Converts an <see cref="XElement"/> to an <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="xElement">Specifies the <see cref="XElement"/> to convert.</param>
        /// <returns>An <see cref="XmlDocument"/>.</returns>
        public static XmlDocument ToXmlDocument(this XElement xElement)
        {   //TODO: Unit test
            if (xElement == null)
                throw new ArgumentNullException("xElement", "xElement is null.");

            XmlDocument result = new XmlDocument();

            result.LoadXml(xElement.ToString());

            return result;
        }
    }
}
