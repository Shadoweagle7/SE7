using static SE7.Utility.OptionStatic;

namespace SE7.Utility
{
    public readonly ref struct SuccessEmptyResult;

    public static class Result
    {
        public void TestOption()
        {
            var a = Some(1);
            var b = None;
        }

        public static SuccessEmptyResult Success => new();
    }

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

    public readonly struct Result<TFailure>
    {
        private readonly Option<TFailure> Data;

        public bool IsSuccess => !Data.HasValue;
        public bool IsFailure => Data.Is<TFailure>();

        public Result(SuccessEmptyResult _) { }
        public Result(TFailure failure) => Data = failure;

        public static implicit operator Result<TFailure>(SuccessEmptyResult _) => new(_);
        public static implicit operator Result<TFailure>(TFailure failure) => new(failure);

        public Option<TFailure> GetFailure() => Data.As<TFailure>();
    }
}
