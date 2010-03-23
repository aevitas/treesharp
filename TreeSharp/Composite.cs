using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeSharp
{
    /// <summary>
    /// The base class of the entire behavior tree system.
    /// Nearly all branches derive from this class.
    /// </summary>
    public abstract class Composite
    {
        public Exception LastException { get; set; }

        public IEnumerable<Composite> Children { get; set; }
        protected Stack<CleanupHandler> CleanupHandlers { get; set; }

        private IEnumerator<RunStatus> _current;
        protected static readonly object Locker = new object();

        public RunStatus? LastStatus { get; set; }

        public bool IsRunning { get { return _current != null && LastStatus.HasValue && LastStatus.Value == RunStatus.Running; } }

        public abstract IEnumerable<RunStatus> Execute(object context);
        
        public virtual RunStatus Tick(object context)
        {
            lock (Locker)
            {
                if (LastStatus.HasValue && LastStatus != RunStatus.Running)
                    return LastStatus.Value;
                if (_current == null)
                {
                    throw new ApplicationException("Cannot run Tick before running Start first!");
                }
                if (_current.MoveNext())
                    LastStatus = _current.Current;
                else
                    throw new ApplicationException("Nothing to run? Something has gone wrong!");

                if (LastStatus != RunStatus.Running)
                    Stop(context);

                return LastStatus.Value;
            }
        }

        public virtual void Start(object context)
        {
            LastStatus = null;
            LastException = null;
            _current = Execute(context).GetEnumerator();
        }

        public virtual void Stop(object context)
        {
            if (_current != null)
            {
                _current.Dispose();
                _current = null;
            }

            if (LastStatus.HasValue && LastStatus.Value == RunStatus.Running)
            {
                // Set the last status to failed if we died mid-stream. THis is usually a fail case.
                LastException = new Exception("Stopped the branch while it was running!");
                LastStatus = RunStatus.Exception;
            }
        }

        protected void Cleanup()
        {
            if (CleanupHandlers.Count != 0)
            {
                lock (Locker)
                {
                    while(CleanupHandlers.Count!=0)
                    {
                        CleanupHandlers.Pop().Dispose();
                    }
                }
            }
        }

        protected abstract class CleanupHandler : IDisposable
        {
            private bool _disposed;

            protected CleanupHandler(Composite owner, object context)
            {
                Owner = owner;
                Context = context;
            }

            public Composite Owner { get; private set; }
            public object Context { get; private set; }
            public bool IsDisposed { get { return _disposed; } }

            public void Dispose()
            {
                if (!IsDisposed)
                {
                    lock(Locker)
                    {
                        _disposed = true;
                        DoCleanup(Context);
                    }
                }
            }

            protected abstract void DoCleanup(object context);
        }

        protected class ChildrenCleanupHandler : CleanupHandler
        {
            public ChildrenCleanupHandler(Composite owner, object context) : base(owner, context)
            {
            }

            protected override void DoCleanup(object context)
            {
                foreach (var composite in Owner.Children)
                {
                    composite.Stop(context);
                }
            }
        }
    }
}
