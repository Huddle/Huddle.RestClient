using System.Net;
using Huddle.Clients.Exceptions;

namespace Huddle.Clients.ApiCallObservers.HttpStatusCodeHandlers
{
    public class InternalServerErrorExceptionHandler : IHttpStatusCodeExceptionHandler
    {
        public HttpStatusCode StatusCode { get { return HttpStatusCode.InternalServerError; } }

        public void Handle(ApiResponse response)
        {
            throw new InternalServerErrorException(response);
        }
    }

    public class InternalServerErrorException : HuddleApiException
    {
        public InternalServerErrorException(ApiResponse response) : base(response)
        {
        }
    }
}