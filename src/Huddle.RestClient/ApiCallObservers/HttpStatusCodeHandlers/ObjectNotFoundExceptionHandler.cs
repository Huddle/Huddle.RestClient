using System.Net;

namespace Huddle.Clients.ApiCallObservers.HttpStatusCodeHandlers
{
    public class ObjectNotFoundExceptionHandler : IHttpStatusCodeExceptionHandler
    {
        public HttpStatusCode StatusCode { get { return HttpStatusCode.NotFound; } }

        public void Handle(ApiResponse response)
        {
            throw new Exceptions.ObjectNotFoundException(response);
        }
    }
}