using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Utility.Hashing
{
    public static class HashingExtensions
    {
        private static ulong FNV_offset_basis = 14695981039346656037;
        private static ulong FNV_prime = 1099511628211;

        public static TNumber GetHashCode<TNumber>(this TNumber value) where TNumber : INumber<TNumber>, IConvertible
        {
            unchecked
            {
                var hash = FNV_offset_basis;

                var bytes = BitConverterEx.GetBytes(value);

                foreach (var b in bytes)
                {
                    hash ^= b;
                    hash *= FNV_prime;
                }

                return (TNumber)Convert.ChangeType(hash, typeof(TNumber));
            }
        }
    }
}
