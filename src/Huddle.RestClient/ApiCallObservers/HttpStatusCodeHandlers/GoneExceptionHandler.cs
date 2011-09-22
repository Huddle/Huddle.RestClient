using System.Net;
using Huddle.Clients.Exceptions;

namespace Huddle.Clients.ApiCallObservers.HttpStatusCodeHandlers
{
    public class GoneExceptionHandler : IHttpStatusCodeExceptionHandler
    {
        public HttpStatusCode StatusCode { get { return HttpStatusCode.Gone; } }

        public void Handle(ApiResponse response)
        {
            throw new ObjectDeletedException();
        }
    }
}