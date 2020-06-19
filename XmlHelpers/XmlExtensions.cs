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
        /// Serialises an object to an XML string.
        /// </summary>
        /// <param name="typeToSerialise">The object to serialise.</param>
        /// <param name="type">The object type you are serialising.</param>
        /// <param name="extraTypes">If you are using any derived types then pass them in here.</param>
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
        /// Deserialises an XML string to a specified type.
        /// </summary>
        /// <typeparam name="T">The type to desrialise to.</typeparam>
        /// <param name="xml">The XML string you are deserialising.</param>
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
