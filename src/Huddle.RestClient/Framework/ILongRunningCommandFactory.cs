namespace Huddle.Clients.Framework
{
    public delegate TResult DoWorkWith<TResult>();

    public interface ILongRunningCommandFactory
    {
        ILongRunningCommand<TResult> Create<TResult>(DoWorkWith<TResult> func);
    }
}