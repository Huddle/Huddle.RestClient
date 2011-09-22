using System.Net;

namespace Huddle.Clients
{
    public interface IApiResponse
    {
        HttpStatusCode Status { get; }
        string ContentType { get; }
        string Response { get; }
        TData DeserializeAs<TData>();
    }
}