namespace Huddle.Clients.Exceptions
{
    public class ObjectNotFoundException : ObjectNotFoundException<object>
    {
        public ObjectNotFoundException(ApiResponse response) : base(response) { }

        public ObjectNotFoundException(string message, params object[] args) : base(message, args) { }

        public ObjectNotFoundException() :base(string.Empty) { }
    }
}