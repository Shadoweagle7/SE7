using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Events
{
    public interface IEvent {}

    public interface IEvent<TDelegate> : IEvent where TDelegate : Delegate
    {
        public void AddCallback(TDelegate @delegate);
        public void RemoveCallback(TDelegate @delegate);
    }
}
