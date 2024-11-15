using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Events.Args
{
    public sealed class AsyncEventArgs<TEventArgs>(TEventArgs args) : EventArgs where TEventArgs : notnull, EventArgs
    {
        private readonly TEventArgs Args = args;

        public class Accessor
        {
            private readonly TEventArgs Args;

            internal Accessor(TEventArgs args) => Args = args;

            public class Successor
            {
                private readonly TEventArgs Args;

                internal Successor(TEventArgs args) => Args = args;

                public void Lock(Action<TEventArgs> action)
                {
                    lock (Args)
                    {
                        action(Args);
                    }
                }

                public void Mutex(Action<TEventArgs> action)
                {
                    Mutex? m = null;

                    try
                    {
                        m = new Mutex(true);

                        action(Args);
                    }
                    finally
                    {
                        m?.ReleaseMutex();
                    }
                }

                public void Semaphore(int initialCount, int maximumCount, Action<TEventArgs> action)
                {
                    Semaphore? s = null;

                    try
                    {
                        s = new Semaphore(initialCount, maximumCount);
                    }
                    finally
                    {
                        s?.Release();
                    }
                }

                public void SemaphoreSlim(int initialCount, int maximumCount, Action<TEventArgs> action)
                {
                    SemaphoreSlim? s = null;
                    
                    try
                    {
                        s = new SemaphoreSlim(initialCount, maximumCount);
                    }
                    finally
                    {
                        s?.Release();
                    }
                }
            }

            public Successor With => new(Args);
        }

        public Accessor Access => new(Args);
    }
}
