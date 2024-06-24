using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Utility.Extensions.Linq
{
    public static class OptionLinqExtensions
    {
        public static Option<IEnumerable<Option<T>>> ToOptionEnumerable<T>(this IEnumerable<T> source) =>
            source.Select(t => t.ToOption()).ToOption();

        public static Option<IEnumerable<Option<T>>> ToOptionEnumerable<T>(this Option<IEnumerable<T>> source) =>
            source.Match(e => e.Select(t => t.ToOption()), () => Option.None);

        public static Option<IEnumerable<Option<T>>> ToOptionEnumerable<T>(this IEnumerable<Option<T>> source) =>
            source.ToOption();

        public static Option<T> FirstOrNone<T>(this Option<IEnumerable<Option<T>>> source)
        {
            return source.Match(e => e.FirstOrDefault(), () => Option.None).Unwrap();
        }
    }
}
