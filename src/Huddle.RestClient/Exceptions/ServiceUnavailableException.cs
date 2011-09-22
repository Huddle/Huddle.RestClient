using System;

namespace Huddle.Clients.Exceptions
{
    public class ServiceUnavailableException : HuddleApiException
    {
        public ServiceUnavailableException() : base(string.Empty) { }

        public ServiceUnavailableException(ApiResponse response) : base(response) { }

        public ServiceUnavailableException(string message) : base(message) { }

        public ServiceUnavailableException(string message, Exception innerException) : base(message, innerException) { }
    }
}