using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Huddle.Clients.Converters
{
    public static class XmlConverter
    {
        public static string ToXml(object entity)
        {
            var serializer = new XmlSerializer(entity.GetType());
            var builder = new StringBuilder();
            var writer = new StringWriterWithEncoding(builder, new UTF8Encoding());
            serializer.Serialize(writer, entity);

            return builder.ToString();
        }

        public static T FromXml<T>(string xml)
        {
            var encodedXml = xml;
            return (T)new XmlSerializer(typeof(T)).Deserialize(new StringReader(encodedXml));
        }

        public static string ToPrettyXml(string xml)
        {
            try
            {
                using (var stringWriter = new StringWriter())
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(xml);

                    var xmlReader = new XmlNodeReader(doc);
                    var xmlWriter = new XmlTextWriter(stringWriter)
                    {
                        Formatting = Formatting.Indented, 
                        Indentation = 1, 
                        IndentChar = '\t'
                    };

                    xmlWriter.WriteNode(xmlReader, true);

                    return stringWriter.ToString();
                }
            }
            catch (XmlException)
            {
                return xml;
            }
            catch (WebException) 
            {
                return xml;
            }
        }
    }
}