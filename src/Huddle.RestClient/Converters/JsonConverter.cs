using System.IO;
using System.Text;
using Jayrock.Json;
using Jayrock.Json.Conversion;

namespace Huddle.Clients.Converters
{
    public static class JsonConverter
    {
        public static string ToJson(object entity)
        {
            var sw = new StringWriter(new StringBuilder());
            var writer = new JsonTextWriter(sw) { PrettyPrint = true };
            JsonConvert.Export(entity, writer);
            return sw.ToString();
        }

        public static T FromJson<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
                return default(T);
            
            var reader = new JsonTextReader(new StringReader(json));
            return (T) JsonConvert.Import(typeof (T), reader);
        }

        public static object FromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                return new { };
            
            var reader = new JsonTextReader(new StringReader(json));
            return JsonConvert.Import(reader);
        }

        public static string ToPrettyJson(string json)
        {
            if (json == string.Empty) 
                return json;

            var sb = new StringBuilder();
            using (var reader = new JsonTextReader(new StringReader(json)))
            {
                using (var writer = new JsonTextWriter(new StringWriter(sb)))
                {
                    writer.PrettyPrint = true;
                    writer.WriteFromReader(reader);
                }
            }

            return sb.ToString();
        }
    }
}