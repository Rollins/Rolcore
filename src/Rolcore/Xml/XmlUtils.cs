//-----------------------------------------------------------------------
// <copyright file="XmlUtils.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Xml
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Static class containing utility methods for working with XML.
    /// </summary>
    public static class XmlUtils
    {
        public static string GetElementFromNodeList(XmlNodeList nodeList, string elementName)
        { 
            foreach (XmlNode node in nodeList)
                if (node.Name == elementName)
                    return node.InnerText;

            return string.Empty;
        }

        /// <summary>
        /// Deserializes the given XML into an object instance.
        /// </summary>
        /// <typeparam name="T">The type of object to be deserialized.</typeparam>
        /// <param name="xml">The XML to deserialize in to an instance of <see cref="T"/>.</param>
        /// <returns>An instance of <see cref="T"/> deserialized from the given XML.</returns>
        public static T DeserializeFromXml<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentException("xml is null or empty");

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader stream = new StringReader(xml))
                return (T)serializer.Deserialize(stream);
        }
    }
}
