namespace Huddle.Clients.Exceptions
{
    public class BadRequestException : HuddleApiException
    {
        readonly ApiResponse _response;

        public virtual IApiResponse ApiResponse { get { return _response; } }

        public BadRequestException() : base(string.Empty) { }

        public BadRequestException(ApiResponse response)
            :base(response)
        {
            _response = response;
        }

        public BadRequestException(string message, params object[] args) : base(string.Format(message, args)) { }
    }
}