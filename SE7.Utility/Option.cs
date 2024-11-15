using System.Diagnostics.CodeAnalysis;

namespace SE7.Utility
{
    public readonly struct EmptyOption;

#error TODO: TESTING
    class MyClass
    {
        void a()
        {
            new Option<int>()
                .Match(
                    Option.MatchTrueIfPresent<int>(),
                    Option.MatchFalseIfNotPresent
                );
        }
    }

    public static class OptionStatic
    {
        public static Option<T> Some<T>(T value) => new(value);
        public static EmptyOption None => Option.None;
    }

    public readonly ref struct Option
    {
        public readonly struct MatchFunc
        {
            private readonly Func<bool> Func;

            public MatchFunc(Func<bool> func) => Func = func;

            public static implicit operator Func<bool>(MatchFunc matchFunc) => matchFunc.Func;
        }

        public readonly struct MatchFunc<T>
        {
            private readonly Func<T, bool> Func;

            public MatchFunc(Func<T, bool> func) => Func = func;

            public static implicit operator Func<T, bool>(MatchFunc<T> matchFunc) => matchFunc.Func;
        }

        public static MatchFunc<T> MatchTrueIfPresent<T>() => new(_ => true);
        public static MatchFunc<T> MatchFalseIfPresent<T>() => new(_ => false);

        public static MatchFunc MatchTrueIfNotPresent { get; } = new(() => true);
        public static MatchFunc MatchFalseIfNotPresent { get; } = new(() => false);

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

    public struct Option<T> : ITypeTestable, IEquatable<Option<T>>, IEquatable<T>
    {
        private object? RawValue;
        internal readonly T? Value => (T?)RawValue;

        [MemberNotNullWhen(true, nameof(RawValue), nameof(Value))]
        public readonly bool HasValue => RawValue != null;
        public readonly Option<Type> Type => typeof(T);

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
            if (HasValue)
            {
                return new Option<TResult>(selector(Value));
            }

            return Option.None;
        }

        public readonly Option<TResult> Match<TResult>(Func<T, TResult> onValuePresent, Func<TResult> onEmpty)
        {
            if (HasValue)
            {
                return new Option<TResult>(onValuePresent(Value));
            }

            return new Option<TResult>(onEmpty());
        }

        public readonly Option<TResult> Match<TResult>(Func<T, EmptyOption> onValuePresent, Func<TResult> onEmpty)
        {
            if (HasValue)
            {
                return new Option<TResult>(onValuePresent(Value));
            }

            return new Option<TResult>(onEmpty());
        }

        public readonly Option<TResult> Match<TResult>(Func<T, TResult> onValuePresent, Func<EmptyOption> onEmpty)
        {
            if (HasValue)
            {
                return new Option<TResult>(onValuePresent(Value));
            }

            return new Option<TResult>(onEmpty());
        }

        public Option<T> SetValue(T? value)
        {
            RawValue = value;

            return this;
        }

        public readonly bool Is<TOther>() => RawValue is TOther;
        public readonly bool Is<TOther>(out Option<TOther> other)
        {
            if (Is<TOther>())
            {
                other = new Option<TOther>((TOther)RawValue!);

                return true;
            }

            other = default;

            return false;
        }

        public readonly Option<TResult> As<TResult>()
        {
            return Is<TResult>(out var value) ? value : Option.None;
        }

        public readonly bool Equals(Option<T> other)
        {
            if (!HasValue && !other.HasValue)
            {
                return true;
            }

            if ((!HasValue && other.HasValue) || (HasValue && !other.HasValue))
            {
                return false;
            }

            return RawValue!.Equals(other.RawValue);
        }

        public readonly bool Equals(Option<T> other, IEqualityComparer<T> equalityComparer)
        {
            if (!HasValue && !other.HasValue)
            {
                return true;
            }

            if ((!HasValue && other.HasValue) || (HasValue && !other.HasValue))
            {
                return false;
            }

            return equalityComparer.Equals(Value, other.Value);
        }

        public readonly bool Equals(T? other)
        {
            if (!HasValue && other == null)
            {
                return true;
            }

            if ((!HasValue && other != null) || (HasValue && other == null))
            {
                return false;
            }

            return RawValue!.Equals(other);
        }

        public readonly bool Equals(T? other, IEqualityComparer<T> equalityComparer)
        {
            if (!HasValue && other == null)
            {
                return true;
            }

            if ((!HasValue && other != null) || (HasValue && other == null))
            {
                return false;
            }

            return equalityComparer.Equals(Value, other);
        }

        public override readonly bool Equals(object? obj) => obj is Option<T> option && Equals(option);
        public readonly bool Equals(object? obj, IEqualityComparer<T> equalityComparer)
            => obj is Option<T> option && Equals(option, equalityComparer);

        public override readonly int GetHashCode() => RawValue?.GetHashCode() ?? 0;
    }
}
