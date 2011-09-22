namespace Huddle.Clients.Exceptions
{
    public class ObjectDeletedException : ObjectDeletedException<object>
    {
        public ObjectDeletedException(ApiResponse response) : base(response) { } 
        
        public ObjectDeletedException(string message) : base(message) { }

        public ObjectDeletedException() : base(string.Empty) { }
    }
}