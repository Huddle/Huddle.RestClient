namespace Huddle.Clients.Exceptions
{
    public class ObjectDeletedException<T> : ObjectException<T>
    {
        public ObjectDeletedException(ApiResponse response) : base(response) { }

        public ObjectDeletedException(string message, T obj) : base(message, obj) { }

        public ObjectDeletedException(string message) : base(message, default(T)) { }
    }
}