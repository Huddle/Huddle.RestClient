namespace Huddle.Clients.Exceptions
{
    public class ObjectException<T> : HuddleApiException
    {
        public T Object { get; private set; }

        public ObjectException(ApiResponse response) : base(response) { }
      
        public ObjectException(T obj, string message, params object[] args) : base(string.Format(message, args))
        {
            Object = obj;
        }

        public ObjectException(string message, T obj) : this(obj, message, typeof(T).Name) { }
    }
}