using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using SystemBitConverter = System.BitConverter;

namespace SE7.Utility
{
    public static class BitConverterEx
    {
        private static readonly Dictionary<Type, Func<object, byte[]>> Converters = [];

        public static bool TryAddConverter<TNumber>(Func<byte[], TNumber> converter)
        {
            return Converters.TryAdd(typeof(TNumber), o => );
        }

        public static byte[] GetBytes<TNumber>(TNumber number) where TNumber : INumber<TNumber>
        {
            if (number is bool b)
            {
                return SystemBitConverter.GetBytes(b);
            }
            else if (number is char c)
            {
                return SystemBitConverter.GetBytes(c);
            }
            else if (number is short s)
            {
                return SystemBitConverter.GetBytes(s);
            }
            else if (number is ushort us)
            {
                return SystemBitConverter.GetBytes(us);
            }
            else if (number is int i)
            {
                return SystemBitConverter.GetBytes(i);
            }
            else if (number is uint ui)
            {
                return SystemBitConverter.GetBytes(ui);
            }
            else if (number is long l)
            {
                return SystemBitConverter.GetBytes(l);
            }
            else if (number is ulong ul)
            {
                return SystemBitConverter.GetBytes(ul);
            }
            else if (number is float f)
            {
                return SystemBitConverter.GetBytes(f);
            }
            else if (number is double d)
            {
                return SystemBitConverter.GetBytes(d);
            }

            throw new ArgumentException("INTERNAL ERROR: TNumber wasn't a registered INumber.");
        }
    }
}
