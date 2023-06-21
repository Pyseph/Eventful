using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System.Diagnostics;

namespace Eventful
{
    public class GameEvents
    {
        public class InvokedEvent<T> : GameEvents
        {
            private bool yieldThread;


            public delegate void InvokedDelegate(T Data);
            public event InvokedDelegate Invoked;

            public InvokedEvent(bool yieldThread = false)
            {
                this.yieldThread = yieldThread;

                Invoked += (T Data) => { };
            }

            public void Invoke(T Data)
            {
                if (yieldThread)
                {
                    foreach (InvokedDelegate handler in Invoked.GetInvocationList())
                    {
                        handler.Invoke(Data);
                    }
                } else {
                    // call handlers on a separate thread
                    new Thread(() =>
                    {
                        foreach (InvokedDelegate handler in Invoked.GetInvocationList())
                        {
                            handler.Invoke(Data);
                        }
                    }).Start();
                }
            }
        }

        public static InvokedEvent<double> PreRender = new(true);
        public static InvokedEvent<double> PrePhysics = new(true);
        public static InvokedEvent<double> PostPhysics = new();

        public static InvokedEvent<Keys> InputBegan = new();
        public static InvokedEvent<Keys> InputEnded = new();

        public static InvokedEvent<Point> MouseButton1Down = new();
    }
}