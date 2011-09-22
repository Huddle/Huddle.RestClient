using System.Net;
using Huddle.Clients.DataPrinters;

namespace Huddle.Clients
{
    public class ApiResponse<T> : ApiResponse, IApiResponse<T> where T : class
    {
        public virtual T Data { get; private set; }

        internal ApiResponse(T data, WebResponse response, IPrintData xmlDataPrinter) : base(response, xmlDataPrinter)
        {
            Data = data;
        }

        public static implicit operator T(ApiResponse<T> response)
        {
            return response.Data;
        }
    }
}