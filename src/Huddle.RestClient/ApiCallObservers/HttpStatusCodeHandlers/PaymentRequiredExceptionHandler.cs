using System.Net;
using Huddle.Clients.Exceptions;

namespace Huddle.Clients.ApiCallObservers.HttpStatusCodeHandlers
{
    public class PaymentRequiredExceptionHandler : IHttpStatusCodeExceptionHandler
    {
        public HttpStatusCode StatusCode { get { return HttpStatusCode.PaymentRequired; } }

        public void Handle(ApiResponse response)
        {
            throw new PaymentRequiredException(response);
        }
    }
}