using System.Net;
using Huddle.Clients.Exceptions;

namespace Huddle.Clients.ApiCallObservers.HttpStatusCodeHandlers
{
    public class AuthenticationFailedStatusCodeHandler : IHttpStatusCodeExceptionHandler
    {
        public HttpStatusCode StatusCode
        {
            get { return HttpStatusCode.Unauthorized; }
        }

        public void Handle(ApiResponse response)
        {
            throw new AuthenticationException("Authentication failed");
        }
    }
}
