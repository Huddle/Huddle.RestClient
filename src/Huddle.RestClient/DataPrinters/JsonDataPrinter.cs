using Huddle.Clients.Converters;
using log4net;

namespace Huddle.Clients.DataPrinters
{
    internal class JsonDataPrinter : IPrintData
    {
        public void Print(string response, ILog log)
        {
            log.DebugFormat("Json:\n{0}", JsonConverter.ToPrettyJson(response));
        }
    }
}