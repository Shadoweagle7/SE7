using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Utility.Try
{
    public class TryResult
    {
        public static TryResult Success { get; } = new();

        public Option<Exception> Exception { get; }

        public TryResult(Exception ex) { Exception = ex; }

        protected TryResult() { }

        public static implicit operator TryResult(Exception exception) => new(exception);
    }

    public class TryResult<T> : TryResult
    {
        public Option<T> Result { get; }

        public TryResult(Exception exception) : base(exception) { }
        public TryResult(T result) => Result = result;

        public static implicit operator TryResult<T>(T result) => new(result);
        public static implicit operator TryResult<T>(Exception exception) => new(exception);
    }
}
