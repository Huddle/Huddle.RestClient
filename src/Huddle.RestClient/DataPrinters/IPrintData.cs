using log4net;

namespace Huddle.Clients.DataPrinters
{
    public interface IPrintData
    {
        void Print(string response, ILog log);
    }
}