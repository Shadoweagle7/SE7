namespace SE7.Utility
{
    public readonly struct EmptyOption;

    public readonly ref struct Option
    {
        public static EmptyOption None { get; } = new();

        public static bool operator ==(Option _, EmptyOption __) => true;
        public static bool operator !=(Option _, EmptyOption __) => false;
        public static bool operator ==(EmptyOption _, Option __) => true;
        public static bool operator !=(EmptyOption _, Option __) => false;

        public override bool Equals(object? obj) => obj is EmptyOption eo && Equals(eo);

        public override int GetHashCode() => 0;

        public static Option<T> Create<T>() => new();
        public static Option<T> Create<T>(T? value) => new(value);
        public static Option<T> Create<T>(EmptyOption empty) => new(empty);
    }

    public struct Option<T> : ITypeTestable, IEquatable<Option<T>>
    {
        private object? RawValue;
        internal readonly T? Value => (T?)RawValue;

        public Option() => RawValue = null;
        public Option(T? value) => RawValue = value;
        public Option(EmptyOption _) => RawValue = null;
        public Option(Option _) => RawValue = null;

        public static implicit operator Option<T>(T? value) => new(value);
        public static implicit operator Option<T>(EmptyOption _) => new();
        public static implicit operator Option<T>(Option _) => new();
        public static bool operator ==(Option<T> lhs, Option<T> rhs) => lhs.RawValue == rhs.RawValue;
        public static bool operator !=(Option<T> lhs, Option<T> rhs) => lhs.RawValue != rhs.RawValue;

        public readonly Option<Type> GetRuntimeType() => new(RawValue?.GetType());

        public readonly Option<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            if (RawValue != null)
            {
                return new Option<TResult>(selector((T)RawValue));
            }

            return Option.None;
        }

        public readonly Option<TResult> Match<TResult>(Func<T, TResult> onValuePresent, Func<TResult> onEmpty)
        {
            if (RawValue != null)
            {
                return new Option<TResult>(onValuePresent((T)RawValue));
            }

            return new Option<TResult>(onEmpty());
        }

        public readonly Option<TResult> Match<TResult>(Func<T, EmptyOption> onValuePresent, Func<TResult> onEmpty)
        {
            if (RawValue != null)
            {
                return new Option<TResult>(onValuePresent((T)RawValue));
            }

            return new Option<TResult>(onEmpty());
        }

        public readonly Option<TResult> Match<TResult>(Func<T, TResult> onValuePresent, Func<EmptyOption> onEmpty)
        {
            if (RawValue != null)
            {
                return new Option<TResult>(onValuePresent((T)RawValue));
            }

            return new Option<TResult>(onEmpty());
        }

        public Option<T> SetValue(T? value)
        {
            RawValue = value;

            return this;
        }

        public readonly bool Is<TOther>() => RawValue is TOther;
        public readonly bool Is<TOther>(out TOther? other)
        {
            if (Is<TOther>())
            {
                other = (TOther)RawValue!;

                return true;
            }

            other = default;

            return false;
        }

        public readonly Option<TResult> As<TResult>()
        {
            if (RawValue == null)
            {
                return Option.None;
            }

            return new Option<TResult>((TResult)RawValue);
        }

        public readonly bool Equals(Option<T> other)
        {
            if (RawValue == null && other.RawValue == null)
            {
                return true;
            }

            if ((RawValue == null && other.RawValue != null) || (RawValue != null && other.RawValue == null))
            {
                return false;
            }

            return RawValue!.Equals(other.RawValue);
        }

        public override readonly bool Equals(object? obj) => obj is Option<T> option && Equals(option);

        public override readonly int GetHashCode() => RawValue?.GetHashCode() ?? 0;
    }
}
