//-----------------------------------------------------------------------
// <copyright file="XElementExtensionMethods.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

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

        /// <summary>
        /// Retrieves the child element value  
        /// </summary>
        /// <param name="parentElement">The parent element which contains the child element required</param>
        /// <param name="nameOfTheChildElement">Name of the child element to be retrieved</param>
        /// <returns>Returns the value of the child element, if it finds or returns null</returns>
        public static string GetChildElementValue(this XElement parentElement, string nameOfTheChildElement)
        {
            if (parentElement == null)
                throw new ArgumentNullException("parentElement", "parentElement is null.");
            if (String.IsNullOrEmpty(nameOfTheChildElement))
                throw new ArgumentException("nameOfTheChildElement is null or empty.", "nameOfTheChildElement");
            
            var _childElement = parentElement.Elements().Where(e => e.Name.LocalName == nameOfTheChildElement).FirstOrDefault();
            return (_childElement == null) ? string.Empty : _childElement.Value;
        }

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
