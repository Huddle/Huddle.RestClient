namespace Huddle.Clients.Exceptions
{
    public class AuthorizationException : HuddleApiException
    {
        public AuthorizationException(ApiResponse response):base(response) { }

        public AuthorizationException():base(string.Empty) { }

        public AuthorizationException(string message):base(message) { }
    }
}