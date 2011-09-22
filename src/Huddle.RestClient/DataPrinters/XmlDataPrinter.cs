using Huddle.Clients.Converters;
using log4net;

namespace Huddle.Clients.DataPrinters
{
    internal class XmlDataPrinter : IPrintData
    {
        public void Print(string response, ILog log)
        {
            log.DebugFormat("XML:\n{0}", XmlConverter.ToPrettyXml(response));
        }
    }
}