using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Rolcore.Xml;

namespace Rolcore.Xml.Linq
{
    /// <summary>
    /// Extension methods for <see cref="object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns an <see cref="XElement"/> representing the specified object.
        /// </summary>
        /// <param name="o">Specifies the object to convert.</param>
        public static XElement ToXElement(this object o)
        {
            return XElement.Parse(o.ToXmlString());
        }
    }
}
