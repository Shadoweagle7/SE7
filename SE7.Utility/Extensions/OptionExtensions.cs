using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Utility.Extensions
{
    public static class OptionExtensions
    {
        public static Option<T> ToOption<T>(this T? value) => new(value);
        public static Option<T> Unwrap<T>(this Option<Option<T>> value) => value.Value;
    }
}
