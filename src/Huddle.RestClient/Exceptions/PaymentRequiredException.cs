using System;

namespace Huddle.Clients.Exceptions
{
    public class PaymentRequiredException : HuddleApiException
    {
        public PaymentRequiredException(ApiResponse response) : base(response) { }

        public PaymentRequiredException(string message) : base(message) { }

        public PaymentRequiredException(string message, Exception innerException) : base(message, innerException) { }
    }
}