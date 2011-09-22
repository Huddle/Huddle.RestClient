namespace Huddle.Clients.Framework
{
    public class BlockingLongRunningCommandFactory : ILongRunningCommandFactory
    {
        public ILongRunningCommand<TResult> Create<TResult>(DoWorkWith<TResult> func)
        {
            return new BlockingLongRunningCommand<TResult>(func);
        }
    }
}