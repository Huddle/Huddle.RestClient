    using System.Net;

namespace Huddle.Clients.ApiCallObservers.HttpStatusCodeHandlers
{
    public interface IHttpStatusCodeExceptionHandler 
    {
        HttpStatusCode StatusCode { get; }
        void Handle(ApiResponse response);
    }
}