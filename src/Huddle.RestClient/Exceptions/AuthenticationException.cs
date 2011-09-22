using System;

namespace Huddle.Clients.Exceptions
{
    public class AuthenticationException : HuddleApiException
    {
        public AuthenticationException() :base(string.Empty) { }

        public AuthenticationException(string message) : base(message) { }

        public AuthenticationException(string message, Exception innerException) : base(message, innerException) { }
    }
}