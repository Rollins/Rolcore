//-----------------------------------------------------------------------
// <copyright file="XElementExtensionMethods.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Xml.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    public static class XElementExtensionMethods
    {
        /// <summary>
        /// Converts an <see cref="XElement"/> to an <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="xElement">Specifies the <see cref="XElement"/> to convert.</param>
        /// <returns>An <see cref="XmlDocument"/>.</returns>
        public static XmlDocument ToXmlDocument(this XElement xElement)
        {   
            Contract.Requires<ArgumentNullException>(xElement != null, "xElement is null");

            var result = new XmlDocument();
            result.LoadXml(xElement.ToString());
            return result;
        } // TODO: Unit test

        /// <summary>
        /// Retrieves the child element value  
        /// </summary>
        /// <param name="parentElement">The parent element which contains the child element required</param>
        /// <param name="childElementName">Name of the child element to be retrieved</param>
        /// <returns>Returns the value of the child element, if it finds or returns null</returns>
        public static string GetChildElementValue(this XElement parentElement, string childElementName)
        {
            Contract.Requires<ArgumentNullException>(parentElement != null, "parentElement is null");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(childElementName), "childElementName is null or empty");
            
            var _childElement = parentElement.Elements().Where(e => e.Name.LocalName == childElementName).FirstOrDefault();
            return (_childElement == null) ? string.Empty : _childElement.Value;
        } // TODO: Unit test

        /// <summary>
        /// Retrieves all the elements by specified name
        /// </summary>
        /// <param name="xmlReader"></param>
        /// <param name="nameOfTheElement"></param>
        /// <returns>Returns an IEnumerable collection of elements with specified name</returns>
        public static IEnumerable<XElement> GetAllElementsByNameFromXmlReader(XmlReader xmlReader, string nameOfTheElement)
        {
            if (xmlReader == null)
                throw new ArgumentNullException("xmlReader", "xmlReader is null.");
            if (String.IsNullOrEmpty(nameOfTheElement))
                throw new ArgumentException("nameOfTheElement is null or empty.", "nameOfTheElement");

            return XElement.Load(xmlReader).Descendants().Where(e => e.Name.LocalName == nameOfTheElement);
        }
    }
}
