using System;
using Huddle.Clients.Entities;

namespace Huddle.Clients.Framework
{
    public interface ILongRunningCommand
    {
        event EventHandler<EventArgs<double>> Progressed;
        event EventHandler Finished;
        event EventHandler<EventArgs<Exception>> Failed;
        event EventHandler Cancelled;
        bool IsCancelAllowed { get; }
        void Cancel();
        void Start();
    }

    public interface ILongRunningCommand<TResult>
    {
        event EventHandler Started;
        event EventHandler<ILongRunningCommand<TResult>, EventArgs<double>> Progressed;
        event EventHandler<ILongRunningCommand<TResult>, EventArgs<TResult>> Finished;
        event EventHandler<ILongRunningCommand<TResult>, EventArgs<Exception>> Failed;
        event EventHandler Cancelled;
        bool IsCancelAllowed { get; }
        void Cancel();
        void Start();
    }
}