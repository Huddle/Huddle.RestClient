using System;
using System.Net;

namespace Huddle.Clients
{
    public interface IApiResponse
    {
        HttpStatusCode Status { get; }
        string ContentType { get; }
        string Response { get; }
        bool Error { get; set; }
        Exception Exception { get; set; }
        TData DeserializeAs<TData>();
    }
}