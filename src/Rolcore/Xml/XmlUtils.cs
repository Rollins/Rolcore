using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Rolcore.Xml
{
    /// <summary>
    /// Static class containing utility methods for working with XML.
    /// </summary>
    public static class XmlUtils
    {
        public static string GetElementFromNodeList(XmlNodeList nodeList, string elementName)
        { 
            foreach (XmlNode n in nodeList)
            {
                if (n.Name == elementName)
                    return n.InnerText;
            }
            return string.Empty;
        }

        public static T DeserializeFromXml<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader stream = new StringReader(xml))
                return (T)serializer.Deserialize(stream);
        }
    }
}
