//-----------------------------------------------------------------------
// <copyright file="XmlReaderExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Xml
{
    using System;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Extension methods for <see cref="XmlReader"/>.
    /// </summary>
    public static class XmlReaderExtensions
    {
        /// <summary>
        /// Reads the contents of the specified <see cref="XmlReader"/> into a string.
        /// </summary>
        /// <param name="xmlReader">Specifies the <see cref="XmlReader"/> to read.</param>
        /// <returns>A string containing the contents of the specified <see cref="XmlReader"/>.</returns>
        public static string ReadToString(this XmlReader xmlReader)
        {
            if (xmlReader == null)
                throw new ArgumentNullException("xmlReader");

            var result = new StringBuilder();
            while (xmlReader.Read())
                result.Append(xmlReader.ReadOuterXml());

            return result.ToString();
        }
    }
}
