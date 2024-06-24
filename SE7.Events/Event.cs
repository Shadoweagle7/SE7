using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Events
{
    public abstract class Event : IEvent<Action<EventArgs>>
    {
        private readonly List<Action<EventArgs>> Callbacks = [];

        public void AddCallback(Action<EventArgs> @delegate) => Callbacks.Add(@delegate);

        public void Raise(EventArgs eventArgs)
        {
            foreach (var callback in Callbacks)
            {
                callback(eventArgs);
            }
        }

        public void RemoveCallback(Action<EventArgs> @delegate) => Callbacks.Remove(@delegate);
    }
}
