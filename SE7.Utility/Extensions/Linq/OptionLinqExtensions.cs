using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Utility.Extensions.Linq
{
    public static class OptionLinqExtensions
    {
        public static IEnumerable<Option<TResult>> Cast<TResult>(this IEnumerable source, bool throwOnInvalidCast = true)
        {
            foreach (var item in source)
            {
                yield return item
                    .ToOption()
                    .As<TResult>()
                    .Match(
                        onValuePresent: v => v,
                        onEmpty: () =>
                        {
                            if (throwOnInvalidCast)
                            {
                                var fromType = item.GetType();
                                var toType = typeof(TResult);
                                var fromTypeName = fromType.FullName ?? fromType.Name;
                                var toTypeName = toType.FullName ?? toType.Name;

                                throw new InvalidCastException($"Failed to cast from type {fromTypeName} to {toTypeName}.");
                            }

                            return Option.None;
                        }
                    )
                ;
            }
        }

        public static IEnumerable<Option<TResult>> DefaultIfNone<TResult>(this IEnumerable<TResult> source)
        {
            return source.DefaultIfEmpty().Select(x => x.ToOption());
        }

        public static IEnumerable<Option<TResult>> DefaultIfNone<TResult>(this IEnumerable<TResult> source, TResult defaultValue)
        {
            return source.DefaultIfEmpty(defaultValue).Select(x => x.ToOption());
        }

        public static Option<TSource> ElementAtOrNone<TSource>(this IEnumerable<TSource> source, Index index)
        {
            return source.ElementAtOrDefault(index);
        }

        public static Option<TSource> FirstOrNone<TSource>(this IEnumerable<TSource> source)
        {
            return source.FirstOrDefault();
        }

        public static Option<TSource> FirstOrNone<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate
        )
        {
            return source.FirstOrDefault(predicate);
        }

        public static Option<TSource> LastOrNone<TSource>(this IEnumerable<TSource> source)
        {
            return source.LastOrDefault();
        }

        public static Option<TSource> LastOrNone<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate
        )
        {
            return source.LastOrDefault(predicate);
        }

        public static IEnumerable<Option<TResult>> OfType<TResult>(this IEnumerable source)
        {
            return source.OfType<TResult>();
        }

        public static Option<TSource> SingleOrNone<TSource>(this IEnumerable<TSource> source)
        {
            return source.SingleOrDefault();
        }

        public static Option<TSource> SingleOrNone<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate
        )
        {
            return source.SingleOrDefault(predicate);
        }
    }
}
