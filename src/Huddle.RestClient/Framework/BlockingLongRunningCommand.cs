using System;
using Huddle.Clients.Entities;

namespace Huddle.Clients.Framework
{
    internal class BlockingLongRunningCommand<T> : ILongRunningCommand<T>
    {
        private bool _cancelled;
        private DoWorkWith<T> _func;

        public event EventHandler Started = delegate { };
        public event EventHandler<ILongRunningCommand<T>, EventArgs<double>> Progressed = delegate { };
        public event EventHandler<ILongRunningCommand<T>, EventArgs<T>> Finished = delegate { };
        public event EventHandler<ILongRunningCommand<T>, EventArgs<Exception>> Failed = delegate { };
        public event EventHandler Cancelled;
        
        public bool IsCancelAllowed { get { return true; } }

        public BlockingLongRunningCommand(DoWorkWith<T> func)
        {
            _func = func;
        }

        public void Cancel()
        {
            _cancelled = true;
            Cancelled(this, EventArgs.Empty);
        }

        public void Start()
        {
            try
            {
                if(Started != null)
                {
                    Started(this, EventArgs.Empty);
                }

                var result = _func();
                if (!_cancelled)
                {
                    Finished(this, new EventArgs<T>(result));
                }
            }
            catch (Exception ex)
            {
                Failed(this, new EventArgs<Exception>(ex));
            }
        }
    }
}