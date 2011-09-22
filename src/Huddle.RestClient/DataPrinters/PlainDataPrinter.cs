using log4net;

namespace Huddle.Clients.DataPrinters
{
    internal class PlainDataPrinter : IPrintData
    {
        public void Print(string response, ILog log)
        {
            log.Debug("Data:");
            log.Debug(response);
        }
    }
}