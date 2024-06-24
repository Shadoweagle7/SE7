using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Threading.Events
{
    internal class ExceptionThrownInThreadPoolEventArgs : EventArgs
    {
        public Exception Exception { get; }

        public ExceptionThrownInThreadPoolEventArgs(Exception exception) => Exception = exception;
    }
}
