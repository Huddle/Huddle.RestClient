namespace Huddle.Clients.Exceptions
{
    public class ObjectException : ObjectException<object>
    {
        public ObjectException(object obj, string message, params object[] args) : base(obj, message, args) { }

        public ObjectException(string message, object obj) : base(message, obj) { }
    }
}