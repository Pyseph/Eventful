using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventful
{
    public class Connection
    {
        private Action callback;
        public Connection(Action callback)
        {
            this.callback = callback;
        }
        public void Disconnect()
        {
            callback();
        }
    }
    public class GameEvents
    {
        public class Event<T> : GameEvents
        {
            private bool yieldsUntilAllInvoked;
            private List<Action<T>> callbacks = new();
            public Event(bool yieldsUntilAllInvoked = false)
            {
                this.yieldsUntilAllInvoked = yieldsUntilAllInvoked;
            }

            public Connection Connect(Action<T> callback)
            {
                callbacks.Add(callback);
                return new Connection(() => {
                    callbacks.Remove(callback);
                });
            }
            public void Invoke(T arg)
            {
                if (yieldsUntilAllInvoked)
                {
                    foreach (Action<T> callback in callbacks)
                    {
                        callback(arg);
                    }
                }
                else
                {
                    Parallel.ForEach(callbacks, callback => {
                        callback(arg);
                    });
                }
            }
        }
    }
}