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
                    // Avoiding edge case: adding a callback while its being invoked will cause an exception -
                    // "Collection was modified; enumeration operation may not execute."
                    for (int i = 0; i < callbacks.Count; i++)
                    {
                        callbacks[i](arg);
                    };
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