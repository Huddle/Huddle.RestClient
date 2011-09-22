using System;
using System.Net;
using System.Runtime.Remoting;
using Huddle.Clients.Exceptions;

namespace Huddle.Clients.ApiCallObservers.HttpStatusCodeHandlers
{
    public class BadGatewayExceptionHandler : IHttpStatusCodeExceptionHandler
    {
        public HttpStatusCode StatusCode { get { return HttpStatusCode.BadGateway; } }
        
        public void Handle(ApiResponse response)
        {
            var ex = response.Exception as WebException;

            if (null != ex)
            {
                switch (ex.Status)
                {
                    case WebExceptionStatus.ProtocolError:
                        throw new InvalidOperationException(response.ErrorMessage + " should not be handled as bad gateway");
                    case WebExceptionStatus.ServerProtocolViolation:
                        throw new ServerException(response);
                    default:
                        throw new NetworkConnectionException(ex.Status);
                }
            }
        }
    }
}