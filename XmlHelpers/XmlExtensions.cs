using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XmlHelpers
{
    public static class XmlExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeToSerialise"></param>
        /// <param name="type"></param>
        /// <param name="extraTypes"></param>
        /// <returns></returns>
        public static string SerialiseToXml(this object typeToSerialise, Type type, Type[] extraTypes = null)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(type);

            if (extraTypes != null)
                xmlSerializer = new XmlSerializer(type, extraTypes);

            using (StringWriter stringWriter = new Utf8StringWriter())
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
            {
                xmlSerializer.Serialize(xmlWriter, typeToSerialise);

                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeserialiseXml<T>(this string xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringReader stringReader = new StringReader(xml))
            using (XmlReader xmlReader = XmlReader.Create(stringReader))
            {
                return (T)xmlSerializer.Deserialize(xmlReader);
            }
        }
    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
