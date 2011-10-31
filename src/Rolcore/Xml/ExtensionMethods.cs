using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Rollins.Xml
{
    public static class ExtensionMethods
    {
        public static string ToXmlString(this object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (StringWriter resultWriter = new StringWriter())
            {
                serializer.Serialize(resultWriter, obj);
                return resultWriter.ToString();
            }
        }

    }
}
