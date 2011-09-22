using System;
using System.Net;

namespace Huddle.Clients.Exceptions
{
    public class NetworkConnectionException : HuddleApiException
    {
        public WebExceptionStatus Status { get; private set; }

        public NetworkConnectionException() : base(string.Empty) { }

        public NetworkConnectionException(WebExceptionStatus status) : base(string.Empty)
        {
            Status = status;
        }

        public NetworkConnectionException(WebExceptionStatus status, Exception innerException) :base(string.Empty, innerException)
        {
            Status = status;
        }
    }
}