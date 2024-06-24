using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Utility
{
    public readonly struct Result<TSuccess, TFailure>
    {
        private readonly Option<object> Data;

        public bool IsSuccess => Data.Is<TSuccess>();
        public bool IsFailure => Data.Is<TFailure>();

        public Result(TSuccess success) => Data = success;
        public Result(TFailure failure) => Data = failure;

        public static implicit operator Result<TSuccess, TFailure>(TSuccess success) => new(success);
        public static implicit operator Result<TSuccess, TFailure>(TFailure failure) => new(failure);

        public Option<TSuccess> GetSuccess() => Data.As<TSuccess>();
        public Option<TFailure> GetFailure() => Data.As<TFailure>();
    }
}
