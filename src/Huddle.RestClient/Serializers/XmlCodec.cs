using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Huddle.Clients.Serializers
{
    public class XmlCodec : ICodec
    {
        private static string[] IllegalXmlEntities = new[] 
        { 
            "&#x1;",
            "&#x2;",
            "&#x3;",
            "&#x4;",
            "&#x5;",
            "&#x6;",
            "&#x7;",
            "&#x8;",
            "&#xB;",
            "&#xC;",
            "&#xE;",
            "&#xF;",
            "&#x10;",
            "&#x11;",
            "&#x12;",
            "&#x13;",
            "&#x14;", 
            "&#x15;",
            "&#x16;",
            "&#x17;",
            "&#x18;",
            "&#x19;",
            "&#x1A;",
            "&#x1B;",
            "&#x1C;",
            "&#x1D;",
            "&#x1E;",
            "&#x1F;"
        };

        public string Encode<T>(T data)
        {
            var serializer = new XmlSerializer(data.GetType());
            var builder = new StringBuilder();
            var writer = new StringWriterWithEncoding(builder, new UTF8Encoding());
            serializer.Serialize(writer, data);

            return SanitizeForXmlSerialization(writer.ToString());
        }

        public T Decode<T>(string data)
        {
            var encodedXml = data;
            return (T)new XmlSerializer(typeof(T)).Deserialize(new StringReader(encodedXml));
        }

        /// <summary>
        /// Strips illegal xml 1.0 characters out of the string to conform with our strict api
        /// http://www.w3.org/TR/2008/REC-xml-20081126/#charsets
        /// </summary>
        private string SanitizeForXmlSerialization(string input)
        {
            string sanitizedXml = input;
            foreach (string s in IllegalXmlEntities)
            {
                sanitizedXml = sanitizedXml.Replace(s, " ");
            }

            return sanitizedXml;
        }
    }
}