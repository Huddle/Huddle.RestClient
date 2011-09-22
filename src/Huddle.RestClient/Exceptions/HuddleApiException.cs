using System;
using System.Runtime.Serialization;

namespace Huddle.Clients.Exceptions
{
    public class HuddleApiException : ApplicationException
    {
        public int HuddleErrorCode { get; set; }

        public string MessageResourceKey { get; set; }

        public HuddleApiException(string message) : base(message) { }

        public HuddleApiException(string message, Exception innerException) : base(message, innerException) { }

        public HuddleApiException(ApiResponse response) : base(GetMessage(response)) { }

        private static string GetMessage(ApiResponse response)
        {
            try
            {
                return string.Empty;
            }
            catch (SerializationException)
            {
                return response.Exception.Message;
            }
        }
    }
}