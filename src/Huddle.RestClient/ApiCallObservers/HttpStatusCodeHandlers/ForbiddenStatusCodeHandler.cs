using System.Net;
using Huddle.Clients.Exceptions;

namespace Huddle.Clients.ApiCallObservers.HttpStatusCodeHandlers
{
    public class ForbiddenStatusCodeHandler : IHttpStatusCodeExceptionHandler
    {
        public HttpStatusCode StatusCode { get { return HttpStatusCode.Forbidden; } }

        public void Handle(ApiResponse response)
        {
            throw new AuthorizationException(response);
        }
    }
}