using System;
using System.ComponentModel;
using Huddle.Clients.Entities;

namespace Huddle.Clients.Framework
{
    internal class AsyncLongRunningCommand<TResult> : ILongRunningCommand<TResult>
    {
        private DoWorkWith<TResult> _func;
        private bool _cancelled;
        private BackgroundWorker _worker;
        private TResult _result;

        public event EventHandler Started = delegate { };
        public event EventHandler<ILongRunningCommand<TResult>, EventArgs<double>> Progressed = delegate { };
        public event EventHandler<ILongRunningCommand<TResult>, EventArgs<TResult>> Finished = delegate { };
        public event EventHandler<ILongRunningCommand<TResult>, EventArgs<Exception>> Failed = delegate { };
        public event EventHandler Cancelled;

        public bool IsCancelAllowed { get { return true; } }
        
        public AsyncLongRunningCommand(DoWorkWith<TResult> somethingToDo)
        {
            _func = somethingToDo;
        }

        public void Start()
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += DoWork;
            _worker.RunWorkerCompleted += RunWorkCompleted;
            _worker.RunWorkerAsync();
            Started(this, EventArgs.Empty);
        }

        private void RunWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!_cancelled)
            {
                if (e.Error != null)
                {
                    Failed(this, new EventArgs<Exception>(e.Error));
                }
                else
                {
                    Finished(this, new EventArgs<TResult>(_result));
                }
            }
        }

        public void Cancel()
        {
            Cancelled(this, EventArgs.Empty);
        }

        internal void DoWork(object sender, DoWorkEventArgs e)
        {
            _result = _func();
        }
    }
}