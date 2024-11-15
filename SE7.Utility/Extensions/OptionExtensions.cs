namespace SE7.Utility.Extensions
{
    public static class OptionExtensions
    {
        public static Option<T> ToOption<T>(this T? value) => new(value);
        public static Option<T> Unwrap<T>(this Option<Option<T>> value) => value.Value;
        public static bool ValueOrTrueIfNone(this Option<bool> value) => value.Match(v => v, () => true).Value;
        public static bool ValueOrFalseIfNone(this Option<bool> value) => value.Match(v => v, () => false).Value;
        public static string ValueOrEmptyStringIfNone(this Option<string> value) => value.Match(v => v, () => string.Empty).Value!;
    }
}
