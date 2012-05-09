using System.IO;
using System.Xml.Serialization;
using System;

namespace Rolcore.Xml
{
    /// <summary>
    /// Extends <see cref="System.Object"/> with XML-related methods.
    /// </summary>
    public static class ObjectExtenisions
    {
        /// <summary>
        /// Converts the object to an XML formatted string.
        /// </summary>
        /// <param name="obj">Specifies the object to convert.</param>
        /// <returns>A <see cref="string"/> that represents the object.</returns>
        public static string ToXmlString(this object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (StringWriter resultWriter = new StringWriter())
            {
                serializer.Serialize(resultWriter, obj);
                return resultWriter.ToString();
            }
        }

    }
}
