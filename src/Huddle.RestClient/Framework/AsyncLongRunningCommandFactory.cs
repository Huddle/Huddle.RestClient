namespace Huddle.Clients.Framework
{
    public class AsyncLongRunningCommandFactory : ILongRunningCommandFactory
    {
        public ILongRunningCommand<T> Create<T>(DoWorkWith<T> func)
        {
            return new AsyncLongRunningCommand<T>(func);
        }
    }
}