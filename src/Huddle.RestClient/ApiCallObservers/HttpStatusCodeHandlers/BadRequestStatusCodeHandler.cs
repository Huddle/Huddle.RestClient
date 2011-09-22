using System.Net;
using Huddle.Clients.Exceptions;

namespace Huddle.Clients.ApiCallObservers.HttpStatusCodeHandlers
{
    public class BadRequestStatusCodeHandler : IHttpStatusCodeExceptionHandler
    {
        public HttpStatusCode StatusCode { get { return HttpStatusCode.BadRequest; } }

        public void Handle(ApiResponse response)
        {
            throw new BadRequestException(response);
        }
    }
}