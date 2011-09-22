namespace Huddle.Clients.Exceptions
{
    public class ObjectNotFoundException<T> : ObjectException<T>
    {
        public ObjectNotFoundException(ApiResponse response) : base(response) { }

        public ObjectNotFoundException() : base(default(T), typeof (T).Name) { }

        public ObjectNotFoundException(string message, params object[] args) : base(default(T), string.Format(message, args)) { }
    }
}