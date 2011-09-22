using System;

namespace Huddle.Clients.Exceptions
{
    public class ConflictException : HuddleApiException
    {
        public string FieldName { get; set; }

        public object Value { get; set; }

        public ConflictException(ApiResponse response) : base(response) { }

        public ConflictException(string message) : base(message) { }

        public ConflictException(string message, Exception innerException) : base(message, innerException) { }
    }
}