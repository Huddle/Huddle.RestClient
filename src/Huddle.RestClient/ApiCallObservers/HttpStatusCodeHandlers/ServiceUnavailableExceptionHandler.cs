using System.Net;
using Huddle.Clients.Exceptions;

namespace Huddle.Clients.ApiCallObservers.HttpStatusCodeHandlers
{
    public class ServiceUnavailableExceptionHandler : IHttpStatusCodeExceptionHandler
    {
        public HttpStatusCode StatusCode { get { return HttpStatusCode.ServiceUnavailable; } }

        public void Handle(ApiResponse response)
        {
            throw new ServiceUnavailableException(response);
        }
    }
}