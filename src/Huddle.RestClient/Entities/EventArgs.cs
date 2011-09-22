using System;

namespace Huddle.Clients.Entities
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(){}

        public EventArgs(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public static implicit operator EventArgs<T>(T typeInstance)
        {
            return new EventArgs<T>(typeInstance);
        }

        public static implicit operator T(EventArgs<T> args)
        {
            return args.Data;
        }
    }

    [SerializableAttribute]
    public delegate void EventHandler<TSender, TEventArgs>(TSender sender, TEventArgs e) 
        where TEventArgs : EventArgs;
}
